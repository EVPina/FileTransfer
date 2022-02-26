using FileTransfer.Data;
using FileTransfer.Models.Entities;
using FileTransfer.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.Controllers
{
    public class UserController : Controller
    {
        FileTransferContext _context;
        public UserController(FileTransferContext context)
        {
            _context = context;
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

        [HttpPost]
        public async Task<string> GetUsername(string id)
        {
            var name_user = await _context.Users.Where(a => a.Id == id).Select(c => c.UserName).FirstOrDefaultAsync();
            return name_user;
        }
        [HttpPost]
        public IActionResult OptionsProfile(string ValueOpcion)
        {
            if(ValueOpcion == "1")
                return View(nameof(Cuenta));
            return View(nameof(Profile));
        }

        [HttpPost]
        public async Task<IActionResult> SendProfile(VMProfile vMProfile)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Profile));
            }
            return View(vMProfile);
        }
    }
}
