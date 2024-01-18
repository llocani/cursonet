using basededatos;
using Microsoft.EntityFrameworkCore.Storage;
using Stores.IContext;

namespace Data.Queries
{
    public class TransactionQuery : ITransactionQuery
    {
        private readonly CosasContext _serviceContext;
        public TransactionQuery(CosasContext serviceContext)
        {
            _serviceContext = serviceContext;
        }

        public void BeginTransaction()
        {
             _serviceContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            GetCurrentTransaction().CommitAsync();
        }

        public void ConfirmChanges()
        {
            _serviceContext.SaveChanges();
        }

        public string CreateTransactionSavepoint(string savePointName)
        {
            GetCurrentTransaction().CreateSavepoint(savePointName);
            return savePointName;
        }

        public void RollbackToSavepoint(string savePointName)
        {
            GetCurrentTransaction().RollbackToSavepoint(savePointName);
        }

        public void RollbackTransaction()
        {
            GetCurrentTransaction().Rollback();
        }
        private IDbContextTransaction GetCurrentTransaction()
        {
            var currentTransaction = _serviceContext.Database.CurrentTransaction;
            if (currentTransaction != null)
            {
                return currentTransaction;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}