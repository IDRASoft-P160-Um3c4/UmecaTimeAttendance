using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace TimeAttendanceService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract, WebGet(UriTemplate = "/")]
        List<UserInfo> GetAllUsers();
    }



}