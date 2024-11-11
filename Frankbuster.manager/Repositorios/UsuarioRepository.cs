using BlockBuster.manager.Entidades;
using BlockBuster.manager.ModelFactories;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuster.manager.Repositorios
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuario(int IdUsuario);

        IEnumerable<Usuario> GetUsuarios(bool? SoloActivos = true);
        IEnumerable<UsuarioCompleto> GetUsuariosCompleto();
        int CrearUsuario(Usuario usuario);
        bool ModificarUsuario(int IdUsuario, Usuario usuario);
        bool EliminarUsuario(int IdUsuario);
        Usuario GetUsuarioPorGoogleSubject(string googleSubject);
        string ObtenerRol(int idUsuario);
    }

    public class UsuarioRepository : IUsuarioRepository
    {

        private string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;

        }

        /// <summary>
        /// Consulta a la base de datos por Id
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>

        public Usuario GetUsuario(int IdUsuario)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {

                string query = "SELECT * FROM Usuario WHERE IdUsuario = " + IdUsuario.ToString();

                Usuario result = con.QuerySingle<Usuario>(query);

                return result;
            }

        }


        ///<summary>
        /// Consulta a la DDBB por la lista de Usuario
        /// </summary>
        /// <param name="SoloActivos">True: Solo trae los activos, False: Trae todos los registros</param>
        /// <returns>Devuelve una lista de usuarios activos</returns>
        public IEnumerable<Usuario> GetUsuarios(bool? SoloActivos = true)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {


                string query = "SELECT * FROM Usuario ";
                if (SoloActivos == true)
                    query += " WHERE FechaBaja is null";
                IEnumerable<Usuario> results = conn.Query<Usuario>(query);

                return results;

            }
        }

        /// <summary>
        /// Obtiene una lista completa de los usuarios
        /// </summary>
        /// <returns>Lista completa de usuarios</returns>
        /// 
        public IEnumerable<UsuarioCompleto> GetUsuarioCompletos()
        {

            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"SELECT Usuario-*,

                                  ";



                IEnumerable<UsuarioCompleto> results = conn.Query<UsuarioCompleto>(query);

                return results;

             }

        }


  



        /// <summary>
        /// Crear un nuevo Usuario en la base de datos
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Devuelve el ID del container creado</returns>
        public int CrearUsuario(Usuario usuario)
        {
            // ---- Using: Para abrir y cerrar la conexion una vez hecho la consulta ---- //
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"INSERT INTO Usuario (nombre, fecha_alta, activo, identificacion_id, googleIdentificador)  
                            VALUES (@nombre, @fechaAlta, @activo, @identificacionId, @googleIdentificador);                    
                            SELECT CAST(SCOPE_IDENTITY() AS INT) ";


                usuario.UsuarioId = conn.QuerySingle<int>(query, usuario);


                return usuario.UsuarioId;
            }


        }


        /// <summary>
        /// Modificar Usuario en la base de Datos
        /// </summary>
        /// <param name="usuarioId">Id del Usuario a modificar</param>
        /// <param name="usuario"></param>
        /// <param name="IdUsuarioModificacion"></param>
        /// <returns></returns>
        public bool ModificarUsuario(int IdUsuario, Usuario usuario)
        {
            // ---- Using: Para abrir y cerrar la conexion una vez hecho la consulta ---- //
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"UPDATE 
                                Usuario 
                            SET 
                                Usuario_Id = @Usuario_Id, 
                                Nombre = @Nombre, 
                                Fecha_Alta = @Fecha_Alta, 
                                Activo  = @Activo    

                                WHERE Usuario_Id = " + IdUsuario.ToString();

                // conn.execute devuelve un entero que representa la cantidad de filas afectadas. 
                //Se espera que se haya modificado solo un registro, por eso se lo compara con un 1.
                return conn.Execute(query, usuario) == 1;
            }
        }


        /// <summary>
        /// Eliminar de manera lógica un usuario de la base de datos
        /// </summary>
        /// <param name="Usuario_Id">Id del usuario que sera dado de baja</param
        /// <returns></returns>


        //FALTA CAMPO FechaBaja EN LA TABLA USUARIOS 
        public bool EliminarUsuario(int IdUsuario)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"Update 
                                      Usuario
                               SET

                                   FechaBaja = '" + DateTime.Now.ToString("yyyyMMdd") + "'," +
                                    "WHERE Usuario_Id =" + IdUsuario.ToString();
                //db.execute devuelve un entero que representa la cantidad de filas afectadas. 
                //Se espera que se haya modificado solo un registro, por eso se lo compara con un 1.
                return conn.Execute(query) == 1;
            }
        }

        public IEnumerable<UsuarioCompleto> GetUsuariosCompleto()
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                IEnumerable<UsuarioCompleto> results =con.Query<UsuarioCompleto>(@"SELECT * FROM usuario;");
                return results;
            }
        }

        public bool EliminarUsuario(int IdUsuario, int IdUsuarioBaja)
        {
            throw new NotImplementedException();
        }


        public Usuario GetUsuarioPorGoogleSubject(string googleSubject)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                Usuario usuarios = db.Query<Usuario>("SELECT * FROM Usuario WHERE googleIdentificador = '" + googleSubject.ToString() + "'").FirstOrDefault();
                return usuarios;
            }
        }

        public string ObtenerRol(int idUsuario)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SELECT r.nombre_rol " +
                            "FROM relacion_usuario_rol ur " +
                            "JOIN roles r ON ur.rol_id = r.rol_id " +
                            "WHERE ur.usuario_id = @UsuarioId";

                var nombreRol = db.Query<string>(query, new { UsuarioId = idUsuario }).FirstOrDefault();

                return nombreRol;
            }
        }
    }
}