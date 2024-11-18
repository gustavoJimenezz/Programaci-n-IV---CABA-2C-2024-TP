using BlockBuster.manager.ModelFactories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockBuster.manager.Entidades;
using BlockBuster.manager.ModelFactories;
using Dapper;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Identity.Client;

namespace BlockBuster.manager.Repositorios
{
    public interface IRegistroActividadRepository
    {
        RegistroActividad GetRegistroActividad(int actividad_id);
        IEnumerable<RegistroActividadCompleto> GetRegistroActividadesCompleto();
        List<PeliculaCompleta> GetPeliculasAlquiladasPorGoogle(decimal googleIdentificator);
        void CrearRegistroDeActividad(RegistroActividad registroActividad);
    }

    public class RegistroActividadRepository : IRegistroActividadRepository
    {
        private string _connectionString;

        public RegistroActividadRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public RegistroActividad GetRegistroActividad(int actividad_id)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM registro_actividades WHERE actividad_id = " + actividad_id.ToString();
                RegistroActividad result = con.QuerySingle<RegistroActividad>(query, actividad_id);
                return result;
            }
        }

        public IEnumerable<RegistroActividadCompleto> GetRegistroActividadesCompleto()
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM registro_actividades";
                IEnumerable<RegistroActividadCompleto> results = conn.Query<RegistroActividadCompleto>(query);
                return results;
            }
        }

        public List<PeliculaCompleta> GetPeliculasAlquiladasPorGoogle(decimal googleIdentificator)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = @"
                SELECT p.pelicula_id, p.titulo, p.descripcion, p.fecha_publicacion
                FROM RegistroActividad AS ra
                INNER JOIN Pelicula AS p ON ra.pelicula_id = p.pelicula_id
                WHERE ra.googleIdentificator = @GoogleIdentificator";

                var peliculas = new List<PeliculaCompleta>();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@GoogleIdentificator", googleIdentificator));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            peliculas.Add(new PeliculaCompleta
                            {
                                pelicula_id = reader.GetInt32(0),
                                titulo = reader.GetString(1),
                                descripcion = reader.GetString(2),
                                fecha_publicacion = reader.GetString(3)
                            });
                        }
                    }
                }
                return peliculas;
            }
        }

        public void CrearRegistroDeActividad(RegistroActividad registroActividad)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                            INSERT INTO registro_actividades 
                            (usuario_id, actividad, descripcion, fecha, pelicula_id, googleIdentificator)  
                            VALUES (@usuario_id, @actividad, @descripcion, @fecha, @pelicula_id, @googleIdentificator)";

                conn.Execute(query, registroActividad);
            }
        }
    }
}
