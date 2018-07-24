using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AstralNotes.ViewModels;
using System.Threading.Tasks;
using AstralNotes.Domain.Abstractions;
using AstralNotes.Domain.Entities;

namespace AstralNotes.Controllers
{
    /// <summary>
    /// Контроллер управления аккаунтом
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;
        private readonly SignInManager<User> _signInManager;

        /// <summary />
        public AccountController(IUserService userService, SignInManager<User> signInManager,
            IAuthorizationService authorizationService)
        {
            _userService = userService;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
        }

        /// <summary />
        public IActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="model" href="RegistrationViewModel" />
        /// <returns href="IActionResult" />
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.Create(model.UserName, model.Password, model.Email);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="returnUrl">Возвращаемый юрл</param>
        /// <returns href="IActionResult" />
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel {ReturnUrl = returnUrl});
        }

        /// <summary>
        /// 123
        /// </summary>
        /// <param name="model">123</param>
        /// <returns>123</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authorizationService.Authorize(model.UserName, model.Password);
                await _signInManager.SignInAsync(user, model.RememberMe);

                if (string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}