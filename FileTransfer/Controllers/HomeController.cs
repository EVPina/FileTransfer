using FileTransfer.Data;
using FileTransfer.Models;
using FileTransfer.Models.Entities;
using FileTransfer.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.Controllers
{
    public class Prueba
    {
        public string name { get; set; }
    }
    public class HomeController : Controller
    {
        FileTransferContext _context;

        public HomeController(FileTransferContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Archivos()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Archivos([FromBody] List<VMFilesUser> files)
        {
            List<VMFilesUser> copia_files = new List<VMFilesUser>();
            copia_files.AddRange(files);
            foreach (var file in files)
            {
                try
                {
                    FilesUser filesUser = new FilesUser();
                    filesUser.IdFileUser = System.Guid.NewGuid().ToString().Substring(20);
                    filesUser.NameFile = file.NameFile;
                    filesUser.SizeFile = file.SizeFile;
                    filesUser.DateUpload = file.DateUpload;
                    filesUser.IdUser = file.IdUser;

                    var check = await _context.FilesUsers.AddAsync(filesUser);
                    if (check.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                    {
                        await _context.SaveChangesAsync();
                    }
                    copia_files.Remove(file);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, files = copia_files });
                }
            }
            return Json(new { success = true, files = copia_files });
        }
    }
}
