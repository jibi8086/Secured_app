using Microsoft.AspNetCore.Http;
using SecureAppCommon;
using System.Collections.Generic;

namespace SecureAppServiceInterface
{
    public interface IFileService
    {
        ProcessResult ProcessFile(IFormFile formFile);
        List<FileDetail> GetAllFiles();
        FileDetail GetFileById(int fileId);
    }
}
