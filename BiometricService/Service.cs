using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using zkemkeeper;

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
            using (var db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                db.Open();

                var command = db.CreateCommand();
                command.CommandText = "select * from cat_device where is_obsolete = 0";

                command.CommandType = CommandType.Text;
                var reader = command.ExecuteReader();
                _devices = new List<Device>();

                while (reader.Read())
                {
                    _devices.Add(new Device
                    {
                        Id = reader.GetInt64("id_device"),
                        Name = reader.GetString("name"),
                        Ip = reader.GetString("Ip"),
                        Port = reader.GetInt32("Port")
                    });
                }
            }
        }
        private bool Connect(Device device)
        {
            IsConnected = _service.Connect_Net(device.Ip, device.Port);

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
                using (var db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
                {
                    db.Open();

                    var command = db.CreateCommand();

                    var strCommand = new StringBuilder("insert ignore into attendancelog(eventtime, workcode, id_employee) values ");

                    foreach (var attendanceLog in attendanceLogs)
                    {
                        strCommand.AppendFormat("('{0}', {1}, {2}),", attendanceLog.Date.ToString("s"), attendanceLog.WorkCode, attendanceLog.EnrollNumber);
                    }

                    command.CommandText = strCommand.ToString(0, strCommand.Length - 1);

                    command.CommandType = CommandType.Text;
                    var n = command.ExecuteNonQuery();
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

            if (operation == FingerPrintOperation.Update)
            {
                using (var db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
                {
                    db.Open();

                    var command = db.CreateCommand();
                    command.CommandText =
                        string.Format(
                            "delete from employee_fingerprint where id_employee = {0} and finger = {1}; insert into employee_fingerprint(id_employee, finger, fingerprint) values({0}, {1}, '{2}');",
                            enrollNumber, finger, fingerPrint);

                    command.CommandType = CommandType.Text;
                    n = command.ExecuteNonQuery();
                }
            }
            else
            {
                using (var db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
                {
                    db.Open();

                    var command = db.CreateCommand();
                    command.CommandText = string.Format("delete from employee_fingerprint where id_employee = {0} and finger = {1};", enrollNumber, finger);

                    command.CommandType = CommandType.Text;
                    n = command.ExecuteNonQuery();
                }
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

        public void UpdateImputedFingerPrint(long user, string enrollNumber, int finger, string fingerPrint, FingerPrintOperation operation, long deviceId)
        {
            var name = UsersInfo.FirstOrDefault(u => u.EnrollNumber == enrollNumber)?.Name ?? "";
            var n = 0;

            if (operation == FingerPrintOperation.Update)
            {
                using (var db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
                {
                    db.Open();

                    var command = db.CreateCommand();
                    command.CommandText =
                        string.Format(
                            "delete from imputed_fingerprint where id_imputed = {0} and finger = {1}; insert into imputed_fingerprint(id_imputed, finger, data, id_user, timestamp) values({0}, {1}, '{2}', {3}, current_timestamp());",
                            enrollNumber, finger, fingerPrint, user);

                    command.CommandType = CommandType.Text;

                    n = command.ExecuteNonQuery();
                }
            }
            else
            {
                using (var db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
                {
                    db.Open();

                    var command = db.CreateCommand();
                    command.CommandText = string.Format("delete from imputed_fingerprint where id_imputed = {0} and finger = {1};", enrollNumber, finger);

                    command.CommandType = CommandType.Text;
                    n = command.ExecuteNonQuery();
                }
            }

            n = 0;
            var device = Devices.FirstOrDefault(d => d.Id == deviceId);

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
        public void Enroll(long deviceId, string userId, int finger)
        {
            var device = Devices.FirstOrDefault(d => d.Id == deviceId);
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
            using (var db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                db.Open();

                var command = db.CreateCommand();
                command.CommandText = "select id_employee, concat(name, ' ', last_name_p, ' ', last_name_m) name from employee where is_obsolete = 0";

                command.CommandType = CommandType.Text;
                var reader = command.ExecuteReader();
                UsersInfo = new List<UserInfo>();

                while (reader.Read())
                {
                    UsersInfo.Add(new UserInfo
                    {
                        EnrollNumber = reader.GetInt64("id_employee").ToString(),
                        Name = reader.GetString("name")
                    });
                }

                reader.Close();

                foreach (var userInfo in UsersInfo)
                {
                    var commandfp = db.CreateCommand();
                    commandfp.CommandText = string.Format("select id_employee_fingerprint, id_employee, finger, fingerprint from employee_fingerprint where id_employee = {0}", userInfo.EnrollNumber);
                    commandfp.CommandType = CommandType.Text;
                    var readerfp = commandfp.ExecuteReader();
                    var fingerPrints = new List<FingerPrint>();

                    while (readerfp.Read())
                    {
                        fingerPrints.Add(new FingerPrint
                        {
                            Finger = readerfp.GetInt32("finger"),
                            Data = readerfp.GetString("fingerprint")
                        });
                    }

                    userInfo.FingerPrints = fingerPrints;

                    readerfp.Close();
                }
            }
        }

        public void GetImputedFromDb(long idImputed)
        {
            using (var db = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
            {
                db.Open();

                var command = db.CreateCommand();
                command.CommandText = string.Format("select id_imputed, concat(name, ' ', lastname_p, ' ', lastname_m) name from imputed where id_imputed = {0}", idImputed);

                command.CommandType = CommandType.Text;
                var reader = command.ExecuteReader();


                while (reader.Read())
                {
                    ImputedInfo = new UserInfo
                    {
                        EnrollNumber = reader.GetInt64("id_imputed").ToString(),
                        Name = reader.GetString("name")
                    };
                }

                reader.Close();


                var commandfp = db.CreateCommand();
                commandfp.CommandText = string.Format("select id_fingerprint, id_imputed, finger, data from imputed_fingerprint where id_imputed = {0}", ImputedInfo.EnrollNumber);
                commandfp.CommandType = CommandType.Text;
                var readerfp = commandfp.ExecuteReader();
                var fingerPrints = new List<FingerPrint>();

                while (readerfp.Read())
                {
                    fingerPrints.Add(new FingerPrint
                    {
                        Finger = readerfp.GetInt32("finger"),
                        Data = readerfp.GetString("data")
                    });
                }

                ImputedInfo.FingerPrints = fingerPrints;

                readerfp.Close();

            }
        }
    }
}
