using BlockBuster.manager.Entidades;
using BlockBuster.manager.Manager;
using Frankbuster.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

namespace Frankbuster.web.Controllers
{
    public class PeliculasController : Controller
    {

        private readonly IPeliculasManager _peliculasManager;

        public  PeliculasController(IPeliculasManager peliculasManager)
        {
            _peliculasManager = peliculasManager;
        }



        // GET: PeliculasController
        public ActionResult Index()
        {
            var peliculas = _peliculasManager.GetPeliculas();
            return View("Delete");
        }

        // GET: PeliculasController/Details/5
        public ActionResult Details()
        {
            var peliculas = _peliculasManager.GetPeliculas(); // Obtener todas las películas

            List<PeliculaVM> peliculasModel = new List<PeliculaVM>();

            // Convertir cada película a PeliculaVM
            foreach (var pelicula in peliculas)
            {
                PeliculaVM modelo = new PeliculaVM
                {
                    pelicula_id = pelicula.pelicula_id,
                    titulo = pelicula.titulo,
                    descripcion = pelicula.descripcion,
                    fecha_publicacion = pelicula.fecha_publicacion
                };
                peliculasModel.Add(modelo);
            }

            return View(peliculasModel);
        }

        // GET: PeliculasController/Create
        public ActionResult Create()
        {
            PeliculaVM pelicula = new PeliculaVM();
            pelicula.titulo = null;
            pelicula.descripcion = null;
            pelicula.fecha_publicacion = null;

            return View(pelicula);
        }

        // POST: PeliculasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Pelicula peliculasModel = new Pelicula();
                peliculasModel.titulo = collection["titulo"];
                peliculasModel.descripcion = collection["descripcion"];
                peliculasModel.fecha_publicacion = collection["fecha_publicacion"];

                if (string.IsNullOrWhiteSpace(peliculasModel.titulo) ||
                    string.IsNullOrWhiteSpace(peliculasModel.descripcion) ||
                    string.IsNullOrWhiteSpace(peliculasModel.fecha_publicacion))
                {
                    // Si alguno de los campos está vacío, mostrar un mensaje de error
                    TempData["ErrorMessage"] = "Todos los campos son obligatorios.";
                    //return View();

                    return View();
                }
                _peliculasManager.CrearPelicula(peliculasModel);
                TempData["SuccessMessage"] = "La película ha sido creada exitosamente.";

                return RedirectToAction(nameof(Details));
                

            }
            catch
            {
                return View();
            }
        }

        // GET: PeliculasController/Edit/5
        public ActionResult Edit(int id)
        {
            var pelicula = _peliculasManager.GetPelicula(id);

            PeliculaVM peliculaModel = new PeliculaVM();
            peliculaModel.titulo = pelicula.titulo;
            peliculaModel.descripcion = pelicula.descripcion;
            peliculaModel.fecha_publicacion = pelicula.fecha_publicacion;

            return View(peliculaModel);
        }

        // POST: PeliculasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {

                Pelicula pelicula = new Pelicula()
                {
                    titulo = collection["titulo"],
                    descripcion = collection["descripcion"],
                    fecha_publicacion = collection["fecha_publicacion"],
                };

                if (string.IsNullOrWhiteSpace(pelicula.titulo) ||
                    string.IsNullOrWhiteSpace(pelicula.descripcion) ||
                    string.IsNullOrWhiteSpace(pelicula.fecha_publicacion))
                {
                    TempData["ErrorMessage"] = "Todos los campos son obligatorios.";
                    return RedirectToAction(nameof(Edit));

                }else{
                    TempData["SuccessMessage"] = "Película modificada exitosamente.";
                    _peliculasManager.ModificarPelicula(id, pelicula);
                    return RedirectToAction(nameof(Details));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: PeliculasController/Delete/5
        public ActionResult Delete(int id)
        {
            var container = _peliculasManager.GetPelicula(id);

            PeliculaVM peliculasModel = new PeliculaVM();
            peliculasModel.titulo = container.titulo;
            peliculasModel.descripcion = container.descripcion;
            peliculasModel.fecha_publicacion = container.fecha_publicacion;

            return View(container);
        }

        // POST: PeliculasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _peliculasManager.EliminarPelicula(id);
                TempData["SuccessMessage"] = "Película eliminada exitosamente.";
                //return View("../Home/index");
                return RedirectToAction(nameof(Details));

            }
            catch
            {
                return View();
            }
        }
    }
}
