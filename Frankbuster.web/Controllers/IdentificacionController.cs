using Blockbuster.web.Models;
using BlockBuster.manager.Manager;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blockbuster.web.Controllers
{
    public class IdentificacionController : Controller
    {

        private readonly IdentificacionManager _identificacionManager;

        public IdentificacionController(IdentificacionManager identificacionManager)
        {
            _identificacionManager = identificacionManager;
        }

        // GET: IdentificacionController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult Login(IdentificacionVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var identificacion = _identificacionManager.ValidarCredenciales(model.Email, model.ContraHash);
        //        if (identificacion != null)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        TempData["ErrorMessage"] = "Credenciales incorrectas";
        //    }
    //    //    return View("Index");
    //}
    public IActionResult Login(IdentificacionVM model)
    {
        if (ModelState.IsValid)
        {
            // Simulación de un usuario administrador (harcodeado)
            if (model.Email == "admin@admin.com" && model.ContraHash == "admin1234")
            {
                // Crear Claims para el usuario
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(ClaimTypes.Role, "Admin") // Asignar rol
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Recordar la sesión
                };

                // Iniciar sesión
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                         new ClaimsPrincipal(claimsIdentity),
                                         authProperties).Wait();

                return RedirectToAction("Index", "Home");
            }

            // Validar usuario con el servicio
            var identificacion = _identificacionManager.ValidarCredenciales(model.Email, model.ContraHash);
            if (identificacion != null)
            {
                // Crear Claims para el usuario válido
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(ClaimTypes.Role, "User") // Rol por defecto
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Recordar la sesión
                };

                // Iniciar sesión
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                         new ClaimsPrincipal(claimsIdentity),
                                         authProperties).Wait();

                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Credenciales incorrectas";
        }

        return View("Index");
    }




    // GET: IdentificacionController/Details/5
    public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IdentificacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IdentificacionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IdentificacionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IdentificacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IdentificacionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IdentificacionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
