using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.BusinessObjects;
using Dapper;
using Npgsql;

namespace BusinessSystem.Database.Contexts.Category
{
    public class CategoryContext : DataBaseContext, IEntityRepository<CategoryEntityModel>
    {
        public async Task<int> CreateAsync(CategoryEntityModel entityModel)
        {
            var query = $@"INSERT INTO public.categories(
	                            user_id, name, removed, insertdate)
	                            VALUES (@UserId, @Name, @Removed, @InsertDate) RETURNING id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return 0;
            var categoryId = await connection.ExecuteScalarAsync<int?>(query, entityModel);
            await connection.CloseAsync();
            return categoryId ?? 0;
        }

        public async Task DeleteAsync(int id)
        {
            var query = $@"DELETE FROM public.categories
                              WHERE id = @id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, new { id });
            await connection.CloseAsync();
        }

        public async Task<List<CategoryEntityModel>> GetAllAsync()
        {
            var query = $@"SELECT id AS Id, 
                                  user_id AS UserId, 
                                  name AS Name, 
                                  removed AS Removed, 
                                  insertdate AS InsertDate 
                           FROM public.categories";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var data = await connection.QueryAsync<CategoryEntityModel>(query) ?? null;
            if (data == null)
            {
                await connection.CloseAsync();
                return null;
            }
            await connection.CloseAsync();
            return data.ToList();
        }

        public async Task<CategoryEntityModel> GetAsync(int id)
        {
            var query = $@"SELECT id AS Id, 
                                  user_id AS UserId, 
                                  name AS Name, 
                                  removed AS Removed, 
                                  insertdate AS InsertDate 
                              FROM public.categories
                              WHERE id=@id";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var category = await connection.QueryFirstOrDefaultAsync<CategoryEntityModel>(query, new { id });
            await connection.CloseAsync();
            return category;
        }

        public async Task UpdateAsync(CategoryEntityModel entityModel)
        {
            var query = $@"UPDATE public.categories SET name=@Name WHERE id=@Id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, entityModel);
            await connection.CloseAsync();
        }

        public async Task<List<CategoryEntityModel>> GetPartnerCategoriesAsync(int userId)
        {
            var query = $@"SELECT id AS Id, 
                                  user_id AS UserId, 
                                  name AS Name, 
                                  removed AS Removed, 
                                  insertdate AS InsertDate 
                              FROM public.categories
                              WHERE user_id=@userId";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return null;
            var data = await connection.QueryAsync<CategoryEntityModel>(query, new { userId }) ?? null;
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