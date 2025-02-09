﻿using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfEncryption.Models;
using SecureAppCommon;
using SecureAppServiceInterface;

namespace PdfEncryption.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService _fileService;

        public HomeController(IFileService fileService)
        {
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            List<FileDetail> fileDetails= _fileService.GetAllFiles();
            return View(fileDetails);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(IFormFile uploadedFile)
        {
            ProcessResult processResult= _fileService.ProcessFile(uploadedFile);
            ViewBag.ProcessResult = processResult;
            return View();
        }
        [HttpGet]
        public FileDetail GetFileDetail(int fileId)
        {
            return _fileService.GetFileById(fileId);
        }
    }
}
