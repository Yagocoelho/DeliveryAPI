using Delivery.Domain.DTO;
using Delivery.Domain.Entities;
using Delivery.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers
{

    // LocacaoController.cs

    [ApiController]
    [Route("api/[controller]")]
    public class LocacaoController : ControllerBase
    {
        private readonly ILocacaoService _locacaoService;

        public LocacaoController(ILocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        [HttpPost]
        [Route("Alugar")]
        public async Task<IActionResult> AlugarMoto([FromBody] LocacaoDto locacaoDto)
        {
            var resultado = await _locacaoService.AlugarMoto(locacaoDto);

            if (resultado.Sucesso)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        [HttpPut]
        [Route("Devolver")]
        public async Task<IActionResult> DevolverMoto([FromBody] DevolucaoDto devolucaoDto)
        {
            var resultado = await _locacaoService.DevolverMoto(devolucaoDto);

            if (resultado.Sucesso)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

        
        [HttpGet]
        [Route("consultar/{idLocacao}")]
        public async Task<IActionResult>  ConsultarLocacao(int idLocacao, DateTime dataDevolucao)
        {
            var resultado = await _locacaoService.ConsultarValorTotalLocacao(idLocacao, dataDevolucao);

            if (resultado != null)
            {
                return Ok(resultado);
            }
            else
            {
                return BadRequest(resultado);
            }
        }

    }


}
