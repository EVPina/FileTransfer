using FileTransfer.Data;
using FileTransfer.Models;
using FileTransfer.Models.Entities;
using FileTransfer.ViewsModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.Controllers
{
    public class HomeController : Controller
    {
        FileTransferContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public HomeController(FileTransferContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Archivos()
        {
            return View();
        }

        //Guardar en wwwroot
        [HttpPost]
        public async Task<IActionResult> MoverArchivos([FromQuery] string iduser)
        {
            var files = Request.Form.Files;
            _webHostEnvironment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var copia_files = new List<IFormFile>();
            copia_files.AddRange(files.ToList());
           
            foreach (var file in files)
            {
                string filename = file.FileName;
                string filesize = null;
                filesize = SizeSuffix(file.Length);
                string dateupload = DateTime.UtcNow.ToString("dd/MM/yyyy");
                string rootfile = Path.Combine(_webHostEnvironment.WebRootPath + @"\archivos", file.FileName);
                try
                {
                    using (var stream = new FileStream(rootfile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    bool check = await GuardarArchivos(filename, filesize, dateupload, iduser);
                    if(check)
                        copia_files.Remove(file);
                    else
                    {
                        System.IO.File.Delete(rootfile);
                        return Json(new { success = false, files = copia_files });
                    }

                }
                catch
                {
                    if (System.IO.File.Exists(rootfile))
                        System.IO.File.Delete(rootfile);
                    return Json(new { success = false, files = copia_files });                    
                }
            }
            return Json(new { success = true, files = copia_files });
        }

        //Guarda en Base de Datos
        private async Task<bool> GuardarArchivos(string filename, string filesize, string dateupload, string iduser)
        {
            try
            {
                FilesUser filesUser = new FilesUser();
                filesUser.IdFileUser = System.Guid.NewGuid().ToString().Substring(20);
                filesUser.NameFile = filename;
                filesUser.SizeFile = filesize;
                filesUser.DateUpload = dateupload;
                filesUser.IdUser = iduser;

                var check = await _context.FilesUsers.AddAsync(filesUser);
                if (check.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                {
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                var lastfile = await _context.FilesUsers.Where(c => c.IdUser == iduser & c.NameFile == filename).FirstOrDefaultAsync();
                if (lastfile != null)
                {
                    _context.FilesUsers.Remove(lastfile);
                    await _context.SaveChangesAsync();
                }
                return false;
            }
            return true;
        }

        static readonly string[] SizeSuffixes =
                  { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            if (value < 1000)
            {
                return string.Format("{0:n" + decimalPlaces + "} {1}", 1, "KB");
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }
    }
}
