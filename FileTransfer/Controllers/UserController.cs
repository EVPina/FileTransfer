using FileTransfer.Data;
using FileTransfer.Models.Entities;
using FileTransfer.ViewsModels;
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
        UserManager<Usuario> _userManager;
        public UserController(FileTransferContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Cuenta(VMCuenta vMCuenta)
        {
            return View(vMCuenta);
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
            if(ValueOpcion == "1")
                return View(nameof(Cuenta));
            return View(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> SendCuenta(VMCuenta vMCuenta)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario =  await _userManager.FindByIdAsync(vMCuenta.IdUser);
                var correo = vMCuenta.Correo;
                var pass = vMCuenta.Password;
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
                if (pass != null)
                    await _userManager.ChangePasswordAsync(usuario, vMCuenta.OldPassword, pass);
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
