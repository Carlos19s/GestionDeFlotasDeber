using GestionDeFlotas.Modelos;
using Microsoft.AspNetCore.Mvc;
using Proyecto.API.Consumer;
using Microsoft.AspNetCore.Http;


namespace Proyecto.Api.MVC.Controllers
{
    public class LoginDtoesController : Controller
    {
        // GET: LoginDtoes/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login2(LoginDto login)
        {

            bool esValido = Crud<LoginDto>.Loggin(login); 

            if (esValido)
            {
                HttpContext.Session.SetString("usuario", login.Usuario);
                return RedirectToAction("Index", "Home"); 
            }

            ViewData["ErrorMessage"] = "Usuario o contraseña incorrectos.";
            return View("Login", login);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
