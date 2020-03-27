using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PdfEncryption.Models;
using SecureAppCommon;
using SecureAppServiceInterface;

namespace PdfEncryption.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IRegisterService _registerService;
        public LoginController(IConfiguration config ,IRegisterService registerService)
        {
            _config = config;
            _registerService = registerService;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");

        }
        [HttpPost]
        public int EmployeeRegister(Employee employe)
        {
            try
            {
                string connectionString = _config.GetSection("ConnectionStrings").GetSection("Database").Value;
                if (Captcha.ValidateCaptchaCode(employe.Captcha, HttpContext))
                {
                   return _registerService.EmployeeRegister(employe, connectionString);
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                return -3;
                throw;
            }
        }
        [HttpGet]
        public User SignUp(User userInfo)
        {
            try
            {
                string connectionString = _config.GetSection("ConnectionStrings").GetSection("Database").Value;
                int result= _registerService.LoginUser(userInfo, connectionString);
                IActionResult response = Unauthorized();


                if (result == 1)
                {
                    var tokenstring = GenerateJSONWebToken(userInfo);
                    userInfo.Token = tokenstring.ToString();
                    return userInfo;//ok(new envelope<user>(true, "data-fetch-success", userinfo));
                }
                else if (result == -2)
                {
                    userInfo.Blocked = true;
                    return userInfo;
                }
                else {
                    userInfo.ValidUser = false; 
                    userInfo.Blocked = false; 
                    userInfo.Token = "";
                    return userInfo;
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress)
                            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [Authorize]
        public void Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            //if (currentUser.HasClaim(c => c.Email == ""))
            //{
                
            //}

        }

        //public User LoginUser(User userInfo, string connectionString)
        //{
        //    try
        //    {
        //        DataTable dataTable = new DataTable();
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("[dbo].[Sp_Login]", conn))
        //            {
        //                conn.Open();
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userInfo.EmailAddress;
        //                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = userInfo.Passwd;
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                dataTable.Load(reader);
        //                if (dataTable.Rows.Count != 0)
        //                    return userInfo;
        //                else
        //                    return null;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}