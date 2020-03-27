using SecureAppCommon;
using SecureAppRepoInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SecureAppRepo
{
    public class RegisterRepo : IRegisterRepo
    {
        public int EmployeeRegister(Employee employe, string connectionString)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_AddEmployee]", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FullName", SqlDbType.VarChar).Value = employe.FullName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = employe.EmailAddress;
                    cmd.Parameters.Add("@Passwd", SqlDbType.VarChar).Value = employe.Passwd;
                    return (Int32)cmd.ExecuteNonQuery();

                }
            }
        }
        public int LoginUser(User userInfo, string connectionString)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[dbo].[Sp_Login]", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userInfo.EmailAddress;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = userInfo.Passwd;
                        return (Int32)cmd.ExecuteScalar();
                        //SqlDataReader reader = cmd.ExecuteReader();
                        //dataTable.Load(reader);
                        //if (dataTable.Rows.Count != 0)
                        //    return userInfo;
                        //else
                        //    return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
