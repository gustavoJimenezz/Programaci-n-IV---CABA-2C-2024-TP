﻿using BlockBuster.manager.Entidades;
using BlockBuster.manager.ModelFactories;
using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        bool AlquilarPelicula(int pelicula_id);
        bool MostrarPeliculaAlquiladaPorGoogle(decimal googleIdentificator);


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
                //pelicula_id = 9;
                string query = "SELECT * FROM peliculas WHERE pelicula_id = " + pelicula_id.ToString();



                // PROBANDO 20/10
                //string query = "SELECT * FROM peliculas WHERE 1";
                Pelicula result = con.QuerySingle<Pelicula>(query, pelicula_id);

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

                string query = @"
                                INSERT INTO peliculas (titulo, descripcion, fecha_publicacion)  
                                VALUES (@titulo, @descripcion, @fecha_publicacion);
                                SELECT CAST(SCOPE_IDENTITY() AS int);"; // Obtener el último ID insertado

                int peliculaId = conn.QuerySingle<int>(query, pelicula);

                return peliculaId;
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
                                 
                                descripcion  = @descripcion,
                                fecha_publicacion = @fecha_publicacion

                                WHERE pelicula_id = " + pelicula_id.ToString();

                // conn.execute devuelve un entero que representa la cantidad de filas afectadas. 
                //Se espera que se haya modificado solo un registro, por eso se lo compara con un 1.
                return conn.Execute(query, pelicula) == 1;
            }
        }

        public bool AlquilarPelicula(int pelicula_id)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE peliculas 
                                    SET peliculaAlquilada = 1 
                                    WHERE pelicula_id = " + pelicula_id.ToString();

                    return conn.Execute(query) == 1;
                }
                catch
                {
                    return false;
                }
                
            }
        }

        public bool MostrarPeliculaAlquiladaPorGoogle(decimal googleIdentificator)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"select descripcion as registro_actividades, actividad as registro_actividades, fecha as registro_actividades from registro_actividades where googleIdentificator = " + googleIdentificator.ToString();
                return conn.Execute(query) == 1;
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
            try
            {
                using (IDbConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"DELETE FROM peliculas WHERE pelicula_id = @PeliculaId";
                    conn.Open();
                    var result = conn.Execute(query, new { PeliculaId = pelicula_id });
                    return result == 1;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (por ejemplo, registrar el error)
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public IEnumerable<PeliculaCompleta> GetPeliculaCompleta()
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {

                string query = @"SELECT * FROM 
                                      peliculas";
                IEnumerable<PeliculaCompleta> results = conn.Query<PeliculaCompleta>(query);
                return results;
            }
            
                               

               throw new NotImplementedException();
        }

        public bool EliminarPelicula(string titulo)
        {
            throw new NotImplementedException();
        }
    }

}
