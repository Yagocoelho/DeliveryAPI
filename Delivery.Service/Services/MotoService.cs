using Delivery.Domain.Commands;
using Delivery.Domain.Entities;
using Delivery.Domain.Interface;

namespace Delivery.Service.Services
{

    // Services/MotoService.cs
    public class MotoService: IMotoService
    {
        private readonly IMotoRepository _motoRepository;

        public MotoService(IMotoRepository motoRepository)
        {
            _motoRepository = motoRepository;
        }

        public async Task<CadastrarMotoCommand> CadastrarMoto(CadastrarMotoCommand command)
        {
            // Lógica de validação e regras de negócio
            // ...

            return await _motoRepository.CadastrarMoto(command);
        }

        public async Task<IEnumerable<Moto>> ConsultarMotos(ConsultarMotosCommand command)
        {
            // Lógica de consulta e filtragem
            // ...

            return await _motoRepository.ConsultarMotos(command);
        }

        public async Task<ModificarMotoCommand> ModificarMoto(ModificarMotoCommand command)
        {
            // Lógica de modificação
            // ...

            return await _motoRepository.ModificarMoto(command);
        }

        public async Task RemoverMoto(RemoverMotoCommand command)
        {
            // Lógica de remoção
            // ...

            await _motoRepository.RemoverMoto(command);
        }
    }


}
