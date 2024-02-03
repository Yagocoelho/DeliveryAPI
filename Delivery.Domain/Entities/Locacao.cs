using Delivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Entities
{
    // Entities/Locacao.cs
    public class Locacao
    {
        public int Identificador { get; set; }
        public int IdEntregador { get; set; }
        public int IdMoto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public DateTime DataPrevistaTermino { get; set; }
        public decimal ValorTotal { get; set; }
        public StatusLocacao Status { get; set; }
        public EPlanoLocacao PlanoLocacao { get; set; }
        public DateTime DataDevolucao { get; set; }
    }

}
