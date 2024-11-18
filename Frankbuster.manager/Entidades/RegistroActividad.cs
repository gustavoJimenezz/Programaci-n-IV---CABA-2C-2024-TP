using BlockBuster.manager.ModelFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuster.manager.Entidades
{
    public class RegistroActividad
    {
        public int actividad_id {  get; set; }
        public int usuario_id { get; set; }
        public string actividad {  get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public int pelicula_id { get; set; }
        public string googleIdentificator { get; set; }
    }

    public static class RegistroActividadFactory
    {
        public static RegistroActividad CrearRegistroAlquilar(int usuario_id,string googleIdentificator , Pelicula pelicula = null)
        {
            //Nota: agregar datos de pelicula?
            return new RegistroActividad
            {
                usuario_id = usuario_id,
                actividad = "Alquiler",
                descripcion = $"Titulo : {pelicula.titulo} " +
                $"Descripcion : {pelicula.descripcion}" +
                $"Fecha de publicacion : {pelicula.fecha_publicacion}", 
                fecha = DateTime.Now,
                pelicula_id = pelicula.pelicula_id,
                googleIdentificator = googleIdentificator
            };
        }


    }
}
