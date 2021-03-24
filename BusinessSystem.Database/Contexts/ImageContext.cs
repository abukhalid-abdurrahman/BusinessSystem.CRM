using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Npgsql;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.BusinessObjects;

namespace BusinessSystem.Database.Contexts
{
    public class ImageContext : DataBaseContext, IEntityRepository<ImageEntityModel>
    {
        public async Task<int> CreateAsync(ImageEntityModel entityModel)
        {
            var query = $@"INSERT INTO public.images(filename, insertdate)
	                          VALUES (@FileName, @InsertDate) RETURNING id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return 0;
            var imageId = await connection.ExecuteScalarAsync<int?>(query, entityModel);
            await connection.CloseAsync();
            return imageId ?? 0;
        }

        public async Task DeleteAsync(int id)
        {
            var query = $@"DELETE FROM public.images
                              WHERE id=@id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, new { id });
            await connection.CloseAsync();
        }

        public async Task<List<ImageEntityModel>> GetAllAsync()
        {
            var query = $@"SELECT * FROM public.images";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var data = await connection.QueryAsync<ImageEntityModel>(query) ?? null;
            if (data == null)
            {
                await connection.CloseAsync();
                return null;
            }
            await connection.CloseAsync();
            return data.ToList();
        }

        public async Task<ImageEntityModel> GetAsync(int id)
        {
            var query = $@"SELECT * FROM public.images
                              WHERE id=@id";


            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var imageEntity = await connection.QueryFirstOrDefaultAsync<ImageEntityModel>(query, new { id });
            await connection.CloseAsync();
            return imageEntity;
        }

        public async Task UpdateAsync(ImageEntityModel entityModel)
        {
            var query = $@"UPDATE public.images
	                            SET filename=@FileName, insertdate=@InsertDate
	                            WHERE @id=Id;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, entityModel);
            await connection.CloseAsync();
        }
    }
}
