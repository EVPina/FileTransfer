using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.ViewsModels
{
    public class VMCuenta : IListErros
    {    
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Correo { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Contraseñas diferentes")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Imagen")]
        public IFormFile ImagenUsuario { get; set; }

        public List<dynamic> ListErros { get; set; }
    }
}
