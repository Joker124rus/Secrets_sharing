using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secrets_sharing.Models
{
    public class User : IdentityUser
    {
        public List<File> Files { get; set; }
    }
}
