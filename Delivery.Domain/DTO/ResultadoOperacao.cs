using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.DTO
{


    public class ResultadoOperacao
    {
        public bool Sucesso { get; private set; }
        public string Mensagem { get; private set; }

        private ResultadoOperacao(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public static ResultadoOperacao CriarSucesso(string mensagem = null, DetalhesLocacaoDto detalhesLocacaoDto = null)
        {
            return new ResultadoOperacao(true, mensagem);
        }

        public static ResultadoOperacao CriarFalha(string mensagem)
        {
            return new ResultadoOperacao(false, mensagem);
        }
    }

}
