﻿using FileTransfer.Models.Entities;
using FileTransfer.ViewsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileTransfer.Controllers
{
    public class AccountController : Controller
    {
        UserManager<Usuario> _userManager;
        SignInManager<Usuario> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        public AccountController(SignInManager<Usuario> signInManager,UserManager<Usuario> userManager,RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLoginUser vMLoginUser)
        {

            if (ModelState.IsValid)
            {
                var checklogin = await _signInManager.PasswordSignInAsync(vMLoginUser.Correo, vMLoginUser.Contraseña, vMLoginUser.Remember, false);
                if (checklogin.Succeeded)
                {
                    var checkuser = await _userManager.FindByEmailAsync(vMLoginUser.Correo);
                    if (checkuser != null)
                        return RedirectToAction("Index", "Home", checkuser.Id);
                }
            }
            ModelState.AddModelError(string.Empty, "Error al momento de loguearse");
            return View(vMLoginUser);
            
        }
        
        [HttpPost]
        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();            
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(VMRegistar vMRegistar)
        {

            if (ModelState.IsValid)
            {
                List<dynamic> ListErros = new List<dynamic>();
                var check_role = await _roleManager.FindByNameAsync("user");
                string user_role = "user";
                if (check_role == null)
                {
                    var role = new IdentityRole { Name = user_role };
                    var checkrole = await _roleManager.CreateAsync(role);

                    if(!checkrole.Succeeded)
                        ListErros.Add("Error al momento de crear role");
                }

                Usuario usuario = new Usuario() { Email = vMRegistar.Correo,UserName = vMRegistar.Correo };
                var checkuser =  await _userManager.FindByEmailAsync(vMRegistar.Correo);
                if(checkuser == null)
                {
                    var checkregister = await _userManager.CreateAsync(usuario, vMRegistar.Contraseña);
                    if (checkregister.Succeeded)
                    {
                        var add_role = await _userManager.AddToRoleAsync(usuario, user_role);
                        if (add_role.Succeeded)
                        {
                            return RedirectToAction("Login");
                        }
                        else
                            ListErros.Add("Error al momento de añadir rol");
                    }
                    else
                        ListErros.Add("Error al momento de crear usuario");
                }
                else
                    ListErros.Add("El usuario existe");

                foreach (var error in ListErros)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            
            return View(vMRegistar);

        }
    }
}
