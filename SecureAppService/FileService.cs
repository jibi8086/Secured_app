using ByteSizeLib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PdfEncryption.Utilities;
using SecureAppCommon;
using SecureAppRepoInterface;
using SecureAppServiceInterface;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.Collections.Generic;
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
        public ProcessResult ProcessFile(IFormFile formFile)
        {
            ProcessResult processResult = CheckFileSize(formFile);
            if (!processResult.IsSuccess)
                return processResult;
            FileDetail fileDetail = MapToFileDetails(formFile);
            EncryptDocument(formFile, fileDetail,processResult);
            SaveFileDetailToDatabase(fileDetail,processResult);
            return processResult;
        }

        private ProcessResult CheckFileSize(IFormFile formFile)
        {
            ByteSize maxFileSize = ByteSize.FromMegaBytes(4);
            return maxFileSize.Bytes < formFile.Length ?
            new ProcessResult { IsSuccess = false, Message = "File size should be less than 4MB" } 
            : new ProcessResult { IsSuccess = true };
        }

        public List<FileDetail> GetAllFiles()
        {
            return _fileDetailRepo.GetFileDetails();
        }
        public FileDetail GetFileById(int fileId)
        {
            return _fileDetailRepo.GetFileById(fileId);
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

        private bool EncryptDocument(IFormFile uploadedFile, FileDetail fileDetail, ProcessResult processResult)
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

                processResult.processResults.Add(
                    new ProcessResult { IsSuccess = true, Message = "File Successfully saved to local filesystem" });
                return true;
            }
            catch (Exception ex)
            {
                processResult.processResults.Add(
                    new ProcessResult { IsSuccess = false, Message = ex.Message });
                return false;
            }
        }
        private bool SaveFileDetailToDatabase(FileDetail fileDetail, ProcessResult processResult)
        {
            return _fileDetailRepo.SaveFileDetail(fileDetail, processResult);
        }

        #endregion
    }
}
