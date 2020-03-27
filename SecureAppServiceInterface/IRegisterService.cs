
using SecureAppCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAppServiceInterface
{
    public interface IRegisterService
    {
        int EmployeeRegister(Employee employe,string connectionString);
        int LoginUser(User userInfo, string connectionString);
        //Captcha.CaptchaResult GetCaptcha();
    }
}
    