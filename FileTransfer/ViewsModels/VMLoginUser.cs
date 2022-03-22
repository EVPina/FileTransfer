using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.ViewsModels
{
    public class VMLoginUser
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display( Name = "Email")]
        public string Correo { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display( Name = "Password")]
        public string Contraseña { get; set; }

        public bool Remember { get; set; }
    }
}
