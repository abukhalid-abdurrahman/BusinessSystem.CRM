using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.DataTransferObjects.Response;
using Dapper;
using Npgsql;

namespace BusinessSystem.Database.Contexts.Category
{
    public class CategoryResponseContext : DataBaseContext, IResponseRepository<CategoryResponseModel>
    {
        public async Task<List<CategoryResponseModel>> GetAllAsync()
        {
            var query = $@"SELECT id AS CategoryId, name AS CategoryName, insertdate AS CreateDate FROM public.categories";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var data = await connection.QueryAsync<CategoryResponseModel>(query) ?? null;
            if (data == null)
            {
                await connection.CloseAsync();
                return null;
            }
            await connection.CloseAsync();
            return data.ToList();
        }

        public async Task<CategoryResponseModel> GetAsync(int id)
        {
            if(id < 1)
                throw new ArgumentException("Category id can not be null, or equal to zero, or be a negative number.");
            
            var query = $@"SELECT id AS CategoryId, name AS CategoryName, insertdate AS CreateDate FROM public.categories WHERE id=@id";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var category = await connection.QueryFirstOrDefaultAsync<CategoryResponseModel>(query, new { id }) ?? null;
            if (category == null)
            {
                await connection.CloseAsync();
                return null;
            }
            await connection.CloseAsync();
            return category;
        }

        public async Task<List<CategoryResponseModel>> GetPartnerCategories(int partnerId)
        {
            if(partnerId < 1)
                throw new ArgumentException("Partner id can not be null, or equal to zero, or be a negative number.");
            var query = $@"SELECT id AS CategoryId, name AS CategoryName, insertdate AS CreateDate FROM public.categories WHERE user_id=@partnerId";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != ConnectionState.Open)
                return default;
            var data = await connection.QueryAsync<CategoryResponseModel>(query, new { partnerId }) ?? null;
            if (data == null)
            {
                await connection.CloseAsync();
                return null;
            }

            await connection.CloseAsync();
            return data.ToList();
        }
    }
}