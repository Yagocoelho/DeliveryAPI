using Delivery.Domain.DTO;
using Delivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Interface
{
    public interface ILocacaoService
    {
        Task<ResultadoOperacao> AlugarMoto(LocacaoDto locacaoDto);
        Task<ResultadoOperacao> FinalizarLocacao(int locacaoId, DateTime dataDevolucao);
        Task<decimal> ConsultarValorTotalLocacao(int locacaoId, DateTime dataDevolucao);
        Task<ResultadoOperacao> DevolverMoto(DevolucaoDto devolucaoDto);
    }
}


