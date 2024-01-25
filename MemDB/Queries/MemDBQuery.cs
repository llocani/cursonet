using Entidades;
using Stores.IContext;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MemDB.Queries
{
    public class MemDBQuery<Item> : IEntityQuery<Item>
    {
        static List<Item> _lista = new List<Item>();


        public Item AddItem(Item item)
        {
            //if (item != null) {
            //    var type = item.GetType();
            //    var maxValue = Activator.CreateInstance(Itemype);
            //    IComparable comparable = (IComparable)maxValue;
            //    foreach (var itemDeLista in _lista)
            //    {
            //        var myPropertyInt = itemDeLista.GetType().GenericTypeArguments[0].GetProperty("Id");
            //        var id = myPropertyInt.GetValue(item);
                    
            //        if (comparable.CompareTo(id)<0) maxValue = id;
            //    }
            //    var myProperty = item.GetType().GenericTypeArguments[0].GetProperty("Id");
            //    myProperty.SetValue(item, maxValue++);
            //    _lista.Add(item);
            //}
            _lista.Add(item);
            return item;
        }

        public void AddItems(List<Item>itemsList)
        {
            _lista.AddRange(itemsList);
        }

        public void DeleteItem(Item item)
        {
            _lista.Remove(item);
        }


        public void DeleteItems(List<Item>itemsList)
        {
            _lista.RemoveAll(x => itemsList.Contains(x));
        }

        public IQueryable<Item>GetAll()
        {
            return _lista.AsQueryable();
        }

        public IQueryable<Item>GetAllUntracked()
        {
            throw new NotImplementedException();
        }


        public IQueryable<Item>GetByCriteria(Expression<Func<Item, bool>> funcPred)
        {
            throw new NotImplementedException();
            //_lista.Where(x => funcPred(x));
        }

        public IQueryable<Item>GetByCriteriaUntracked(Expression<Func<Item, bool>> funcPred)
        {
            throw new NotImplementedException();
        }


        public Item? GetFirstOrDefault(Expression<Func<Item, bool>> funcPred)
        {
            throw new NotImplementedException();
        }


        //public T Create(CosasDto user)
        //{
        //    T userItem = user.ToUserItem();
        //    if (_users.Count > 0)
        //    {
        //        userItem.Id = _users.Max(x => x.Id) + 1;
        //    }
        //    else
        //    {
        //        userItem.Id = 1;
        //    }
        //    _users.Add(userItem);
        //    return userItem;
        //}

        //public void Delete(long id)
        //{
        //    throw new NotImplementedException();
        //}

        //public T? GetForId(long id)
        //{
        //    return _users.Where(u =>  u.Id == id).FirstOrDefault();
        //}

        //public T? GetForUsername(string username)
        //{
        //    return _users.Where(u => u.Username == username).FirstOrDefault();
        //}


        public List<Item>ToList(IQueryable<Item>itemsList)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            var index = _lista.IndexOf(item);
            _lista.RemoveAt(index);
            _lista.Insert(index, item);
        }

        IQueryable<Item>IEntityQuery<Item>.GetAll()
        {
            throw new NotImplementedException();
        }

        IQueryable<Item>IEntityQuery<Item>.GetAllUntracked()
        {
            throw new NotImplementedException();
        }

        Item? IEntityQuery<Item>.GetFirstOrDefaultUntracked(Expression<Func<Item, bool>> funcPred)
        {
            throw new NotImplementedException();
        }

        void IEntityQuery<Item>.UpdateItems(List<Item>itemsList)
        {
            throw new NotImplementedException();
        }
    }
}
