using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.ViewsModels
{
    public class VMRegistar : IListErros
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display( Name = "Email")]
        public string Correo { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display( Name = "Password")]
        public string Contraseña { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Contraseña), ErrorMessage = "Contraseñas diferentes")]
        [Display( Name = "Confirm Password")]
        public string ConfirmContraseña { get; set; }
        public List<dynamic> ListErros { get; set; }
    }
}
