using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Secrets_sharing.Models;

namespace Secrets_sharing.Pages
{
    public class UploadModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        public UploadModel(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public class InputModel
        {
            [Required]
            [Display(Name = "File")]
            [DataType(DataType.Upload)]
            public IFormFile File { get; set; }

            [Display(Name = "Do you want to delete file on download?")]
            public bool DeleteOnDownload { get; set; }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        
        public string GenerateUrl()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-+";
            string url = "";
            var rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                var number = rand.Next(0, chars.Length);
                url += chars[number];
            }
            var findResult = _context.Files.Any(f => f.Url == url);
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
            var currentUser = await _userManager.GetUserAsync(User); // Get current user
            var file = new Models.File {Name = Input.File.FileName, Url = GenerateUrl(), DeleteOnDownload = Input.DeleteOnDownload }; // Create new file object
            byte[] imageData = null; // Array of bytes

            using (var binaryReader = new BinaryReader(Input.File.OpenReadStream())) // Create reader to read bytes
            {
                imageData = binaryReader.ReadBytes((int)Input.File.Length); // Write bytes to array
            }
            file.Bytes = imageData; 

            _context.Entry(currentUser).Collection(u => u.Files).Load();
            currentUser.Files.Add(file);
            await _context.SaveChangesAsync();

            return RedirectToPage("Files");
        }
    }
}
