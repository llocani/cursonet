using cursonetcore.IServicios;
using Entidades;
using Stores;

namespace cursonetcore.Servicios
{
    public class CosasServicio : ICosasServicio
    {
        private readonly CosasStore _cosasStore;
        public CosasServicio(CosasStore cosasStore)
        {
            _cosasStore = cosasStore;
        }
    }
}
