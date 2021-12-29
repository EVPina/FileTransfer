using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTransfer.Models.Entities
{
    public class Usuario : IdentityUser
    {
        public string ImageUser { get; set; }
    }
}
