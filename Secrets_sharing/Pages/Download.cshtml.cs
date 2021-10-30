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
        public IActionResult OnGet()
        {
            var file = _context.Files.First(f => f.Url == FileUrl);
            var path = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllBytes(path, file.Bytes);
            return PhysicalFile(path, MimeTypes.GetMimeType(path), file.Name);
        }
    }
}
