using Delivery.Domain.Commands;
using Delivery.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class EntregadorController : ControllerBase
    {
        private readonly EntregadorService _entregadorService;

        public EntregadorController(EntregadorService entregadorService)
        {
            _entregadorService = entregadorService;
        }

        
        [HttpPost]
        [Route("CadastrarEntregador")]
        public async Task<IActionResult> CadastrarEntregador([FromBody] CadastrarEntregadorCommand command)
        {
             
            return Ok(await _entregadorService.CadastrarEntregador(command));
        }
        // Implementar outros métodos de acordo com os casos de uso do entregador
    }

}
