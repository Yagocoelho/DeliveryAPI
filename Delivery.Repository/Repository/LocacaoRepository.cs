using Dapper;
using Delivery.Domain.Commands;
using Delivery.Domain.Entities;
using Delivery.Domain.Enums;
using Delivery.Domain.Interface;
using System.Data.SqlClient;

namespace Delivery.Repository.Repository
{
    // Repositories/LocacaoRepository.cs
    public class LocacaoRepository : ILocacaoRepository
    {
        private string _connectionString = @"Server=(localdb)\mssqllocaldb;Database=Delivery;Trusted_Connection=True;MultipleActiveResultSets=True";

        public async Task<CadastrarLocacaoCommand> CadastrarLocacao(CadastrarLocacaoCommand command)
        {
            string queryInsert = @"INSERT INTO Locacao(IdEntregador, IdMoto, DataInicio, DataTermino, DataPrevistaTermino, ValorTotal)
                                  VALUES(@IdEntregador, @IdMoto, @DataInicio, @DataTermino, @DataPrevistaTermino, @ValorTotal)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Execute(queryInsert, new
                {
                    IdEntregador = command.IdEntregador,
                    IdMoto = command.IdMoto,
                    DataInicio = DateTime.Now, // Início é o momento atual
                    DataTermino = DateTime.Now.AddDays(command.DuracaoDias),
                    DataPrevistaTermino = DateTime.Now.AddDays(command.DuracaoDias),
                    ValorTotal = CalcularValorTotal(command.DuracaoDias)
                });
            }
            return new CadastrarLocacaoCommand();
        }

        private object CalcularValorTotal(int duracaoDias)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Locacao>> ConsultarLocacoes()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                const string sql = "SELECT * FROM Locacoes;";
                var locacoes = await connection.QueryAsync<Locacao>(sql);

                return locacoes;
            }
        }

        public async Task<Locacao> ConsultarLocacaoPorId(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                const string sql = "SELECT * FROM Locacoes WHERE Id = @Id;";
                var locacao = await connection.QueryFirstOrDefaultAsync<Locacao>(sql, new { Id = id });

                return locacao;
            }
        }

        public async Task AtualizarStatusLocacao(int id, StatusLocacao novoStatus)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    Id = id,
                    NovoStatus = novoStatus
                };

                const string sql = "UPDATE Locacoes SET Status = @NovoStatus WHERE Id = @Id;";
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public Task<Locacao> ObterPorId(int locacaoId)
        {
            throw new NotImplementedException();
        }

        public Task AlugarMoto(Locacao locacao)
        {
            throw new NotImplementedException();
        }

        public Task FinalizarLocacao(Locacao locacao)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Task<Locacao> locacao)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Locacao locacao)
        {
            throw new NotImplementedException();
        }

        // Outros métodos de acordo com a necessidade
    }
}

