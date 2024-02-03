using Delivery.Domain.Commands;
using Delivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Interface
{
    public interface IMotoService
    {
        Task<CadastrarMotoCommand> CadastrarMoto(CadastrarMotoCommand command);
        Task<IEnumerable<Moto>> ConsultarMotos(ConsultarMotosCommand command);
        Task<ModificarMotoCommand> ModificarMoto(ModificarMotoCommand command);
        Task RemoverMoto(RemoverMotoCommand command);
    }
}
