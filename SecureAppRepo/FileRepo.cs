using Microsoft.Extensions.Configuration;
using SecureAppCommon;
using SecureAppRepoInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SecureAppRepo
{
    public class FileRepo : IFileRepo
    {
        private string _connectionString;
        public FileRepo(IConfiguration config) => _connectionString = config.GetSection("ConnectionStrings").GetSection("Database").Value;

        public FileDetail GetFileById(int fileId)
        {
            FileDetail fileDetail = new FileDetail();
            using (SqlConnection conn=new SqlConnection(_connectionString))
            {
                using (SqlDataAdapter adapter=new SqlDataAdapter($"select * from FileDetail where ID={fileId}",conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        fileDetail.ID = Convert.ToInt32(dataRow["ID"]);
                        fileDetail.UserId = Convert.ToInt32(dataRow["UserId"]);
                        fileDetail.FileName = dataRow["FileName"].ToString();
                        fileDetail.FilePath = dataRow["FilePath"].ToString();
                        fileDetail.FilePassword = dataRow["FilePassword"].ToString();
                        fileDetail.UploadedDate = Convert.ToDateTime(dataRow["CreatedDate"]);
                    }
                }
            }
            return fileDetail;
        }

        public List<FileDetail> GetFileDetails()
        {
            List<FileDetail> fileDetails = new List<FileDetail>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("select * from [dbo].[FileDetail]", conn))
                {
                    FileDetail fileDetail = null;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        fileDetail= new FileDetail();
                        fileDetail.ID = Convert.ToInt32(dataRow["ID"]);
                        fileDetail.UserId = Convert.ToInt32(dataRow["UserId"]); 
                        fileDetail.FileName = dataRow["FileName"].ToString();
                        fileDetail.FilePath = dataRow["FilePath"].ToString();
                        fileDetail.FilePassword = dataRow["FilePassword"].ToString();
                        fileDetail.UploadedDate= Convert.ToDateTime(dataRow["CreatedDate"]);
                        fileDetails.Add(fileDetail);
                    }
                }
            }
            return fileDetails;
        }

        public bool SaveFileDetail(FileDetail fileDetail, ProcessResult processResult)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Sp_InsertFiledetails]", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = 1;//antonytodo change this to logged in userd Id
                        cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = fileDetail.FileName;
                        cmd.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = fileDetail.FilePath;
                        cmd.Parameters.Add("@FilePassword", SqlDbType.VarChar).Value = fileDetail.FilePassword;
                        processResult.processResults.Add(
                            new ProcessResult { IsSuccess = true, Message = "File details Successfully saved to DB" });
                        return (cmd.ExecuteNonQuery() > 1) ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                processResult.processResults.Add(
                    new ProcessResult { IsSuccess = true, Message = ex.Message });
                return false;
            }
        }


    }
}
