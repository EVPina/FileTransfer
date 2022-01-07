using FileTransfer.Models.Entities;
using FileTransfer.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Profile(VMProfile vMProfile)
        {
            return View(vMProfile);
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
