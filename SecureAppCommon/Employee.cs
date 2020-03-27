using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAppCommon
{
    public class Employee
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Passwd { get; set; }
        public string Token { get; set; }
        public string Captcha { get; set; }
    }
}
