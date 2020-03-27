using SecureAppCommon;
using SecureAppRepoInterface;
using SecureAppServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAppService
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepo _registerRepo;
        public RegisterService(IRegisterRepo registerRepo)
        {
            _registerRepo = registerRepo;
        }
        public int EmployeeRegister(Employee employe,string connectionString) 
        {
            int result =_registerRepo.EmployeeRegister(employe, connectionString);
            return result > 0 ? 1 : -1;
        }
        public int LoginUser(User userInfo, string connectionString)
        {
             return _registerRepo.LoginUser(userInfo, connectionString);
            //return result > 0 ? 1 : -1;
        }
    }
}
