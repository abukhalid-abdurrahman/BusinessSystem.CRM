using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.BusinessObjects;
using Dapper;
using Npgsql;

namespace BusinessSystem.Database.Contexts.Good
{
    public class GoodContext : DataBaseContext, IEntityRepository<GoodEntityModel>
    {
        public async Task<int> CreateAsync(GoodEntityModel entityModel)
        {
            entityModel.InsertDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            var query = $@"INSERT INTO public.goods(
                                user_id, image_id, description_id, category_id, name, removed, insertdate)
                                VALUES (@UserId, @ImageId, @DescriptionId, @CategoryId, @Name, @Removed, @InsertDate) RETURNING id;";
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return 0;
            var goodId = await connection.ExecuteScalarAsync<int?>(query, entityModel);
            await connection.CloseAsync();
            return goodId ?? 0;
        }

        public async Task<List<GoodEntityModel>> GetAllAsync()
        {
            var query = $@"SELECT id AS Id,
                                  user_id AS UserId, 
                                  image_id AS ImageId, 
                                  description_id AS DescriptionId, 
                                  category_id AS CategoryId, 
                                  name AS Name, 
                                  removed AS Removed,
                                  insertdate AS InsertDate
                          FROM public.goods";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var data = await connection.QueryAsync<GoodEntityModel>(query) ?? null;
            if (data == null)
            {
                await connection.CloseAsync();
                return null;
            }
            await connection.CloseAsync();
            return data.ToList();        
        }

        public async Task<GoodEntityModel> GetAsync(int id)
        {
            var query = $@"SELECT id AS Id,
                                  user_id AS UserId, 
                                  image_id AS ImageId, 
                                  description_id AS DescriptionId, 
                                  category_id AS CategoryId, 
                                  name AS Name, 
                                  removed AS Removed,
                                  insertdate AS InsertDate
                              FROM public.goods
                              WHERE id = @id";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var goodEntity = await connection.QueryFirstOrDefaultAsync<GoodEntityModel>(query, new { id });
            await connection.CloseAsync();
            return goodEntity;
        }

        public async Task<List<GoodEntityModel>> GetGoodsByCategory(int categoryId)
        {
            var query = $@"SELECT id AS Id,
                                  user_id AS UserId, 
                                  image_id AS ImageId, 
                                  description_id AS DescriptionId, 
                                  category_id AS CategoryId, 
                                  name AS Name, 
                                  removed AS Removed,
                                  insertdate AS InsertDate
                              FROM public.goods
                              WHERE category_id=@categoryId";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var goods = await connection.QueryAsync<GoodEntityModel>(query, new { categoryId });
            await connection.CloseAsync();
            return goods.ToList();
        }
        
        public async Task<List<GoodEntityModel>> GetGoodsByPartner(int userId)
        {
            var query = $@"SELECT id AS Id,
                                  user_id AS UserId, 
                                  image_id AS ImageId, 
                                  description_id AS DescriptionId, 
                                  category_id AS CategoryId, 
                                  name AS Name, 
                                  removed AS Removed,
                                  insertdate AS InsertDate
                              FROM public.goods
                              WHERE user_id=@userId";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var goods = await connection.QueryAsync<GoodEntityModel>(query, new { userId });
            await connection.CloseAsync();
            return goods.ToList();
        }
        
        public async Task<List<GoodEntityModel>> GetGoodsByPartnerAndCategory(int userId, int categoryId)
        {
            var query = $@"SELECT id AS Id,
                                  user_id AS UserId, 
                                  image_id AS ImageId, 
                                  description_id AS DescriptionId, 
                                  category_id AS CategoryId, 
                                  name AS Name, 
                                  removed AS Removed,
                                  insertdate AS InsertDate
                                  FROM public.goods
                                  WHERE user_id=@userId AND category_id=@categoryId";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var goods = await connection.QueryAsync<GoodEntityModel>(query, new { userId, categoryId });
            await connection.CloseAsync();
            return goods.ToList();
        }

        public async Task UpdateAsync(GoodEntityModel entityModel)
        {
            var query = $@"UPDATE public.goods
	                            SET user_id=@UserId, 
                                    image_id=@ImageId, 
                                    description_id=@DescriptionId, 
                                    category_id=@CategoryId, 
                                    name=@Name, 
                                    removed=@Removed
	                            WHERE id=@Id";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, entityModel);
            await connection.CloseAsync();        
        }

        public async Task DeleteAsync(int id)
        {
            var query = $@"DELETE FROM public.goods
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