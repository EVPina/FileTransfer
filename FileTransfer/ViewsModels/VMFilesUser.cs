using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.ViewsModels
{
    public class VMFilesUser : IListErros
    {
        public string NameFile { get; set; }
        public string SizeFile { get; set; }
        public string DateUpload { get; set; }
        public string IdUser { get; set; }
        public int NumeroFile { get; set; }
        public List<dynamic> ListErros { get; set; }
    }
}
