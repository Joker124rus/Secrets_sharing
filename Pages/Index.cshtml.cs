using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secrets_sharing.Pages
{
    public class IndexModel : PageModel
    {

        public IndexModel()
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
