using Delivery.Domain.Commands;
using Delivery.Domain.Interfaces;

namespace Delivery.Service.Services
{
    public class EntregadorService
    {
        private readonly IEntregadorRepository _entregadorRepository;

        public EntregadorService(IEntregadorRepository entregadorRepository)
        {
            _entregadorRepository = entregadorRepository;
        }

        public async Task<CadastrarEntregadorCommand> CadastrarEntregador(CadastrarEntregadorCommand command)
        {
            // Lógica de validação e regras de negócio
            // ...

            return await _entregadorRepository.CadastrarEntregador(command);
        }
    }
}


