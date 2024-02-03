using Delivery.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.DTO
{


    public class DetalhesLocacaoDto
    {
        public int LocacaoId { get; set; }
        public int EntregadorId { get; set; }
        public int MotoId { get; set; }
        public EPlanoLocacao PlanoLocacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevistaTermino { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public decimal ValorDiaria { get; set; }
        public decimal ValorTotal { get; set; }
    }

}
