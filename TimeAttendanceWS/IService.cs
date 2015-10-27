using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TimeAttendanceWS
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract, WebGet(UriTemplate = "/")]
        List<UserInfo> GetAllUsers();
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
