using BlockBuster.manager.Entidades;
using BlockBuster.manager.Manager;
using BlockBuster.manager.ModelFactories;
using Frankbuster.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Frankbuster.web.Controllers
{
    public class RegistroActividadController : Controller
    {

        private readonly IRegistroActividadManager _registroActividadManager;
        private readonly IUsuarioManager _usuarioManager;

        public RegistroActividadController(IRegistroActividadManager registroActividadManager, IUsuarioManager usuarioManager)
        {
            _registroActividadManager = registroActividadManager;
            _usuarioManager = usuarioManager;
        }

        // GET: ActividadController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ActividadController/Details/5
        public ActionResult Details()
        {
            var RegistrosActividad = _registroActividadManager.GetRegistroActividades(); // Obtener todos los registros de Actividad
            List<RegistroActividadVM> registrosActividadModel = new List<RegistroActividadVM>();

            // Convertir cada película a RegistroActividadVM
            foreach (var registroActividad in RegistrosActividad)
            {
                Usuario usuario_actual = _usuarioManager.GetUsuario(registroActividad.usuario_id);
                RegistroActividadVM modelo = new RegistroActividadVM
                {
                    actividad_id = registroActividad.actividad_id,
                    usuario_id = registroActividad.usuario_id,
                    descripcion = registroActividad.descripcion,
                    actividad = registroActividad.actividad,
                    fecha = registroActividad.fecha,
                    pelicula_id = registroActividad.pelicula_id,
                    googleIdentificator = registroActividad.googleIdentificator,
                    nombre_usuario = usuario_actual.Nombre,

                };
                registrosActividadModel.Add(modelo);
            }
            return View(registrosActividadModel);
        }

        public ActionResult ListaRegistroUsuario()
        {
            var RegistrosActividad = _registroActividadManager.GetRegistroActividades(); // Obtener todos los registros de Actividad

            List<RegistroActividadVM> registrosActividadModel = new List<RegistroActividadVM>();

            // Convertir cada película a RegistroActividadVM
            foreach (var registroActividad in RegistrosActividad)
            {
                RegistroActividadVM modelo = new RegistroActividadVM
                {
                    actividad_id = registroActividad.actividad_id,
                    usuario_id = registroActividad.usuario_id,
                    descripcion = registroActividad.descripcion,
                    actividad = registroActividad.actividad,
                    fecha = registroActividad.fecha,
                    pelicula_id = registroActividad.pelicula_id,
                    googleIdentificator = registroActividad.googleIdentificator,

                };
                registrosActividadModel.Add(modelo);
            }

            return View(registrosActividadModel);
        }
        
        // GET: ActividadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActividadController/Create
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

        // GET: ActividadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActividadController/Edit/5
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

        // GET: ActividadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActividadController/Delete/5
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
