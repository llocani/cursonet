using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stores
{
    public interface ICosasStore
    {
        public List<CosasItem> GetAllCosas();
        public CosasItem InsertCosas(CosasItem cosasItem);
        public CosasItem UpdateCosas(CosasItem cosasItem);

    }
}
