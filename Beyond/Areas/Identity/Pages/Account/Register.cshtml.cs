using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Beyond.Data.Models;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Beyond.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IAssignToRole _assignToRole;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAssignToRole assignToRole)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _assignToRole = assignToRole;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (!ModelState.IsValid) return Page();
            var user = new User
            {
                UserName = Input.Email,
                Email = Input.Email
            };

            if (await _userManager.FindByEmailAsync(Input.Email)!=null)
            {
                ModelState.AddModelError("Email", errorMessage:"Account with that Email already exists");
                return Page();
            }

            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                var registeredUser = await _userManager.FindByEmailAsync(Input.Email);
                if (registeredUser != null)
                    await _assignToRole.User(registeredUser);
                return LocalRedirect(returnUrl);
            }

            if (!result.Errors.Any()) return RedirectToPage("Login");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);

            }

            return Page();
        }
    }
}
