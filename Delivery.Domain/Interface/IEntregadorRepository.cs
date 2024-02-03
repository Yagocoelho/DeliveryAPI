using Delivery.Domain.Commands;
using Delivery.Domain.Entities;
using Delivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Interfaces
{
    public interface IEntregadorRepository
    {
        Task<CadastrarEntregadorCommand> CadastrarEntregador(CadastrarEntregadorCommand command);
        Task<Entregador> ObterPorId(int entregadorId);
        Task<bool> EntregadorTemCNH(int entregadorId, ETipoCNH tipoCNH);
    }

}
