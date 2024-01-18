using Entidades;

namespace Stores
{
    public interface ISessionStore
    {
        public List<SessionItem> GetAllSession();
        public SessionItem InsertSession(SessionItem SessionItem);
        public SessionItem UpdateSession(SessionItem SessionItem);
    }
}
