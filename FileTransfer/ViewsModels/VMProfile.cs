using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.ViewsModels
{
    public class VMProfile : IListErros
    {

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        public List<dynamic> ListErros { get; set; }
    }
}
