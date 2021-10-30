using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Secrets_sharing.Models;

namespace Secrets_sharing.Pages
{
    [Authorize]
    public class FilesModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        public FilesModel(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public List<File> Files { get; set; }
        public void OnGet()
        {
            var user = _userManager.GetUserAsync(User).Result;
            _context.Entry(user).Collection(u => u.Files).Load();
            Files = user.Files;
        }
    }
}
