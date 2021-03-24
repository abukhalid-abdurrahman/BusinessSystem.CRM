using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.DataTransferObjects.Response;
using Dapper;
using Npgsql;

namespace BusinessSystem.Database.Contexts.Good
{
    public class GoodResponseContext : DataBaseContext, IResponseRepository<GoodResponseModel>
    {
        public async Task<List<GoodResponseModel>> GetAllAsync()
        {
            var query = $@"SELECT g.id AS GoodId, 
	                               g.user_id AS PartnerId, 
	                               g.category_id AS CategoryId, 
	                               g.name AS GoodName, 
	                               g.removed AS Active, 
	                               g.insertdate AS CreateDate,
	                               d.description AS GoodDescription,
	                               c.name AS CategoryName,
	                               i.filename AS GoodImageUrl,
	                               u.username AS PartnerName
                        FROM public.goods g
                        JOIN public.descriptions d ON g.description_id = d.id
                        JOIN public.categories c ON g.category_id = c.id
                        JOIN public.images i ON g.image_id = i.id
                        JOIN public.users u ON g.user_id = u.id
						ORDER BY g.insertdate DESC";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var data = await connection.QueryAsync<GoodResponseModel>(query) ?? null;
            if (data == null)
            {
                await connection.CloseAsync();
                return null;
            }
            await connection.CloseAsync();
            return data.ToList();
        }

        public async Task<GoodResponseModel> GetAsync(int id)
        {
            var query = $@"SELECT g.id AS GoodId, 
							   g.user_id AS PartnerId, 
							   g.category_id AS CategoryId, 
							   g.name AS GoodName, 
							   g.removed AS Active, 
							   g.insertdate AS CreateDate,
							   d.description AS GoodDescription,
							   c.name AS CategoryName,
							   i.filename AS GoodImageUrl,
							   u.username AS PartnerName
						FROM public.goods g
						JOIN public.descriptions d ON g.description_id = d.id
						JOIN public.categories c ON g.category_id = c.id
						JOIN public.images i ON g.image_id = i.id
						JOIN public.users u ON g.user_id = u.id
						WHERE g.id = @id ORDER BY g.insertdate DESC";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var goodResponse = await connection.QueryFirstOrDefaultAsync<GoodResponseModel>(query, new { id }) ?? new GoodResponseModel();
            await connection.CloseAsync();
            return goodResponse;        
        }
        
        public async Task<List<GoodResponseModel>> GetPartnerGoodsAsync(int userId)
        {
	        var query = $@"SELECT g.id AS GoodId, 
	                               g.user_id AS PartnerId, 
	                               g.category_id AS CategoryId, 
	                               g.name AS GoodName, 
	                               g.removed AS Active, 
	                               g.insertdate AS CreateDate,
	                               d.description AS GoodDescription,
	                               c.name AS CategoryName,
	                               i.filename AS GoodImageUrl,
	                               u.username AS PartnerName
                        FROM public.goods g
                        JOIN public.descriptions d ON g.description_id = d.id
                        JOIN public.categories c ON g.category_id = c.id
                        JOIN public.images i ON g.image_id = i.id
                        JOIN public.users u ON g.user_id = u.id
						WHERE g.user_id = @userId ORDER BY g.insertdate DESC";

	        await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
	        await connection.OpenAsync();
	        if (connection.State != System.Data.ConnectionState.Open)
		        return default;
	        var data = await connection.QueryAsync<GoodResponseModel>(query, new { userId }) ?? null;
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