using Dapper;
using Delivery.Domain.Commands;
using Delivery.Domain.Entities;
using Delivery.Domain.Interface;
using System.Data.SqlClient;

namespace Delivery.Repository.Repository
{
    // Repositories/MotoRepository.cs
    public class MotoRepository : IMotoRepository
    {
        private string _connectionString = @"Server=(localdb)\mssqllocaldb;Database=Delivery;Trusted_Connection=True;MultipleActiveResultSets=True";

        public async Task<CadastrarMotoCommand> CadastrarMoto(CadastrarMotoCommand command)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    Ano = command.Ano,
                    Modelo = command.Modelo,
                    Placa = command.Placa
                };

                const string sql = "INSERT INTO Moto (Ano, Modelo, Placa) VALUES (@Ano, @Modelo, @Placa);";
                await connection.ExecuteAsync(sql, parameters);
            }

            return command;
        }

        public async Task<IEnumerable<Moto>> ConsultarMotos(ConsultarMotosCommand command)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                const string sql = "SELECT * FROM Moto WHERE Placa = @Placa;";
                var result = await connection.QueryAsync<Moto>(sql, new { Placa = command.Placa });

                return result;
            }
        }

        public async Task<ModificarMotoCommand> ModificarMoto(ModificarMotoCommand command)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new
                {
                    Identificador = command.Identificador,
                    NovaPlaca = command.NovaPlaca
                };

                const string sql = "UPDATE Moto SET Placa = @NovaPlaca WHERE Identificador = @Identificador;";
                await connection.ExecuteAsync(sql, parameters);
            }

            return command;
        }

        public Task<Moto> ObterMotoDisponivel()
        {
            throw new NotImplementedException();
        }

        public async Task RemoverMoto(RemoverMotoCommand command)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                const string sql = "DELETE FROM Moto WHERE Identificador = @Identificador;";
                await connection.ExecuteAsync(sql, new { Identificador = command.Identificador });
            }
        }
    }
}
