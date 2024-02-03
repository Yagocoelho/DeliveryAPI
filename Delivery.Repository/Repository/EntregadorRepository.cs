using Dapper;
using Delivery.Domain.Commands;
using Delivery.Domain.Entities;
using Delivery.Domain.Enums;
using Delivery.Domain.Interfaces;
using System.Data.SqlClient;

namespace Delivery.Repository.Repository
{
    // Repositories/EntregadorRepository.cs
    public class EntregadorRepository : IEntregadorRepository
    {
        private string conexao = @"Server=(localdb)\mssqllocaldb;Database=Delivery;Trusted_Connection=True;MultipleActiveResultSets=True";

        public async Task<string> CadastrarEntregador(CadastrarEntregadorCommand command)
        {
            string queryInsert = @"INSERT INTO Entregador(Nome, CNPJ, DataNascimento, NumeroCNH, TipoCNH )
                                  VALUES(@Nome, @CNPJ, @DataNascimento, @NumeroCNH, @TipoCNH )";
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Execute(queryInsert, new
                {
                    Nome = command.Nome,
                    CNPJ = command.CNPJ,
                    DataNascimento = command.DataNascimento,
                    NumeroCNH = command.NumeroCNH,
                    TipoCNH = command.TipoCNH
                });
            }
            return "Entregador cadastrado com sucesso";
        }

        public Task<bool> EntregadorTemCNH(int entregadorId, ETipoCNH tipoCNH)
        {
            throw new NotImplementedException();
        }

        public Task<Entregador> ObterPorId(int entregadorId)
        {
            throw new NotImplementedException();
        }

        Task<CadastrarEntregadorCommand> IEntregadorRepository.CadastrarEntregador(CadastrarEntregadorCommand command)
        {
            throw new NotImplementedException();
        }
    }
        // métodos de consulta, modificação e remoção
}
