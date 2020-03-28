using SecureAppCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAppRepoInterface
{
    public interface IFileRepo
    {
        bool SaveFileDetail(FileDetail fileDetail, ProcessResult processResult);
        List<FileDetail> GetFileDetails();
        FileDetail GetFileById(int fileId);
    }
}
