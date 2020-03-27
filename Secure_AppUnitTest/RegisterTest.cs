using PdfEncryption.Models;
using SecureAppCommon;
using SecureAppRepo;
using SecureAppRepoInterface;
using SecureAppService;
using SecureAppServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Secure_AppUnitTest
{
    public class RegisterTest
    {
       // protected IRegisterService _registerService;
       //// private IRegisterService registerService;
       // public RegisterTest(IRegisterService registerService)
       // {
       //     _registerService = registerService;
       // }
        [Fact]
        public void RegisterEmployee()
        {
            //Arrange
                int expected = 1;
                Employee emp = new Employee();
                emp.FullName = "test";
                emp.EmailAddress = "test@gmail.com";
                emp.Passwd = "test";
            //Act
                RegisterRepo reg = new RegisterRepo();
                int actual= reg.EmployeeRegister(emp, "Data Source=DESKTOP-K0VSFLK\\SQLEXPRESS;Initial Catalog=SendSecurely;Integrated Security=SSPI;;");
            //Assert
                Assert.Equal(expected, actual);

        }
        [Fact]
        public void Login()
        {
            //Arrange
                int expected = 1;
                User us = new User();
                us.EmailAddress = "user";
                us.Passwd = "123";
            //ACT
                RegisterRepo reg = new RegisterRepo();
                int actual = reg.LoginUser(us, "Data Source=DESKTOP-K0VSFLK\\SQLEXPRESS;Initial Catalog=SendSecurely;Integrated Security=SSPI;;");
            //Assert
                 Assert.Equal(expected, actual);
        }
    }
}
