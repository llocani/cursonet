using DTOs;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapeos
{
    public interface IMapeosCosas
    {
        public CosasItemDto CosasItemaACosasItemDto(CosasItem CosasItem);
        public CosasItem CosasItemaDtoACosasItem(CosasItemDto CosasItem);
    }
}
