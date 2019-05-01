using Dapper;
using Domain.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace Infrastructure.Data.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("Database:ConnectionString");
        }

        public void DropEntity(string name)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
                conexao.Execute($"drop table dbo.{name}");
        }
    }
}
