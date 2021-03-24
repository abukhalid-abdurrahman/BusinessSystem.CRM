using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.DataTransferObjects.Response;
using Dapper;
using Npgsql;

namespace BusinessSystem.Database.Contexts.User
{
    public class UserResponseContext : DataBaseContext, IResponseRepository<PartnerResponseModel>
    {
        public async Task<List<PartnerResponseModel>> GetAllAsync()
        {
            var query = $@"SELECT u.id AS PartnerId, 
	                           u.username AS Username,
	                           u.login AS Login,
                               u.email AS Email,
                               u.phonenumber AS PhoneNumber,
	                           u.password AS Password,
	                           u.removed AS Active,
                               u.insertdate AS InsertDate,
                               d.description AS Description,
	                           i.filename AS PartnerImageUrl
                          FROM public.users u
                          JOIN public.descriptions d ON u.description_id = d.id
                          JOIN public.images i ON u.image_id = i.id
                          ORDER BY u.insertdate DESC";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var data = await connection.QueryAsync<PartnerResponseModel>(query) ?? null;
            if (data == null)
            {
                await connection.CloseAsync();
                return null;
            }
            await connection.CloseAsync();
            return data.ToList();
        }

        public async Task<PartnerResponseModel> GetAsync(int id)
        {
            var query = $@"SELECT u.id AS PartnerId, 
	                           u.username AS Username,
	                           u.login AS Login,
                               u.email AS Email,
                               u.phonenumber AS PhoneNumber,
	                           u.password AS Password,
	                           u.removed AS Active,
                               u.insertdate AS InsertDate,
                               d.description AS Description,
	                           i.filename AS PartnerImageUrl
                        FROM public.users u
                        JOIN public.descriptions d ON u.description_id = d.id
                        JOIN public.images i ON u.image_id = i.id
                        WHERE u.id = @id ORDER BY u.insertdate DESC";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var partnerResponse = await connection.QueryFirstOrDefaultAsync<PartnerResponseModel>(query, new { id });
            await connection.CloseAsync();
            return partnerResponse;       
        }
        
        public async Task<PartnerResponseModel> GetByRoleAndIdAsync(int roleId, int id)
        {
            var query = $@"SELECT u.id AS PartnerId, 
	                           u.username AS Username,
	                           u.login AS Login,
                               u.email AS Email,
                               u.phonenumber AS PhoneNumber,
	                           u.password AS Password,
                               u.removed AS Active,
                               u.insertdate AS InsertDate,
	                           d.description AS Description,
	                           i.filename AS PartnerImageUrl
                        FROM public.users u
                        JOIN public.descriptions d ON u.description_id = d.id
                        JOIN public.images i ON u.image_id = i.id
                        WHERE u.id = @id AND u.role_id = @roleId ORDER BY u.insertdate DESC";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var partnerResponse = await connection.QueryFirstOrDefaultAsync<PartnerResponseModel>(query, new { roleId, id });
            await connection.CloseAsync();
            return partnerResponse;       
        }
        
        public async Task<List<PartnerResponseModel>> GetByRoleAsync(int roleId)
        {
            var query = $@"SELECT u.id AS PartnerId, 
	                           u.username AS Username,
	                           u.login AS Login,
                               u.email AS Email,
                               u.phonenumber AS PhoneNumber,
	                           u.password AS Password,
	                           d.description AS Description,
                               u.removed AS Active,
                               u.insertdate AS InsertDate,
	                           i.filename AS PartnerImageUrl
                        FROM public.users u
                        JOIN public.descriptions d ON u.description_id = d.id
                        JOIN public.images i ON u.image_id = i.id
                        WHERE u.role_id = @roleId ORDER BY u.insertdate DESC";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var partnerResponse = await connection.QueryAsync<PartnerResponseModel>(query, new { roleId }) ?? new List<PartnerResponseModel>();
            await connection.CloseAsync();
            return partnerResponse.ToList();       
        }
    }
}