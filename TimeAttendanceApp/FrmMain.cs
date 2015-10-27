﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using zkemkeeper;

namespace TimeAttendanceApp
{
    public partial class FrmMain : Form
    {

        private readonly Service _service = new Service();
        private void BeginWork()
        {
            btnSyncUsers.Enabled = btnEnroll.Enabled = btnAssistence.Enabled = false;
        }

        private void EndWork()
        {
            btnSyncUsers.Enabled = btnEnroll.Enabled = btnAssistence.Enabled = true;
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnSyncUsers_Click(object sender, EventArgs e)
        {
            try
            {
                BeginWork();
                

                //foreach (var device in _devices)
                //{
                //    var service = new Service(device);

                //    if (service.IsConnected)
                //        service.ReadUsers(ref _usersInfo);
                //}
            }
            finally
            {
                EndWork();
            }
        }

        private void btnAssistence_Click(object sender, EventArgs e)
        {
            try
            {
                BeginWork();

                //GetDevices();

                //foreach (var device in _devices)
                //{
                //    var service = new Service(device);

                //    if (service.IsConnected)
                //        service.ReadLog(ref _attendanceLogs);
                //}
            }
            finally
            {
                EndWork();
            }           
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

        private void btnUsers_Click(object sender, EventArgs e)
        {
            var frmFingerPrints = new FrmFingerPrints(_service);
            frmFingerPrints.ShowDialog(this);
        }
    }

    public class Device
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
    }

    public class AttendanceLog
    {
        public string EnrollNumber { get; set; }
        public int VerifyMode { get; set; }
        public int InOutMode { get; set; }
        public DateTime Date { get; set; }
        public int WorkCode { get; set; }
    }

    public class UserInfo
    {
        public string EnrollNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Privilege { get; set; }
        public bool Enabled { get; set; }
        public List<FingerPrint> FingerPrints { get; set; }
    }

    public class FingerPrint
    {
        public int Finger { get; set; }
        public string Data { get; set; }
    }

    public delegate void EnrollEventHandler(object sender, EnrollEventArgs e);

    public class EnrollEventArgs : EventArgs
    {
        public bool Result { get; }

        public int Finger { get; }

        public string FingerPrint { get; }

        public EnrollEventArgs(bool result, int finger, string fingerPrint)
        {
            Result = result;
            Finger = finger;
            FingerPrint = fingerPrint;
        }
    }

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


        private readonly CZKEMClass _service = new CZKEMClass();
        private const int MachineNumber = 1; //the serial number of the device.After connecting the device ,this value will be changed.
        private List<Device> _devices;
        private readonly string _connectionString;
        public bool IsConnected { get; set; }
        public List<UserInfo> UsersInfo { get; private set; } = new List<UserInfo>();

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
        public Service(Device device) :this()
        {
            
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

        public void ReadLog(ref List<AttendanceLog> attendanceLogs )
        {
            attendanceLogs.Clear();

            _service.EnableDevice(MachineNumber, false);//disable the device
            if (_service.ReadGeneralLogData(MachineNumber))//read all the attendance records to the memory
            {
                string enrollNumber;
                int verifyMode, inOutMode, year, month, day, hour, minute, second, workcode = 0;
                while (_service.SSR_GetGeneralLogData(MachineNumber, out enrollNumber, out verifyMode,
                           out inOutMode, out year, out month, out day, out hour, out minute, out second, ref workcode))//get records from the memory
                {
                    attendanceLogs.Add(new AttendanceLog { EnrollNumber = enrollNumber, Date = new DateTime(year, month, day, hour, minute, second), InOutMode = inOutMode, VerifyMode = verifyMode, WorkCode = workcode });
                }
            }
            else
            {
                ViewError();
            }
            _service.EnableDevice(MachineNumber, true);//enable the device
        }

        public void Enroll(long deviceId, string userId, int finger)
        {
            var device = Devices.FirstOrDefault(d => d.Id == deviceId);
            if (device == null)
                return;
            
            Connect(device);

            _service.CancelOperation();
            _service.SSR_DelUserTmpExt(MachineNumber, userId, finger);//If the specified index of user's templates has existed ,delete it first.(SSR_DelUserTmp is also available sometimes)

            _service.OnEnrollFinger += (number, index, result, length) =>
            {
                int flag, tmpLength;
                string tmpData;
                OnEnrollCompleted(_service.GetUserTmpExStr(MachineNumber, userId, finger, out flag, out tmpData,
                    out tmpLength)
                    ? new EnrollEventArgs(true, finger, tmpData)
                    : new EnrollEventArgs(false, -1, null));

                var userInfo = UsersInfo.FirstOrDefault(u => u.EnrollNumber == userId);
                if (userInfo == null)
                    return;
                var fingerPrint = userInfo.FingerPrints.FirstOrDefault(f => f.Finger == finger);
                if (fingerPrint == null)
                    userInfo.FingerPrints.Add(new FingerPrint {Finger = finger, Data = tmpData});
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
    }
}
