using System.Collections.Generic;
using System.ServiceModel;

namespace TimeAttendanceService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service : IService
    {
        public List<UserInfo> GetAllUsers()
        {
            return null;
        }
    }
}