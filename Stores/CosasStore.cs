using Entidades;
using Stores.IContext;

namespace Stores
{
    public class CosasStore : ICosasStore
    {
        private readonly ITransactionQuery _transactionQuery;
        private readonly IEntityQuery<CosasItem> _cosasQuery;
        public CosasStore(ITransactionQuery transactionQuery,
                         IEntityQuery<CosasItem> entityQuery)
        {
            _transactionQuery = transactionQuery;
            _cosasQuery = entityQuery;
        }
        public List<CosasItem> GetAllCosas()
        {
            return _cosasQuery.GetAll().ToList();
        }

        public CosasItem InsertCosas(CosasItem cosasItem)
        {
            _cosasQuery.AddItem(cosasItem);
            return cosasItem;
        }

        public CosasItem UpdateCosas(CosasItem cosasItem)
        {
            _cosasQuery.UpdateItem(cosasItem);
            return cosasItem;
        }
    }
}
