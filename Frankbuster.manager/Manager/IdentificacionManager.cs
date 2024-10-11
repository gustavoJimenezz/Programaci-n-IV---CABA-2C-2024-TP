using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using BlockBuster.manager.Entidades;
using System.Security.Cryptography;
using static System.Resources.ResXFileRef;
using BlockBuster.manager.Conexion;

namespace BlockBuster.manager.Manager
{
    public class IdentificacionManager
    {
        private readonly ConexionDB _conexion;

        public IdentificacionManager(ConexionDB conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Valida credenciales obtenidas del formulario post.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="contra"></param>
        /// <returns>objeto de tipo Identificacion/ null si no encuentra resultados </returns>
        /// 
        public Identificacion ValidarCredenciales(string email, string contra)
        {
            var contraHash = HashPassword(contra);
            var sqlQuery = "SELECT * FROM identificacion WHERE email = @Email AND contra_hash = @ContraHash";
            //parametros anonimos
            var parametros = new { Email = email, ContraHash = contraHash };

            Identificacion resultado = _conexion.ObtenerRegistro<Identificacion>(sqlQuery, parametros);
            return resultado;
        }

        /// <summary>
        /// Genera un hash de la contraseña utilizando el algoritmo SHA256.
        /// </summary>
        /// <param name="contraseña">La contraseña en texto plano que se desea hashear.</param>
        /// <returns>Un array de bytes que contiene el hash de la contraseña.</returns>
        private byte[] HashPassword(string contraseña)
        {
            byte[] bytesContraseña = System.Text.Encoding.UTF8.GetBytes(contraseña);
            var algoritmoHash = System.Security.Cryptography.HashAlgorithm.Create("SHA256");
            byte[] bytesHash = algoritmoHash.ComputeHash(bytesContraseña);
            return bytesHash;

        }
    }
}
