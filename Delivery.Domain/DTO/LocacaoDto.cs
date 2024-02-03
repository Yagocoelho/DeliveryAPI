using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.DTO
{
    public class LocacaoDto
    {
        public int EntregadorId { get; set; }
        public int PlanoLocacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevistaTermino { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int MotoId { get; set; }

    }

}
