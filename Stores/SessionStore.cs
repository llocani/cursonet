
using Entidades;
using Stores.IContext;

namespace Stores
{
    public class SessionStore : ISessionStore
    {
        private readonly ITransactionQuery _transactionQuery;
        private readonly IEntityQuery<SessionItem> _SessionQuery;
        public SessionStore(ITransactionQuery transactionQuery,
                         IEntityQuery<SessionItem> SessionQuery)
        {
            _transactionQuery = transactionQuery;
            _SessionQuery = SessionQuery;
        }
        public List<SessionItem> GetAllSession()
        {
            return _SessionQuery.GetAll().ToList();
        }

        public SessionItem InsertSession(SessionItem SessionQuery)
        {
            _SessionQuery.AddItem(SessionQuery);
            return SessionQuery;
        }

        public SessionItem UpdateSession(SessionItem SessionQuery)
        {
            _SessionQuery.UpdateItem(SessionQuery);
            return SessionQuery;
        }
    }
}
