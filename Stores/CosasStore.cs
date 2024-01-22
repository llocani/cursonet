using Entidades;
using Stores.IContext;

namespace Stores
{
    public class CosasStore : ICosasStore
    {
        private readonly IEntityQuery<CosasItem> _cosasQuery;
        public CosasStore(
                         IEntityQuery<CosasItem> entityQuery)
        {
            _cosasQuery = entityQuery;
        }
        public List<CosasItem> GetAllCosas()
        {
            return _cosasQuery.GetAll().ToList();
        }

        public CosasItem InsertCosas(CosasItem cosasItem)
        {
            var nuevaCosa = _cosasQuery.AddItem(cosasItem);
            return nuevaCosa;
        }

        public CosasItem UpdateCosas(CosasItem cosasItem)
        {
            _cosasQuery.UpdateItem(cosasItem);
            return cosasItem;
        }
    }
}
