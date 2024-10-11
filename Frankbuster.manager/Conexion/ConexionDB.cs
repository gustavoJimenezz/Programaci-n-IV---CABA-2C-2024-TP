using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BlockBuster.manager.Conexion
{
    public class ConexionDB
    {
        private readonly string StringConexion;

        public ConexionDB(String conexion)
        {
            this.StringConexion = conexion;
        }
        
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(StringConexion);
        }

        public T ObtenerRegistro<T>(string sqlQuery, object parametros)
        {
            using (var conexion = this.ObtenerConexion())
            {
                T resultado = conexion.QuerySingleOrDefault<T>(sqlQuery, parametros);
                return resultado;
            }
        }

        public IEnumerable<T> EjecutarConsulta<T>(string sqlQuery, object parametros = null)
        {
            using (var conexion = this.ObtenerConexion())
            {
                return conexion.Query<T>(sqlQuery, parametros);
            }
        }

    }
}
