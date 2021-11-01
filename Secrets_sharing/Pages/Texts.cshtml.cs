using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Secrets_sharing.Models;

namespace Secrets_sharing.Pages
{
    [Authorize]
    public class TextsModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;
        public TextsModel(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public List<Text> Texts { get; set; }
        public void OnGet()
        {
            var user = _userManager.GetUserAsync(User).Result;
            _context.Entry(user).Collection(u => u.Texts).Load();
            Texts = user.Texts;
        }

        public IActionResult OnPost(string Url)
        {
            var text = _context.Texts.First(t => t.Url == Url);
            _context.Texts.Remove(text);
            _context.SaveChanges();

            return RedirectToPage("Texts");
        }
    }
}
