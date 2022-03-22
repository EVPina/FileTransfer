using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.ViewsModels
{
    public class VMRememberPass
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Contraseñas diferentes")]
        [Display(Name = "Confirma Contraseña")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Ultima Contraseña")]
        public string LastPassword { get; set; }
    }
}
