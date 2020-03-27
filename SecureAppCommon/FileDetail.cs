using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAppCommon
{
    public class FileDetail
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FilePassword { get; set; }
    }
}
