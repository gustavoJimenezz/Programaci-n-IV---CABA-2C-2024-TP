using BlockBuster.manager.Repositorios;
using System;
using BlockBuster.manager.Entidades;
using BlockBuster.manager.ModelFactories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBuster.manager.Manager
{
    public interface IRegistroActividadManager
    {
        IEnumerable<RegistroActividadCompleto> GetRegistroActividades();
        RegistroActividad GetRegistroActividad(int actividad_id);
        void CrearRegistroDeActividad(RegistroActividad registroActividad);
    }

    public class RegistroActividadManager : IRegistroActividadManager
    {
        private IRegistroActividadRepository _repo;
        public RegistroActividadManager(IRegistroActividadRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<RegistroActividadCompleto> GetRegistroActividades()
        {
            return _repo.GetRegistroActividadesCompleto(); ;
        }

        public RegistroActividad GetRegistroActividad(int actividad_id)
        {
            var registro = _repo.GetRegistroActividad(actividad_id);

            return registro;
        }

        public void  CrearRegistroDeActividad(RegistroActividad registroActividad)
        {
            _repo.CrearRegistroDeActividad(registroActividad);
        }

    }

}
