using FileTransfer.Data;
using FileTransfer.Models.Entities;
using FileTransfer.ViewsModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.Controllers
{
    public class UserController : Controller
    {
        FileTransferContext _context;
        IWebHostEnvironment _webHostEnvironment;
        UserManager<Usuario> _userManager;
        public UserController(FileTransferContext context, UserManager<Usuario> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Cuenta()
        {
            return View();
        }
        
        public IActionResult ChangePassword()
        {
            return View();
        }
        
        public async Task<IActionResult> MisArchivos(string iduser)
        {
           List<FilesUser> filesUsers = await  _context.FilesUsers.Where(c => c.IdUser == iduser).ToListAsync();
            return View(filesUsers);
        }

        public FileResult DownloadFile(string iduser,string filename)
        {
            string root = Path.Combine(_webHostEnvironment.WebRootPath + @"\archivos", iduser);
            string rootfile = Path.Combine(root, filename);
            if (System.IO.File.Exists(rootfile))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(rootfile);
                return File(bytes, "application/octet-stream", filename);
            }
            return null;
        }

        [HttpPost]
        public async Task<bool> DeleteFile(string iduser,string filename)
        {
            try
            {

                string root = Path.Combine(_webHostEnvironment.WebRootPath + @"\archivos", iduser);
                string rootfile = Path.Combine(root, filename);
                if (System.IO.File.Exists(rootfile))
                    System.IO.File.Delete(rootfile);
                FilesUser actualfile = await _context.FilesUsers.FirstOrDefaultAsync(c => c.IdUser == iduser & c.NameFile == filename);
                if (actualfile.NameFile != null)
                {
                    _context.Remove(actualfile);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        public async Task<string> GetUserImage(string id)
        {
            var image_user = await _context.Users.Where(a => a.Id == id).Select(c => c.ImageUser).FirstOrDefaultAsync();
            return image_user;
        }
        
        [HttpPost]
        public async Task<Usuario> GetUserData(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        [HttpPost]
        public IActionResult OptionsProfile(string ValueOpcion)
        {
            switch (ValueOpcion)
            {
                case "1":
                    return View(nameof(Cuenta));
                case "2":
                    return View(nameof(ChangePassword));
                default:
                    return View(nameof(Profile));
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendPassword(VMPassword vMPassword)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario =  await _userManager.FindByIdAsync(vMPassword.IdUser);
                var pass = vMPassword.Password;

                if (pass != null)
                    await _userManager.ChangePasswordAsync(usuario, vMPassword.OldPassword, pass);
            }
            return View(nameof(ChangePassword), vMPassword);
        }
        [HttpPost]
        public async Task<IActionResult> SendCuenta(VMCuenta vMCuenta)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario =  await _userManager.FindByIdAsync(vMCuenta.IdUser);
                var correo = vMCuenta.Correo;
                var imagen = vMCuenta.ImagenUsuario;
                if (imagen != null)
                {
                    string imagename = imagen.FileName;
                    string imagen64 = "";
                    using (var ms = new MemoryStream())
                    {
                        await imagen.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        imagen64 = Convert.ToBase64String(fileBytes);
                    }
                    string nombreimagen = $"data:image/{imagename.Substring(imagename.IndexOf(".") + 1)};base64,{imagen64}";
                    usuario.ImageUser = nombreimagen;
                }
                if (correo != null)
                    usuario.Email = correo;

                await _userManager.UpdateAsync(usuario);
            }
            return View(nameof(Cuenta), vMCuenta);
        }


        [HttpPost]
        public async Task<IActionResult> SendProfile(VMProfile vMProfile,string id)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario =  await _userManager.FindByIdAsync(vMProfile.IdUser);
                string nombre = vMProfile.Nombre;
                string celular = vMProfile.Celular;

                if (nombre != null)
                    usuario.UserName = nombre;

                if (celular != null)
                    usuario.PhoneNumber = celular;

                await _userManager.UpdateAsync(usuario);
            }
            return RedirectToAction(nameof(Profile),vMProfile);
        }
    }
}
