using Delivery.Domain.Commands;
using Delivery.Service.Services;
using Microsoft.AspNetCore.Mvc;


namespace Delivery.Controllers
{
    // Controllers/MotoController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly MotoService _motoService;

        public MotoController(MotoService motoService)
        {
            _motoService = motoService;
        }

        
        [HttpPost]
        [Route("CadastrarMoto")]
        public async Task<IActionResult> CadastrarMoto([FromBody] CadastrarMotoCommand command)
        {
            var result = await _motoService.CadastrarMoto(command);
            return Ok(result);
        }


        [HttpGet]
        [Route("ConsultarMotos")]
        public async Task<IActionResult> ConsultarMotos([FromQuery] ConsultarMotosCommand command)
        {
            var result = await _motoService.ConsultarMotos(command);
            return Ok(result);
        }


        [HttpPut]
        [Route("ModificarMoto")]
        public async Task<IActionResult> ModificarMoto([FromBody] ModificarMotoCommand command)
        {
            var result = await _motoService.ModificarMoto(command);
            return Ok(result);
        }


        [HttpDelete]
        [Route("RemoverMoto")]
        public async Task<IActionResult> RemoverMoto([FromBody] RemoverMotoCommand command)
        {
            await _motoService.RemoverMoto(command);
            return NoContent();
        }
    }

}
