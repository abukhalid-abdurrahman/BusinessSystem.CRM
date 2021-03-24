using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Npgsql;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models;
using BusinessSystem.Database.Models.BusinessObjects;

namespace BusinessSystem.Database.Contexts
{
    public class DescriptionContext : DataBaseContext, IEntityRepository<DescriptionEntityModel>
    {
        public async Task<int> CreateAsync(DescriptionEntityModel entityModel)
        {
            var query = $@"INSERT INTO public.descriptions(description) VALUES(@Description) RETURNING id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return 0;
            var descriptionId = await connection.ExecuteScalarAsync<int?>(query, entityModel);
            await connection.CloseAsync();
            return descriptionId ?? 0;
        }

        public async Task DeleteAsync(int id)
        {
            var query = $@"DELETE FROM public.descriptions
                              WHERE id = @id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, new { id });
            await connection.CloseAsync();
        }

        public async Task<List<DescriptionEntityModel>> GetAllAsync()
        {
            var query = $@"SELECT id AS Id, description AS Description FROM public.descriptions";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var data = await connection.QueryAsync<DescriptionEntityModel>(query) ?? null;
            if (data == null)
            {
                await connection.CloseAsync();
                return null;
            }
            await connection.CloseAsync();
            return data.ToList();
        }

        public async Task<DescriptionEntityModel> GetAsync(int id)
        {
            var query = $@"SELECT id AS Id, description AS Description FROM public.descriptions
                              WHERE id=@Id";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var description = await connection.QueryFirstOrDefaultAsync<DescriptionEntityModel>(query, new { id });
            await connection.CloseAsync();
            return description;
        }

        public async Task UpdateAsync(DescriptionEntityModel entityModel)
        {
            var query = $@"UPDATE public.descriptions SET description=@Description WHERE id=@Id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, entityModel);
            await connection.CloseAsync();
        }
    }
}
