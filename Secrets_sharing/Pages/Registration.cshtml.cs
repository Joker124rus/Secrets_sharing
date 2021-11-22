using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Secrets_sharing.Models;

namespace Secrets_sharing.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public RegistrationModel(ApplicationContext context,
                                 SignInManager<User> signInManager,
                                 UserManager<User> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public class InputModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "Incorrect email")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords do not match")]
            [Display(Name = "Password Confirmation")]
            public string PasswordConfirm { get; set; }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var result1 = _context.Users.Any(u => u.Email == Input.Email );
            if (result1)
            {
                ModelState.AddModelError("Input.Email", "This email already exists");
            }
            if (ModelState.IsValid)
            {
                var user = new User { Email = Input.Email, UserName = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password); // Trying to create the user
                if (result.Succeeded) // If user created
                {
                    await _signInManager.SignInAsync(user, isPersistent: false); // Authenticate the user
                    return RedirectToPage("Index");
                }
            }
            return Page();
        }
    }
}
