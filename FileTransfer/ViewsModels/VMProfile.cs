using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.ViewsModels
{
    public class VMProfile : IListErros
    {
        [DataType(DataType.EmailAddress)]
        [Display( Name = "Email")]
        public string Correo { get; set; }
        
        [DataType(DataType.Password)]
        [Display( Name = "Password")]
        public string Contraseña { get; set; }
        
        [DataType(DataType.Password)]
        [Compare(nameof(Contraseña), ErrorMessage = "Contraseñas diferentes")]
        [Display( Name = "Confirm Password")]
        public string ConfirmContraseña { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display( Name = "Mobile Phone")]
        public string Celular { get; set; }


        public List<dynamic> ListErros { get; set; }
    }
}
