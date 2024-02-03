using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Commands
{
    // Commands/CadastrarLocacaoCommand.cs
    public class CadastrarLocacaoCommand
    {
        public int IdEntregador { get; set; }
        public int IdMoto { get; set; }
        public int DuracaoDias { get; set; }
    }

}
