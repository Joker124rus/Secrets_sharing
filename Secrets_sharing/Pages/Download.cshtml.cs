using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Secrets_sharing.Models;
using MimeKit;

namespace Secrets_sharing.Pages
{
    public class DownloadModel : PageModel
    {
        private readonly ApplicationContext _context;

        public DownloadModel(ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string FileUrl { get; set; }
        
        public string TextContent { get; set; }
        public IActionResult OnGet()
        {
            if (_context.Files.Any(f => f.Url == FileUrl))
            {
            var file = _context.Files.First(f => f.Url == FileUrl); // Find file which url equals to given url
            var path = Path.GetTempFileName(); // Creating new temporary file
            System.IO.File.WriteAllBytes(path, file.Bytes); // Write bytes to temp file
            if (file.DeleteOnDownload == true)
            {
                _context.Files.Remove(file);
                _context.SaveChanges();
            }
            return PhysicalFile(path, MimeTypes.GetMimeType(path), file.Name); // Return requested file
            }
            else
            {
                var text = _context.Texts.First(t => t.Url == FileUrl);
                TextContent = text.Content;

                if (text.DeleteOnDownload == true)
                {
                    _context.Texts.Remove(text);
                    _context.SaveChanges();
                }

                return Page();
            }
        }
    }
}
