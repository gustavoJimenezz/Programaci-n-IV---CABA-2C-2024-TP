using BlockBuster.manager.Entidades;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.SqlClient;
using BlockBuster.manager.Conexion;

namespace BlockBuster.manager.Manager
{
    public class UsuarioManager
    {
        private readonly ConexionDB _conexion;

        public UsuarioManager(ConexionDB conexion)
        {
            _conexion = conexion;
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            var sql = "SELECT * FROM usuario";
            return _conexion.EjecutarConsulta<Usuario>(sql);
        }
    }
}
