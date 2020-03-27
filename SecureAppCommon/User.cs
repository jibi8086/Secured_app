using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAppCommon
{
    public class User
    {
        public int ID { get; set; }
        public bool Blocked { get; set; }
        public string EmailAddress { get; set; }
        public string Passwd { get; set; }
        public string Token { get; set; }
        public bool ValidUser { get; set; }
    }
}
