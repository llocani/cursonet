using DTOs;
using Mapeos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stores;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cursonetcore.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class CosasController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICosasStore _cosasStore;
        private readonly IMapeosCosas _mapeosCosas;
        public CosasController(ILogger<CosasController> logger, ICosasStore cosasStore, IMapeosCosas mapeosCosas)
        {
            _logger = logger;
            _cosasStore = cosasStore;
            _mapeosCosas = mapeosCosas;
        }
        // GET: api/<CosasController>
        [HttpGet]
        public IEnumerable<CosasItemDto> GetAllCosas()
        {
            var cosas = _cosasStore.GetAllCosas();
            var returnvar = cosas.Select(cosa => _mapeosCosas.CosasItemaACosasItemDto(cosa));
            return returnvar;
        }

        [HttpPost]
        [Authorize(Policy = "AdminRole")]
        public CosasItemDto InsertCosas(CosasItemDto cosasItemDto)
        {
            var cosasItem = _mapeosCosas.CosasItemaDtoACosasItem(cosasItemDto);
            var cosasItemInsertada = _cosasStore.InsertCosas(cosasItem);
            var resultVar = _mapeosCosas.CosasItemaACosasItemDto(cosasItemInsertada);
            return resultVar;
        }

        [HttpPost]
        [Authorize(Policy = "AdminRole")]
        public CosasItemDto UpdateCosas(CosasItemDto cosasItemDto)
        {
            var cosasItem = _mapeosCosas.CosasItemaDtoACosasItem(cosasItemDto);
            var cosasItemInsertada = _cosasStore.UpdateCosas(cosasItem);
            var resultVar = _mapeosCosas.CosasItemaACosasItemDto(cosasItemInsertada);
            return resultVar;
        }
    }
}
