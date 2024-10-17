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
    public interface IPeliculaRepository
    {
        Pelicula GetPelicula(int pelicula_id);

        IEnumerable<Pelicula> GetPeliculasTitulo(string Titulo);
        IEnumerable<PeliculaCompleta> GetPeliculaCompleta();
        int CrearPelicula(Pelicula pelicula);
        bool ModificarPelicula(int pelicula_id, Pelicula pelicula);
        bool EliminarPelicula(int pelicula_id);


    }

    public class ContainerRepository : IPeliculaRepository
    {

        private string _connectionString;

        public ContainerRepository(string connectionString)
        {
            _connectionString = connectionString;

        }

        /// <summary>
        /// Consulta a la base de datos por Id
        /// </summary>
        /// <param name="pelicula_id"></param>
        /// <returns></returns>

        public Pelicula GetPelicula(int pelicula_id)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {

                string query = "SELECT * FROM peliculas WHERE pelicula_id = " + pelicula_id.ToString();

                Pelicula result = con.QuerySingle<Pelicula>(query);

                return result;
            }

        }


        ///<summary>
        /// Consulta a la DDBB por la lista de Usuario
        /// </summary>
        /// <param name="titulo">True: Solo trae los activos, False: Trae todos los registros</param>
        /// <returns>Devuelve una lista de usuarios activos</returns>
        public IEnumerable<Pelicula> GetPeliculasTitulo(string Titulo)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {


                string query = "SELECT * FROM peliculas WHERE titulo = " + Titulo.ToString();
                IEnumerable<Pelicula> results = conn.Query<Pelicula>(query);

                return results;

            }
        }

        /// <summary>
        /// Obtiene una lista completa de los usuarios
        /// </summary>
        /// <returns>Lista completa de usuarios</returns>
        /// 
        public IEnumerable<PeliculaCompleta> GetPeliculaCompletas()
        {

            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"SELECT Peliculas-*,

                                  ";



                IEnumerable<PeliculaCompleta> results = conn.Query<PeliculaCompleta>(query);

                return results;

             }

        }


  



        /// <summary>
        /// Crear un nuevo Usuario en la base de datos
        /// </summary>
        /// <param name="pelicula"></param>
        /// <returns>Devuelve el ID del container creado</returns>
        public int CrearPelicula(Pelicula pelicula)
        {
            // ---- Using: Para abrir y cerrar la conexion una vez hecho la consulta ---- //
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"INSERT INTO peliculas (pelicula_id, titulo, descripcion, fecha_estreno)  
                            VALUES ( @pelicula_id, @titulo, @descripcion, @fecha_estreno)                    ";


                pelicula.pelicula_id = conn.QuerySingle<int>(query, pelicula);


                return pelicula.pelicula_id;
            }


        }


        /// <summary>
        /// Modificar Usuario en la base de Datos
        /// </summary>
        /// <param name="pelicula_id">Id del Usuario a modificar</param>
        /// <param name="pelicula"></param>
        /// <param name="IdPeliculaModificacion"></param>
        /// <returns></returns>
        public bool ModificarPelicula(int pelicula_id, Pelicula pelicula)
        {
            // ---- Using: Para abrir y cerrar la conexion una vez hecho la consulta ---- //
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"UPDATE 
                                peliculas 
                            SET 
                                 
                                titulo = @titulo, 
                                 
                                descripcion  = @descripcion    

                                WHERE pelicula_id = " + pelicula_id.ToString();

                // conn.execute devuelve un entero que representa la cantidad de filas afectadas. 
                //Se espera que se haya modificado solo un registro, por eso se lo compara con un 1.
                return conn.Execute(query, pelicula) == 1;
            }
        }


        /// <summary>
        /// Eliminar de manera lógica un usuario de la base de datos
        /// </summary>
        /// <param name="pelicula_id">Id del usuario que sera dado de baja</param
        /// <returns></returns>


        //FALTA CAMPO FechaBaja EN LA TABLA USUARIOS 
        public bool EliminarPelicula(int pelicula_id)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"DELETE 
                                      peliculas
                               SET

                                   fecha_estreno = '" + DateTime.Now.ToString("yyyyMMdd") + "'," +
                                    "WHERE pelicula_id =" + pelicula_id.ToString();
                //db.execute devuelve un entero que representa la cantidad de filas afectadas. 
                //Se espera que se haya modificado solo un registro, por eso se lo compara con un 1.
                return conn.Execute(query) == 1;
            }
        }

        public IEnumerable<PeliculaCompleta> GetPeliculaCompleta()
        {
            throw new NotImplementedException();
        }

        public bool EliminarPelicula(string titulo)
        {
            throw new NotImplementedException();
        }
    }

}
