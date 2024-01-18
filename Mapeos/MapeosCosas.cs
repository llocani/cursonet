using DTOs;
using Entidades;

namespace Mapeos
{
    public class MapeosCosas : IMapeosCosas
    {
        public CosasItemDto CosasItemaACosasItemDto(CosasItem CosasItem) => new CosasItemDto()
        {
            CosasDescripcion = CosasItem.CosasDescripcion,
            Id = CosasItem.Id,
        };
        public CosasItem CosasItemaDtoACosasItem(CosasItemDto CosasItem) => new CosasItem()
        {
            CosasDescripcion = CosasItem.CosasDescripcion,
            Id = CosasItem.Id??0,
        };

    }
}
