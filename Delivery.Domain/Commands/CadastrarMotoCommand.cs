using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Commands
{
    // Commands/CadastrarMotoCommand.cs
    public class CadastrarMotoCommand
    {
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }

    // Commands/ConsultarMotosCommand.cs
    public class ConsultarMotosCommand
    {
        public string Placa { get; set; }
    }

    // Commands/ModificarMotoCommand.cs
    public class ModificarMotoCommand
    {
        public int Identificador { get; set; }
        public string NovaPlaca { get; set; }
    }

    // Commands/RemoverMotoCommand.cs
    public class RemoverMotoCommand
    {
        public int Identificador { get; set; }
    }

}
