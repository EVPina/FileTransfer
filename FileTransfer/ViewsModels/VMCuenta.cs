using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.ViewsModels
{
    public class VMCuenta : IData
    {    
        [Required]
        public string IdUser { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Correo { get; set; }

        [Display(Name = "Imagen")]
        public IFormFile ImagenUsuario { get; set; }
    }
}
