using Delivery.Domain.Commands;
using Delivery.Domain.DTO;
using Delivery.Domain.Entities;
using Delivery.Domain.Enums;
using Delivery.Domain.Interface;
using Delivery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Service.Services
{
    // Services/LocacaoService.cs
    // LocacaoService.cs
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IMotoRepository _motoRepository;

        public LocacaoService(ILocacaoRepository locacaoRepository, IEntregadorRepository entregadorRepository, IMotoRepository motoRepository)
        {
            _locacaoRepository = locacaoRepository;
            _entregadorRepository = entregadorRepository;
            _motoRepository = motoRepository;
        }

        public async Task<ResultadoOperacao> AlugarMoto(LocacaoDto locacaoDto)
        {
            // Lógica de validação e aluguel

            var entregador = await _entregadorRepository.ObterPorId(locacaoDto.EntregadorId);
            if (entregador == null || entregador.TipoCNH != ETipoCNH.A)
                return ResultadoOperacao.CriarFalha("Entregador não habilitado para locação.");

            var motoDisponivel = await _motoRepository.ObterMotoDisponivel();
            if (motoDisponivel == null)
                return ResultadoOperacao.CriarFalha("Não há motos disponíveis para locação.");

            // Mapear LocacaoDto para Locacao e salvar no banco
            var locacao = new Locacao
            {
                IdEntregador = locacaoDto.EntregadorId,
                IdMoto = motoDisponivel.Identificador,
                PlanoLocacao = (EPlanoLocacao)locacaoDto.PlanoLocacao,
                DataInicio = DateTime.Now, // Início é o momento atual
                DataPrevistaTermino = locacaoDto.DataPrevistaTermino,
                // Outras atribuições necessárias
            };

            await _locacaoRepository.AlugarMoto(locacao);

            return ResultadoOperacao.CriarSucesso("Locação realizada com sucesso.");
        }

        public async Task<ResultadoOperacao> FinalizarLocacao(int locacaoId, DateTime dataDevolucao)
        {
            // Lógica para finalizar a locação
            var locacao = await _locacaoRepository.ObterPorId(locacaoId);
            if (locacao == null)
                return ResultadoOperacao.CriarFalha("Locação não encontrada.");

            // Lógica para calcular custos adicionais, multas, etc.

            // Atualizar informações da locação no banco de dados
            await _locacaoRepository.FinalizarLocacao(locacao);

            return ResultadoOperacao.CriarSucesso("Locação finalizada com sucesso.");
        }

        public async Task<decimal> ConsultarValorTotalLocacao(int locacaoId, DateTime dataDevolucao)
        {
            // Lógica para consultar o valor total da locação
            var locacao = await _locacaoRepository.ObterPorId(locacaoId);
            if (locacao == null)
                throw new ArgumentException("Locação não encontrada.");

            // Lógica para calcular custos adicionais, multas, etc.

            // Retornar o valor total
            return CalculaValorTotalLocacao(locacao, dataDevolucao);
        }
        private decimal ObterValorDiariaAdicional(EPlanoLocacao plano)
        {
            // Lógica para obter o valor da diária adicional conforme o plano
            switch (plano)
            {
                case EPlanoLocacao.SeteDias:
                    return 50.00m; // Exemplo de valor para o plano de 7 dias
                case EPlanoLocacao.QuinzeDias:
                    return 50.00m; // Exemplo de valor para o plano de 15 dias
                case EPlanoLocacao.TrintaDias:
                    return 50.00m; // Exemplo de valor para o plano de 30 dias
                default:
                    throw new ArgumentException("Plano de locação inválido.");
            }
        }

        private decimal CalcularPercentualMulta(EPlanoLocacao plano)
        {
            // Lógica para obter o percentual de multa conforme o plano
            switch (plano)
            {
                case EPlanoLocacao.SeteDias:
                    return 20.00m; // Exemplo de percentual de multa para o plano de 7 dias
                case EPlanoLocacao.QuinzeDias:
                    return 40.00m; // Exemplo de percentual de multa para o plano de 15 dias
                case EPlanoLocacao.TrintaDias:
                    return 60.00m; // Exemplo de percentual de multa para o plano de 30 dias
                default:
                    throw new ArgumentException("Plano de locação inválido.");
            }
        }

        private decimal ObterValorDiariaPeloPlano(EPlanoLocacao plano)
        {
            // Lógica para obter o valor da diária conforme o plano
            switch (plano)
            {
                case EPlanoLocacao.SeteDias:
                    return 30.00m; // Exemplo de valor para o plano de 7 dias
                case EPlanoLocacao.QuinzeDias:
                    return 28.00m; // Exemplo de valor para o plano de 15 dias
                case EPlanoLocacao.TrintaDias:
                    return 22.00m; // Exemplo de valor para o plano de 30 dias
                default:
                    throw new ArgumentException("Plano de locação inválido.");
            }
        }
        private decimal CalculaValorTotalLocacao(Locacao locacao, DateTime dataDevolucao)
        {
            // Lógica para calcular o valor total
            // Considerando planos de 7, 15 e 30 dias
            int diasLocados = (dataDevolucao - locacao.DataInicio).Days;
            int diasPrevistos = (locacao.DataPrevistaTermino - locacao.DataInicio).Days;

            decimal valorDiaria = ObterValorDiariaPeloPlano(locacao.PlanoLocacao);
            decimal valorTotal = 0;

            if (diasLocados < diasPrevistos)
            {
                // Aplicar multa conforme as regras estabelecidas
                decimal percentualMulta = CalcularPercentualMulta(locacao.PlanoLocacao);
                decimal valorMulta = valorDiaria * diasLocados * (percentualMulta / 100);
                valorTotal = valorDiaria * diasLocados + valorMulta;
            }
            else
            {
                // Cobrar diárias adicionais conforme as regras
                int diasAdicionais = diasLocados - diasPrevistos;
                decimal valorDiariasAdicionais = ObterValorDiariaAdicional(locacao.PlanoLocacao);
                valorTotal = valorDiaria * diasPrevistos + valorDiariasAdicionais * diasAdicionais;

                // Cobrar taxa adicional se a devolução for posterior à data prevista
                if (dataDevolucao > locacao.DataPrevistaTermino)
                {
                    int diasAtraso = (dataDevolucao - locacao.DataPrevistaTermino).Days;
                    decimal valorDiariasAtraso = ObterValorDiariaAtraso();
                    valorTotal += valorDiariasAtraso * diasAtraso;
                }
            }

            return valorTotal;
        }

        private decimal ObterValorDiariaAtraso()
        {
            // Valor fixo por diária de atraso
            return 50.00m;
        }
        public async Task<ResultadoOperacao> DevolverMoto(DevolucaoDto devolucaoDto)
        {
            // Obter a Locacao de forma assíncrona
            var locacaoTask = _locacaoRepository.ObterPorId(devolucaoDto.LocacaoId);
            var locacao = await locacaoTask;

            if (locacao == null)
            {
                return ResultadoOperacao.CriarFalha("Locação não encontrada.");
            }

            // Lógica para calcular valor total da locação considerando a devolução
            decimal valorTotal = CalcularValorTotalComDevolucao(locacao, devolucaoDto.DataDevolucao);

            // Realiza a devolução atualizando informações na entidade
            locacao.DataDevolucao = devolucaoDto.DataDevolucao;
            locacao.ValorTotal = valorTotal;

            // Atualiza a entidade no repositório
            await _locacaoRepository.Atualizar(locacao);

            return ResultadoOperacao.CriarSucesso($"Locação devolvida com sucesso. Valor total: {valorTotal:C}");
        }
        private decimal CalcularValorTotalComDevolucao(Task<Locacao> locacao, DateTime dataDevolucao)
        {
            throw new NotImplementedException();
        }

        public ResultadoOperacao ConsultarLocacao(int idLocacao)
        {
            var locacao = _locacaoRepository.ObterPorId(idLocacao);

            if (locacao == null)
            {
                return ResultadoOperacao.CriarFalha("Locação não encontrada.");
            }

            var detalhesLocacaoDto = CriarDetalhesLocacaoDto(locacao.Result);

            return ResultadoOperacao.CriarSucesso("Consulta realizada com sucesso.", detalhesLocacaoDto);
        }

        private DetalhesLocacaoDto CriarDetalhesLocacaoDto(Locacao locacao)
        {
            // Lógica para mapear a entidade Locacao para DetalhesLocacaoDto
            return new DetalhesLocacaoDto
            {
                LocacaoId = locacao.Identificador,
                EntregadorId = locacao.IdEntregador,
                MotoId = locacao.IdMoto,
                PlanoLocacao = locacao.PlanoLocacao,
                DataInicio = locacao.DataInicio,
                DataPrevistaTermino = locacao.DataPrevistaTermino,
                DataDevolucao = locacao.DataDevolucao,
                ValorDiaria = ObterValorDiariaPeloPlano(locacao.PlanoLocacao),
                ValorTotal = locacao.ValorTotal
                // Adicione outras propriedades conforme necessário
            };
        }

        private decimal CalcularValorTotalComDevolucao(Locacao locacao, DateTime dataDevolucao)
        {
            // Lógica para calcular o valor total considerando a devolução
            // Considere também a aplicação de multas ou descontos, se necessário

            var valorDiaria = ObterValorDiariaPeloPlano(locacao.PlanoLocacao);

            if (dataDevolucao < locacao.DataPrevistaTermino)
            {
                // Aplicar multa por devolução antecipada (exemplo: 10% do valor diário)
                decimal multa = valorDiaria * 0.10m;

                return locacao.ValorTotal + multa;
            }
            else if (dataDevolucao > locacao.DataPrevistaTermino)
            {
                // Aplicar taxa adicional por devolução após a data prevista
                decimal taxaAdicionalDiaria = 50.00m; // Exemplo de taxa adicional por dia

                int diasAtraso = (int)(dataDevolucao - locacao.DataPrevistaTermino).TotalDays;

                return locacao.ValorTotal + (taxaAdicionalDiaria * diasAtraso);
            }

            // Se a devolução estiver dentro do prazo previsto, não há custos adicionais
            return locacao.ValorTotal;
        }


    }

}


