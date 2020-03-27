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

        public List<FileDetail> GetFileDetails()
        {
            throw new NotImplementedException();
        }

        public bool SaveFileDetail(FileDetail fileDetail)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Sp_InsertFiledetails]", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = 1;//todo change this to logged in userd Id
                        cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = fileDetail.FileName;
                        cmd.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = fileDetail.FilePath;
                        cmd.Parameters.Add("@FilePassword", SqlDbType.VarChar).Value = fileDetail.FilePassword;
                        return (cmd.ExecuteNonQuery() > 1) ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
