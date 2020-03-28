using System.Collections.Generic;

namespace SecureAppCommon
{
    public class ProcessResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ProcessResult> processResults { get; set; } = new List<ProcessResult>();
    }
}
