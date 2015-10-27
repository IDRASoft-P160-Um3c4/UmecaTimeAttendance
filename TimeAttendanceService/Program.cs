using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Runtime.Serialization;
using zkemkeeper;

namespace TimeAttendanceService
{
    public class Program
    {
        //Create Standalone SDK class dynamicly.
        private readonly List<UserInfo> _usersList;
        private readonly List<AttendanceLog> _attendanceLogs;

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

        public CZKEMClass Service = new CZKEMClass();


        public class Device
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Ip { get; set; }
            public int Port { get; set; }
        }

        private readonly bool _isConnected;
        private const int MachineNumber = 1; //the serial number of the device.After connecting the device ,this value will be changed.

        [STAThread]
        static void Main(string[] args)
        {
            if (args == null) return;

            var db = new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings["UmecaDb"].ConnectionString);
            db.Open();


            var command = db.CreateCommand();
            command.CommandText = "select * from cat_device where is_obsolete = 0";

            command.CommandType = CommandType.Text;
            var reader = command.ExecuteReader();
            var result = new List<Device>();

            while (reader.Read())
            {
                result.Add(new Device
                {
                    Id = reader.GetInt64("id_device"),
                    Name = reader.GetString("name"),
                    Ip = reader.GetString("Ip"),
                    Port = reader.GetInt32("Port")
                });
            }

            db.Close();
            db.Dispose();
            

            var character = ' ';
            var service = new Program();

            do
            {
                switch (character)
                {
                    case '1': //Read all users
                        service.ReadUsers();

                        Console.Clear();
                        Console.WriteLine("Usuarios encontrados: {0}", service._usersList.Count);
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0,5} {1,-30} {2,-15} {3,-5} {4,-8} {5}", "ID", "Name", "Pass", "Role", "Enabled", "FP");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        foreach (var user in service._usersList)
                        {
                            Console.WriteLine("{0,5} {1,-30} {2,15} {3,5} {4,8} {5}", user.EnrollNumber, user.Name, user.Password, user.Privilege, user.Enabled, user.FingerPrints.Count);
                        }
                        Console.Write("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("Introduzca los siguientes datos:");

                        int enrollNumber, privilege, fingerIndex;
                        string line, name, password;
                        bool enabled;

                        do
                        {
                            Console.Write("ID: ");
                            line = Console.ReadLine();
                            
                        } while (!int.TryParse(line, out enrollNumber));
                        
                        Console.Write("Nombre: ");
                        name = Console.ReadLine();
                        Console.Write("Contraseña: ");
                        password = Console.ReadLine();

                        do
                        {
                            Console.Write("Rol: ");
                            line = Console.ReadLine();    
                        } while (!int.TryParse(line, out privilege));

                        do
                        {
                            Console.Write("Habilitado: ");
                            line = Console.ReadLine();
                        } while (!bool.TryParse(line, out enabled));


                        do
                        {
                            Console.Write("Dedo: ");
                            line = Console.ReadLine();
                        } while (!int.TryParse(line, out fingerIndex) || fingerIndex < 0 || fingerIndex >= 10);


                        service.Write(enrollNumber, name, password, privilege, enabled, fingerIndex);

                        Console.Write("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("Introduzca los siguientes datos:");

                        do
                        {
                            Console.Write("ID: ");
                            line = Console.ReadLine();

                        } while (!int.TryParse(line, out enrollNumber));

                        do
                        {
                            Console.Write("Dedo: ");
                            line = Console.ReadLine();
                        } while (!int.TryParse(line, out fingerIndex) || fingerIndex < 0 || fingerIndex >= 10);


                        service.Enroll(enrollNumber, fingerIndex);

                        Console.Write("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case '4':
                        service.ReadLogs();

                        Console.Clear();
                        Console.WriteLine("Logs encontrados: {0}", service._attendanceLogs.Count);
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0,-10} {1,10} {2,15}", "ID", "Event", "Date");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        foreach (var user in service._attendanceLogs)
                        {
                            Console.WriteLine("{0,-10} {1,10} {2,15}", user.EnrollNumber, user.InOutMode == 4 ? "In" : (user.InOutMode == 5 ? "Out" : user.InOutMode.ToString()), user.Date);
                        }
                        Console.Write("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                    case 'T':
                        service.SetDateTime();
                        break;
                    case 'C':
                        service.Clear();
                        break;
                    case 'E':
                        service.Events();
                        break;
                }
                Menu();

            } while ((character = Console.ReadKey().KeyChar) != 'x');


            
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1. Leer todos los usuarios");
            Console.WriteLine("2. Agregar o modificar usuario");
            Console.WriteLine("3. Agregar huella digital a usuario");
            Console.WriteLine("4. Recuperar registros de asistencia");
            Console.WriteLine();
            Console.WriteLine("X. Salir");
            Console.WriteLine();
            Console.Write("Introduzca su opción: ");
        }

        public Program()
        {
            _usersList = new List<UserInfo>();
            _attendanceLogs = new List<AttendanceLog>();

            _isConnected = false;

            var ip = "192.168.1.111";
            int port = 4370;

            _isConnected = Service.Connect_Net(ip, port);

            if (!_isConnected)
            {
                ViewError();
            }
        }


        public void Events()
        {
            if (Service.RegEvent(MachineNumber, 65535))
            {
                Service.OnFinger += () =>
                {
                    Console.WriteLine("OnFinger");
                };
            }
        }

        private void ViewError()
        {
            var idwErrorCode = 0;
            Service.GetLastError(ref idwErrorCode);
            string value;
            Console.WriteLine(_messageError.TryGetValue(idwErrorCode, out value) ? value : "Unknow error" + idwErrorCode);
        }

        public void ReadUsers()
        {
            if (!_isConnected) return;

            _usersList.Clear();
                
            Service.EnableDevice(MachineNumber, false);
            Service.ReadAllUserID(MachineNumber);

            String enrollNumber, name, password, tmpData;
            int privilege, flag, tmpLength;
            bool enabled;

            while (Service.SSR_GetAllUserInfo(MachineNumber, out enrollNumber, out name, out password, out privilege, out enabled))
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
                _usersList.Add(user);

                for (var idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (Service.GetUserTmpExStr(MachineNumber, enrollNumber, idwFingerIndex, out flag, out tmpData, out tmpLength))//get the corresponding templates string and length from the memory
                    {
                        user.FingerPrints.Add(new FingerPrint{Finger = idwFingerIndex, Data = tmpData});
                    }
                }
            }


            Service.ReadAllUserID(MachineNumber);//read all the user information to the memory
            Service.ReadAllTemplate(MachineNumber);//read all the users' fingerprint templates to the memory

            Service.EnableDevice(MachineNumber, true);
        }

