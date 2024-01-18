
using Entidades;
using Stores.IContext;

namespace Stores
{
    public class UserStore : IUserStore
    {
        private readonly ITransactionQuery _transactionQuery;
        private readonly IEntityQuery<UserItem> _userQuery;
        public UserStore(ITransactionQuery transactionQuery,
                         IEntityQuery<UserItem> userQuery)
        {
            _transactionQuery = transactionQuery;
            _userQuery = userQuery;
        }
        public List<UserItem> GetAllUser()
        {
            return _userQuery.GetAll().ToList();
        }

        public UserItem InsertUser(UserItem userQuery)
        {
            _userQuery.AddItem(userQuery);
            return userQuery;
        }

        public UserItem UpdateUser(UserItem userQuery)
        {
            _userQuery.UpdateItem(userQuery);
            return userQuery;
        }
    }
}
