// Delivery.Domain/Interfaces/ILocacaoRepository.cs
using System.Threading.Tasks;
using Delivery.Domain.Commands;
using Delivery.Domain.Entities;
using Delivery.Domain.Enums;

namespace Delivery.Domain.Interface
{
    public interface ILocacaoRepository
    {
        Task<CadastrarLocacaoCommand> CadastrarLocacao(CadastrarLocacaoCommand command);
        Task<IEnumerable<Locacao>> ConsultarLocacoes();
        Task<Locacao> ConsultarLocacaoPorId(int id);
        Task AtualizarStatusLocacao(int id, StatusLocacao novoStatus);

        Task<Locacao> ObterPorId(int locacaoId);
        Task AlugarMoto(Locacao locacao);
        Task FinalizarLocacao(Locacao locacao);
        void Atualizar(Task<Locacao> locacao);
        Task Atualizar(Locacao locacao);
    }

}
