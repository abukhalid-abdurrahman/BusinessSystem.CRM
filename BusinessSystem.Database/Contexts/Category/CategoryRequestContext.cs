using System;
using System.Threading.Tasks;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using Dapper;
using Npgsql;

namespace BusinessSystem.Database.Contexts.Category
{
    public class CategoryRequestContext : DataBaseContext, IRequestRepository<CategoryRequestModel>
    {
        public async Task<int> CreateAsync(CategoryRequestModel requestModel)
        {
            if(requestModel == null)
                throw new ArgumentNullException();
            
            var query = $@"INSERT INTO public.categories(
	                            user_id, name)
	                            VALUES (@PartnerId, @CategoryName) RETURNING id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return 0;
            var categoryId = await connection.ExecuteScalarAsync<int?>(query, requestModel);
            await connection.CloseAsync();
            return categoryId ?? 0;
        }

        public async Task UpdateAsync(CategoryRequestModel requestModel)
        {
            if(requestModel?.CategoryId == null)
                throw new ArgumentNullException();
            
            var query = $@"UPDATE public.categories SET name=@CategoryName WHERE id=@CategoryId;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, requestModel);
            await connection.CloseAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if(id < 1)
                throw new ArgumentException("Category id can not be null, or equal to zero, or be a negative number.");
            
            var query = $@"DELETE FROM public.categories
                              WHERE id = @id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, new { id });
            await connection.CloseAsync();        
        }
    }
}