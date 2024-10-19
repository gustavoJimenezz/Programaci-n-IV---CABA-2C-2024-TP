using BlockBuster.manager.Entidades;
using BlockBuster.manager.Manager;
using Frankbuster.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(peliculas);
        }

        // GET: PeliculasController/Details/5
        public ActionResult Details(int id)
        {
            var container = _peliculasManager.GetPelicula(id);

            PeliculaVM peliculasModel = new PeliculaVM();
            peliculasModel.titulo = container.titulo;
            peliculasModel.descripcion = container.descripcion;
            peliculasModel.fecha_publicacion = container.fecha_estreno;

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
                peliculasModel.fecha_estreno = collection["fecha_estreno"];

                _peliculasManager.CrearPelicula(peliculasModel);

            return RedirectToAction(nameof(Index));

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
            peliculaModel.fecha_publicacion = pelicula.fecha_estreno;

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
                    fecha_estreno = collection["fecha_estreno"],
                };
                

                _peliculasManager.ModificarPelicula(id, pelicula);
 
                return RedirectToAction(nameof(Index));
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
            peliculasModel.fecha_publicacion = container.fecha_estreno;

            return View(peliculasModel);
        }

        // POST: PeliculasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _peliculasManager.EliminarPelicula(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
