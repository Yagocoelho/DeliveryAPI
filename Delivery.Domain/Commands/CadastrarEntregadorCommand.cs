using Delivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Commands
{
   
    public class CadastrarEntregadorCommand
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCNH { get; set; }
        public ETipoCNH TipoCNH { get; set; }
        public string ImagemCNH { get; set; }
    }

}