        public void Write(int enrollNumber, string name, string password, int privilege, bool enabled, int fingerIndex)
        {
            const int updateFlag = 1;

            if (!_isConnected) return;

            Service.EnableDevice(MachineNumber, false);

            if (Service.BeginBatchUpdate(MachineNumber, updateFlag))
            {
                var result = Service.SSR_SetUserInfo(MachineNumber, enrollNumber.ToString(), name, null, privilege, enabled);

                if (result)
                {
                    if (!Service.BatchUpdate(MachineNumber))
                        ViewError();
                }
                else
                    ViewError();
            }

            Service.RefreshData(MachineNumber);//the data in the device should be refreshed
            Service.EnableDevice(MachineNumber, true);
        }


        public void Enroll(int enrollNumber, int fingerIndex)
        {
            //Enroll
            Service.CancelOperation();
            Service.DelUserTmp(MachineNumber, enrollNumber, fingerIndex);

            if (Service.StartEnrollEx(enrollNumber.ToString(), fingerIndex, 1))
            {
                Service.StartIdentify();//After enrolling templates,you should let the device into the 1:N verification condition
            }
            else
            {
                ViewError();
            }
        }

        public void ReadLogs()
        {
            string enrollNumber;
            int verifyMode, InOutMode, year, month, day, hour, minute, second, workcode = 0;
            _attendanceLogs.Clear();

            Service.EnableDevice(MachineNumber, false);//disable the device
            if (Service.ReadGeneralLogData(MachineNumber))//read all the attendance records to the memory
            {
                while (Service.SSR_GetGeneralLogData(MachineNumber, out enrollNumber, out verifyMode,
                           out InOutMode, out year, out month, out day, out hour, out minute, out second, ref workcode))//get records from the memory
                {
                    _attendanceLogs.Add(new AttendanceLog{EnrollNumber = enrollNumber, Date = new DateTime(year, month, day, hour, minute, second), InOutMode = InOutMode, VerifyMode = verifyMode, WorkCode = workcode});
                }
            }
            else
            {
                ViewError();
            }
            Service.EnableDevice(MachineNumber, true);//enable the device
        }

        public void SetDateTime()
        {
            if (Service.SetDeviceTime2(MachineNumber, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))
            {
                Service.RefreshData(MachineNumber);//the data in the device should be refreshed
            }
            else
            {
                ViewError();
            }
        }

        public void Clear()
        {
            Service.EnableDevice(MachineNumber, false);//disable the device
            if (Service.ClearGLog(MachineNumber))
            {
                Service.RefreshData(MachineNumber);//the data in the device should be refreshed
            }
            else
            {
                ViewError();
            }
            Service.EnableDevice(MachineNumber, true);//enable the device
        }
    }


    [DataContract]
    public struct FingerPrint
    {
        public int Finger { get; set; }
        public string Data { get; set; }
    }

    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public string EnrollNumber { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int Privilege { get; set; }
        [DataMember]
        public bool Enabled { get; set; }
        public List<FingerPrint> FingerPrints { get; set; }
    }

    [DataContract]
    public class AttendanceLog
    {
        [DataMember]
        public string EnrollNumber { get; set; }
        [DataMember]
        public int VerifyMode { get; set; }
        [DataMember]
        public int InOutMode { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public int WorkCode { get; set; }
    }
}