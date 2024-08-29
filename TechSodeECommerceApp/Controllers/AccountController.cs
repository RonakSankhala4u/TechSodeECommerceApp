using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TechSodeECommerceApp.Models;
using TechSodeECommerceApp.Repositories.Repo;
using TechSodeECommerceApp.ViewModels;

namespace TechSodeECommerceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailSender _emailSender;


        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, EmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, IsActive = true };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Send confirmation email here...
                    // Example: await _emailSender.SendEmailConfirmationAsync(user, confirmationLink);

                    TempData["ShowEmailSentModal"] = true;  // Indicate that the email sent modal should be shown

                    // Redirect to the same page to avoid resubmission issues
                    return RedirectToAction(nameof(Register));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
                // Initialize other necessary properties here

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            // If returnUrl is null or empty, set it to a fixed URL
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Profile", "Account"); // Fixed Home Page URL
            }

            model.ReturnUrl = returnUrl;

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl); // Redirect to the specified or fixed return URL
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


        

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);  // Get the current logged-in user

            if (user == null)
            {
                return RedirectToAction("Login");  // Redirect to login if user is not found
            }

            // Map user data to ProfileViewModel
            var model = new ProfileViewModel
            {
                Name = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            // If returnUrl is null or empty, set it to a fixed URL
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Profile", "Account"); // Fixed Home Page URL
            }

            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Get the email claim from external provider
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            if (email == null)
            {
                ModelState.AddModelError(string.Empty, "Error obtaining email from external provider.");
                return View(nameof(Login));
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                // User does not exist, create a new one
                user = new ApplicationUser { UserName = email, Email = email };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    // Send email confirmation
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token = token }, Request.Scheme);
                    await _emailSender.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");

                    // Link external login
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        // Inform user to check their email
                        ViewBag.Message = "Check your email to confirm your account.";
                        return View("ConfirmEmailSent");
                    }
                }
            }
            else
            {
                // Existing user, check if email is confirmed
                if (!user.EmailConfirmed)
                {
                    // Resend email confirmation
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token = token }, Request.Scheme);
                    await _emailSender.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");

                    ViewBag.Message = "Check your email to confirm your account.";
                    return View("ConfirmEmailSent");
                }

                // Sign in the user if email is confirmed
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }

            return View(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData["ShowEmailSuccessModal"] = true;  // Indicate that the email success modal should be shown

                // Redirect to the same page to show the success modal
                return RedirectToAction(nameof(ConfirmEmail));
            }

            return View("Error");
        }

    }
}
