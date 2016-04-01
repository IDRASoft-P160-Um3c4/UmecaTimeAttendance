using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.Windows.Forms;
using BiometricService.BiometricWs;
using zkemkeeper;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace BiometricService
{
    public class Service
    {
        public event EnrollEventHandler EnrollCompleted;

        protected virtual void OnEnrollCompleted(EnrollEventArgs e)
        {
            var handler = EnrollCompleted;
            handler?.Invoke(this, e);
        }

        public void ClearEvents()
        {
            if (EnrollCompleted == null) return;
            foreach (var del in EnrollCompleted.GetInvocationList())
            {
                EnrollCompleted -= (EnrollEventHandler)del;
            }
        }

        private CZKEMClass _service = new CZKEMClass();
        private const int MachineNumber = 1; //the serial number of the device.After connecting the device ,this value will be changed.
        private List<Device> _devices;
        private readonly string _connectionString;
        public bool IsConnected { get; set; }
        public List<UserInfo> UsersInfo { get; private set; } = new List<UserInfo>();
        public UserInfo ImputedInfo { get; private set; } = new UserInfo();

        public List<Device> Devices
        {
            get
            {
                GetDevices();
                return _devices;
            }
        }
        private void GetDevices()
        {

            using (var biometricSvc = GetNewBiometricWsPortTypeClient())
            {
                if (biometricSvc == null) return;
                var data = biometricSvc.getDevices();
                _devices = JsonConvert.DeserializeObject<List<Device>>(data.data);
                biometricSvc.Close();
            }
        }
        private bool Connect(Device device)
        {
            IsConnected = _service.Connect_Net(device.ip, device.port);

            if (!IsConnected)
            {
                ViewError();
                return false;
            }
            return true;
        }
        private void Disconnect()
        {
            _service.Disconnect();
        }
        public Service()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["umeca"].ConnectionString;
        }

        public void UpdateUsers()
        {
            GetUsersFromDb();
            var n = 0;

            foreach (var device in Devices)
            {
                Connect(device);
                _service.EnableDevice(MachineNumber, false);
                if (_service.ClearData(MachineNumber, 5))
                    _service.RefreshData(MachineNumber);

                foreach (var userInfo in UsersInfo)
                {
                    if (_service.SSR_SetUserInfo(MachineNumber, userInfo.EnrollNumber, userInfo.Name, "", 0, true))
                    {
                        if (_service.SSR_DeleteEnrollDataExt(MachineNumber, userInfo.EnrollNumber, 11))
                        {
                            _service.RefreshData(MachineNumber);
                            foreach (var fingerPrint in userInfo.FingerPrints)
                            {
                                if (_service.SetUserTmpExStr(MachineNumber, userInfo.EnrollNumber, fingerPrint.Finger, 1,
                                    fingerPrint.Data))
                                {
                                    n++;
                                }
                            }
                        }
                    }
                }

                _service.EnableDevice(MachineNumber, true);
                Disconnect();
            }
        }

        public enum FingerPrintOperation
        {
            Update,
            Delete
        }

        public void GetAttendanceLog()
        {
            GetUsersFromDb();
            GetDevices();
            var attendanceLogs = new List<AttendanceLog>();

            foreach (var device in Devices)
            {
                Connect(device);

                _service.EnableDevice(MachineNumber, false);//disable the device
                if (_service.ReadGeneralLogData(MachineNumber))//read all the attendance records to the memory
                {
                    int verifyMode, inOutMode, year, month, day, hour, minute, second, workcode = 0;
                    string enrollNumber;
                    while (_service.SSR_GetGeneralLogData(MachineNumber, out enrollNumber, out verifyMode,
                               out inOutMode, out year, out month, out day, out hour, out minute, out second, ref workcode))//get records from the memory
                    {
                        attendanceLogs.Add(new AttendanceLog { EnrollNumber = enrollNumber, Date = new DateTime(year, month, day, hour, minute, second), InOutMode = inOutMode, VerifyMode = verifyMode, WorkCode = inOutMode });
                    }
                }
                else
                {
                    ViewError();
                }
                _service.EnableDevice(MachineNumber, true);//enable the device

                Disconnect();
            }

            var enrollNumbers = UsersInfo.Select(u => u.EnrollNumber.ToString()).Distinct().ToList();
            attendanceLogs = attendanceLogs.Where(a => enrollNumbers.Contains(a.EnrollNumber)).ToList();

            

            if (attendanceLogs.Count > 0)
                using (var biometricSvc = GetNewBiometricWsPortTypeClient())
                {
                    if (biometricSvc == null) return;
                    biometricSvc.updateAttendanceLogs(JsonConvert.SerializeObject(attendanceLogs));
                    biometricSvc.Close();
                }


               

            foreach (var device in Devices)
            {
                Connect(device);
                attendanceLogs.Clear();
                _service.EnableDevice(MachineNumber, false);//disable the device
                if (_service.ClearGLog(MachineNumber))
                {
                    _service.RefreshData(MachineNumber);//the data in the device should be refreshed
                }
                //else
                //{
                //    _service.GetLastError(ref idwErrorCode);
                //}
                _service.EnableDevice(MachineNumber, true);//enable the device
                Disconnect();
            }
        }

        public void UpdateUserFingerPrint(string enrollNumber, int finger, string fingerPrint, FingerPrintOperation operation)
        {
            var name = UsersInfo.FirstOrDefault(u => u.EnrollNumber == enrollNumber)?.Name ?? "";
            var n = 0;

            using (var biometricSvc = GetNewBiometricWsPortTypeClient())
            {
                if (biometricSvc == null) return;
                biometricSvc.updateUserFingerPrint(enrollNumber, finger, fingerPrint, (int)operation);
                biometricSvc.Close();
            }


            n = 0;
            foreach (var device in Devices)
            {
                Connect(device);

                _service.RefreshData(MachineNumber);
                if (operation == FingerPrintOperation.Update)
                {
                    if (_service.SSR_SetUserInfo(MachineNumber, enrollNumber, name, "", 0, true))
                    {
                        if (_service.SetUserTmpExStr(MachineNumber, enrollNumber, finger, 1, fingerPrint))
                        {
                            n++;
                        }
                    }
                }
                else
                {
                    if (_service.SSR_DelUserTmpExt(MachineNumber, enrollNumber, finger))
                    {
                        n++;
                    }
                }

                Disconnect();
            }
        }

        public void Enroll(long deviceId, string userId, int finger)
        {
            var device = Devices.FirstOrDefault(d => d.id == deviceId);
            if (device == null)
                return;
            _service = new CZKEMClass();
            Connect(device);
            _service.CancelOperation();
            _service.SSR_DelUserTmpExt(MachineNumber, userId, finger);//If the specified index of user's templates has existed ,delete it first.(SSR_DelUserTmp is also available sometimes)

            _service.OnEnrollFinger += (number, index, result, length) =>
            {
                int flag, tmpLength;
                string tmpData;

                var enroll = _service.GetUserTmpExStr(MachineNumber, userId, finger, out flag, out tmpData,
                    out tmpLength)
                    ? new EnrollEventArgs(true, finger, tmpData)
                    : new EnrollEventArgs(false, -1, null);
                OnEnrollCompleted(enroll);


                if (!enroll.Result)
                    return;

                var userInfo = UsersInfo.FirstOrDefault(u => u.EnrollNumber == userId);
                if (userInfo == null)
                    return;
                var fingerPrint = userInfo.FingerPrints.FirstOrDefault(f => f.Finger == finger);
                if (fingerPrint == null)
                    userInfo.FingerPrints.Add(new FingerPrint { Finger = finger, Data = tmpData });
                else
                    fingerPrint.Data = tmpData;
            };


            if (_service.StartEnrollEx(userId, finger, 0))
            {
                _service.StartIdentify();//After enrolling templates,you should let the device into the 1:N verification condition
            }
            else
            {
                var idwErrorCode = 0;
                _service.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode, "Error");
            }
        }

        public void GetImputedFromDb(long idImputed)
        {
            using (var biometricSvc = GetNewBiometricWsPortTypeClient())
            {
                if (biometricSvc == null) return;
                var x = biometricSvc.getImputed(idImputed);
                biometricSvc.Close();

                ImputedInfo = JsonConvert.DeserializeObject<UserInfo>(x.data);
            }
        }

        public void UpdateImputedFingerPrint(long user, string enrollNumber, int finger, string fingerPrint,
            FingerPrintOperation operation, long deviceId)
        {
            var name = UsersInfo.FirstOrDefault(u => u.EnrollNumber == enrollNumber)?.Name ?? "";
            var n = 0;

            using (var biometricSvc = GetNewBiometricWsPortTypeClient())
            {
                if (biometricSvc == null) return;
                biometricSvc.updateImputedFingerPrint(enrollNumber, finger, fingerPrint, (int)operation);
                biometricSvc.Close();
            }

            n = 0;
            var device = Devices.FirstOrDefault(d => d.id == deviceId);

            Connect(device);

            _service.RefreshData(MachineNumber);
            if (operation == FingerPrintOperation.Update)
            {
                if (_service.SSR_SetUserInfo(MachineNumber, enrollNumber, name, "", 0, true))
                {
                    if (_service.SetUserTmpExStr(MachineNumber, enrollNumber, finger, 1, fingerPrint))
                    {
                        n++;
                    }
                }
            }
            else
            {
                if (_service.SSR_DelUserTmpExt(MachineNumber, enrollNumber, finger))
                {
                    n++;
                }
            }

            Disconnect();
        }

        private void ReadUsersInternal(Device device)
        {
            if (!Connect(device)) return;

            _service.EnableDevice(MachineNumber, false);
            _service.ReadAllUserID(MachineNumber);

            string enrollNumber, name, password;
            int privilege;
            bool enabled;

            while (_service.SSR_GetAllUserInfo(MachineNumber, out enrollNumber, out name, out password, out privilege, out enabled))
            {
                var user = new UserInfo
                {
                    EnrollNumber = enrollNumber,
                    Name = name,
                    Password = password,
                    Privilege = privilege,
                    Enabled = enabled,
                    FingerPrints = new List<FingerPrint>()
                };

                if (UsersInfo.All(u => u.EnrollNumber != user.EnrollNumber))
                {
                    _service.SSR_DeleteEnrollDataExt(MachineNumber, user.EnrollNumber, 12);
                    continue;
                }
                UsersInfo.Add(user);

                for (var idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    int flag, tmpLength;
                    string tmpData;
                    if (_service.GetUserTmpExStr(MachineNumber, enrollNumber, idwFingerIndex, out flag, out tmpData,
                        out tmpLength)) //get the corresponding templates string and length from the memory
                    {
                        user.FingerPrints.Add(new FingerPrint { Finger = idwFingerIndex, Data = tmpData });
                    }
                }
            }

            _service.ReadAllUserID(MachineNumber);//read all the user information to the memory
            _service.ReadAllTemplate(MachineNumber);//read all the users' fingerprint templates to the memory
            _service.EnableDevice(MachineNumber, true);

            Disconnect();
        }

        public void ReadUsers()
        {
            GetDevices();

            foreach (var device in _devices)
            {
                ReadUsersInternal(device);
            }
        }

        private void ViewError()
        {
            var idwErrorCode = 0;
            _service.GetLastError(ref idwErrorCode);
            string value;
            Console.WriteLine(_messageError.TryGetValue(idwErrorCode, out value) ? value : "Unknow error" + idwErrorCode);
        }

        private readonly IDictionary<int, string> _messageError = new Dictionary<int, string>
        {
            {-100, "Operation failed or data not exist"},
            {-10, "Transmitted data length is incorrect"},
            {-5, "Data already exists"},
            {-4, "Space is not enough"},
            {-3, "Error size"},
            {-2, "Error in file read/write"},
            {-1, "SDK is not initialized and needs to be reconnected"},
            {0, "Data not found or data repeated"},
            {1, "Operation is correct"},
            {4, "Parameter is incorrect"},
            {101, " Error in allocating buffer"},
        };

        public void GetUsersFromDb()
        {
            using (var biometricSvc = GetNewBiometricWsPortTypeClient())
            {
                if (biometricSvc == null) return;
                var data = biometricSvc.getUsersFromDB();
                UsersInfo = JsonConvert.DeserializeObject<List<UserInfo>>(data.data);
            }
        }

        public string GetRemoteAddressWs()
        {
            using (var registry = Registry.LocalMachine)
            {
                var subkey = registry.OpenSubKey(Constants.WS_REGISTRY, false);
                var address = (string)subkey?.GetValue(Constants.WS_SUBKEY) ?? "";
                return address;
            }
        }

        public void SetRemoteAddressWs(string addresss)
        {
            using (var registry = Registry.LocalMachine)
            {
                var subKey = registry.OpenSubKey(Constants.WS_REGISTRY, true) ??
                             registry.CreateSubKey(Constants.WS_REGISTRY);
                subKey?.SetValue(Constants.WS_SUBKEY, addresss, RegistryValueKind.String);
            }
        }


        private BiometricWSPortTypeClient GetNewBiometricWsPortTypeClient()
        {
            var address = GetRemoteAddressWs();
            if (address == null || address.Equals(""))
            {
                MessageBox.Show("Es necesario actualizar la dirección del servicio web", "Error");
                return null;
            }
            var binding = new BasicHttpBinding();
            var remoteAaddress = new EndpointAddress(address);
            return new BiometricWSPortTypeClient(binding, remoteAaddress);
        }
    }
}