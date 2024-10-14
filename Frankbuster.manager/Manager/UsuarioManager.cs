using BlockBuster.manager.Entidades;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.SqlClient;
using BlockBuster.manager.Conexion;
using BlockBuster.manager.ModelFactories;
using System.Drawing.Text;
using BlockBuster.manager.Repositorios;
using System;

namespace BlockBuster.manager.Manager
{

    public interface IUsuarioManager
    {
        IEnumerable<UsuarioCompleto> GetUsuarios();
        Usuario GetUsuario(int IdUsuario);
        int CrearUsuario(Usuario usuario);
        bool ModificarUsuario(int IdUsuario, Usuario usuario);
        bool EliminarUsuario(int IdUsuario);

    }



    public class UsuarioManager:IUsuarioManager
    {
        
        private IUsuarioRepository _repo;

        public UsuarioManager(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Obtiene un Usuario por Id 
        /// </summary>
        /// <param name="IdUsuario">Id del Usuario</param>
        /// <returns></returns>

        public Usuario GetUsuario(int IdUsuario)
        {
            var usuario = _repo.GetUsuario(IdUsuario);

            return usuario;

        }

        /// <summary>
        /// Obtiene una lista de Usuarios
        /// </summary>
        /// <returns></returns>

        public IEnumerable<UsuarioCompleto> GetUsuarios()
        {
            return _repo.GetUsuariosCompleto();

        }

        /// <summary>
        /// Crea un Container en la Base de Datos
        /// </summary>
        /// <param name="IdUsuario">Datos del usuario</param>
        /// <returns></returns>

        public int CrearUsuario(Usuario usuario)          
        {
            //usuario.UsuarioId = usuario;
            //usuario.FechaAlta = DateTime.Now;
            var usu = _repo.CrearUsuario(usuario);

            return usu;

        }
        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="idUsuario">usuario dado de baja</param>
        /// <returns></returns>
        
        public bool EliminarUsuario(int idUsuario)
        {
            return _repo.EliminarUsuario(idUsuario);
        }

        /// <summary>
        /// Modifica los datos de un usuario a partir de un Id
        /// </summary>
        /// <param name="IdUsuario">Id del usuario a modificar</param>
        /// <returns></returns>

        public bool ModificarUsuario(int IdUsuario, Usuario usuario)
        {
            var usuarioEnDb = _repo.GetUsuario(IdUsuario);

            usuarioEnDb.Nombre = usuario.Nombre;
            usuarioEnDb.IdentificacionId = usuario.IdentificacionId;
           
            var usu = _repo.ModificarUsuario(IdUsuario, usuario);

            return usu;

        }
     

    }
}
