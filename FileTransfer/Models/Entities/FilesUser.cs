using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.Models.Entities
{
    public class FilesUser
    {
        [Key]
        public string IdFileUser { get; set; }
        public string NameFile { get; set; }
        public string SizeFile { get; set; }
        public string DateUpload { get; set; }
        public Usuario IdUser { get; set; }
    }
}
