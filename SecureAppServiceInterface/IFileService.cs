using Microsoft.AspNetCore.Http;
using SecureAppCommon;
using System.Collections.Generic;

namespace SecureAppServiceInterface
{
    public interface IFileService
    {
        bool ProcessFile(IFormFile formFile);
        List<FileDetail> GetAllFiles();
        FileDetail GetFileById(int fileId);
    }
}
