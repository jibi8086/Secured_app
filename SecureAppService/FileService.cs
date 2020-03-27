using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PdfEncryption.Utilities;
using SecureAppCommon;
using SecureAppRepoInterface;
using SecureAppServiceInterface;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.IO;

namespace SecureAppService
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileRepo _fileDetailRepo;

        public FileService(IHostingEnvironment hostingEnvironment, IFileRepo fileDetailRepo)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileDetailRepo = fileDetailRepo;
        }
        public bool ProcessFile(IFormFile formFile)
        {
            FileDetail fileDetail = MapToFileDetails(formFile);
            EncryptDocument(formFile, fileDetail);
            return SaveFileDetailToDatabase(fileDetail);
        }

        #region Private Methods
        private FileDetail MapToFileDetails(IFormFile uploadedFile)
        {
            return uploadedFile != null ? new FileDetail
            {
                FileName = $"{ Math.Abs(Guid.NewGuid().GetHashCode())} {uploadedFile.FileName}",
                FilePath = $@"{_hostingEnvironment.WebRootPath}\AppData\",
                FilePassword = RandomNumberGenerator.RandomPassword()
            } : throw new ArgumentNullException(nameof(uploadedFile));
        }

        private bool EncryptDocument(IFormFile uploadedFile, FileDetail fileDetail)
        {
            try
            {
                PdfLoadedDocument document = new PdfLoadedDocument(uploadedFile.OpenReadStream());
                //Create a document security.
                PdfSecurity security = document.Security;
                //Set user password for the document.
                security.UserPassword = fileDetail.FilePassword;
                //Set encryption algorithm.
                security.Algorithm = PdfEncryptionAlgorithm.AES;
                //Set key size.
                security.KeySize = PdfEncryptionKeySize.Key256Bit;

                using (FileStream outputFileStream = new FileStream($"{fileDetail.FilePath}{fileDetail.FileName}", FileMode.Create))
                {
                    document.Save(outputFileStream);
                    document.Close(true);
                }
                return true;
            }
            catch (Exception ex )
            {
                return false;
            }
        }
        private bool SaveFileDetailToDatabase(FileDetail fileDetail) 
        {
            return _fileDetailRepo.SaveFileDetail(fileDetail);
        }
        #endregion
    }
}
