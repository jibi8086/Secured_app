using SecureAppCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAppRepoInterface
{
    public interface IFileRepo
    {
        bool SaveFileDetail(FileDetail fileDetail);
        List<FileDetail> GetFileDetails();
    }
}
