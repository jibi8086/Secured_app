using SecureAppCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAppRepoInterface
{
    public interface IRegisterRepo
    {
         int EmployeeRegister(Employee employe,string connectionString);
         int LoginUser(User userInfo, string connectionString);
    }
}
