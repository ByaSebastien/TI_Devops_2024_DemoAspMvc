using Microsoft.AspNetCore.Mvc;
using TI_Devops_2024_DemoAspMvc.BLL.Interfaces;
using TI_Devops_2024_DemoAspMvc.Mappers;
using TI_Devops_2024_DemoAspMvc.Models;

namespace TI_Devops_2024_DemoAspMvc.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View(new UserRegisterFormDTO());
        }

        [HttpPost]
        public IActionResult Register([FromForm]UserRegisterFormDTO form)
        {
            if(!ModelState.IsValid)
            {
                return View(form);
            }
            _userService.Register(form.ToEntity());
            return RedirectToAction("Index", "Home");
        }
    }
}
