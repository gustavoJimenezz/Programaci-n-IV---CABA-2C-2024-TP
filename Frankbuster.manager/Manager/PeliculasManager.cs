using BlockBuster.manager.Entidades;
using BlockBuster.manager.ModelFactories;
using BlockBuster.manager.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuster.manager.Manager
{
    public interface IPeliculasManager
    {
        IEnumerable<PeliculaCompleta> GetPeliculas();
        IEnumerable<PeliculaCompleta> GetPeliculaAlquiladaPorUsuario(int usuario_id);
        Pelicula GetPelicula(int pelicula_id);
        int CrearPelicula(Pelicula pelicula);
        bool ModificarPelicula(int pelicula_id, Pelicula pelicula);
        bool EliminarPelicula(int pelicula_id);
        bool AlquilarPelicula(int pelicula_id);
        bool MostrarPeliculaAlquiladaPorGoogle(decimal googleIdentificator);
        IEnumerable<PeliculaCompleta> GetPeliculasDisponibles();
        bool DevolverPelicula(int pelicula_id);

    }
    public class PeliculasManager: IPeliculasManager
    {
        private IPeliculaRepository _repo;

        public PeliculasManager(IPeliculaRepository repo)
        {
            _repo = repo;
        }

        public int CrearPelicula(Pelicula pelicula)
        {

            var peli = _repo.CrearPelicula(pelicula);

            return peli;
        }

        public bool EliminarPelicula(int pelicula_id)
        {
            return _repo.EliminarPelicula(pelicula_id);
        }
        public bool DevolverPelicula(int pelicula_id)
        {
            return _repo.DevolverPelicula(pelicula_id);
        }

        public Pelicula GetPelicula(int pelicula_id)
        {
            var peli = _repo.GetPelicula(pelicula_id);

            return peli;
        }

        public IEnumerable<PeliculaCompleta> GetPeliculas()
        {
            return _repo.GetPeliculaCompleta(); ;
        }

        public IEnumerable<PeliculaCompleta> GetPeliculasDisponibles()
        {
            return _repo.GetPeliculasDisponibles(); ;
        }

        public IEnumerable<PeliculaCompleta> GetPeliculaAlquiladaPorUsuario(int usuario_id)
        {
            return _repo.GetPeliculaAlquiladaPorUsuario(usuario_id);
        }

        public bool ModificarPelicula(int pelicula_id, Pelicula pelicula)
        {
            var peliculaEnDb = _repo.GetPelicula(pelicula_id);

            peliculaEnDb.titulo = pelicula.titulo;
            peliculaEnDb.descripcion = pelicula.descripcion;
            peliculaEnDb.fecha_publicacion = pelicula.fecha_publicacion;

            var peli = _repo.ModificarPelicula(pelicula_id, pelicula);

            return peli;
        }

        public bool AlquilarPelicula(int pelicula_id)
        {
            return _repo.AlquilarPelicula(pelicula_id);
        }

        public bool MostrarPeliculaAlquiladaPorGoogle(decimal googleIdentificator)
        {
            return _repo.MostrarPeliculaAlquiladaPorGoogle(googleIdentificator);
        }
    }
}
