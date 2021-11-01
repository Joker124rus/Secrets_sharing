using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Secrets_sharing.Models;

namespace Secrets_sharing.Pages
{
    [Authorize]
    public class UploadTextModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public UploadTextModel(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Text content")]
            public string Content { get; set; }

            [Display(Name = "Do you want to delete this text on download?")]
            public bool DeleteOnDownload { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string GenerateUrl() // Method to generate unique url to file
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";
            string url = "";
            var rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                var number = rand.Next(0, chars.Length);
                url += chars[number];
            }
            var findResult = _context.Texts.Any(f => f.Url == url);
            if (findResult)
            {
                url = GenerateUrl();
                return url;
            }
            else
            {
                return url;
            }

        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            _context.Entry(user).Collection(u => u.Texts).Load();
            var text = new Text { Name = Input.Name,
                                  Content = Input.Content,
                                  Url = GenerateUrl(),
                                  DeleteOnDownload = Input.DeleteOnDownload };

            user.Texts.Add(text);
            await _context.SaveChangesAsync();


            return RedirectToPage("Texts");
        }
    }
}
