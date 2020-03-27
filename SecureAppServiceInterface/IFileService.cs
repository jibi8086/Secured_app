using Microsoft.AspNetCore.Http;
using SecureAppCommon;

namespace SecureAppServiceInterface
{
    public interface IFileService
    {
        bool ProcessFile(IFormFile formFile);
    }
}
