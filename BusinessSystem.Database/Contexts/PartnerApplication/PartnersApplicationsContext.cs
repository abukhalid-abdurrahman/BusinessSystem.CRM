using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.BusinessObjects;
using Dapper;
using Npgsql;

namespace BusinessSystem.Database.Contexts.PartnerApplication
{
    public class PartnersApplicationsContext: DataBaseContext, IEntityRepository<PartnersApplicationsEntityModel>
    {
        public async Task<int> CreateAsync(PartnersApplicationsEntityModel entityModel)
        {
            var query =  $@"INSERT INTO public.partners_applications(
	                        sender_id, 
	                        recipient_id, 
	                        application_text, 
	                        confirmed, 
	                        confirmdate, 
	                        unconfirmdate, 
	                        insertdate, 
	                        removed)
	                        VALUES (@SenderId, @RecipientId, @ApplicationText, @Confirmed, @ConfirmDate, @UnConfirmDate, @InsertDate, @Removed)  RETURNING id;";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return 0;
            var applicationId = await connection.ExecuteScalarAsync<int?>(query, entityModel);
            await connection.CloseAsync();
            return applicationId ?? 0;
        }

        public async Task<List<PartnersApplicationsEntityModel>> GetAllAsync()
        {
            var query = $@"SELECT id AS Id, 
                                  sender_id AS SenderId, 
                                  recipient_id AS RecipientId, 
                                  application_text AS ApplicationText, 
                                  confirmed AS Confirmed, 
                                  confirmdate AS ConfirmDate, 
                                  unconfirmdate AS UnConfirmDate, 
                                  insertdate AS InsertDate, 
                                  removed AS Removed
	                        FROM public.partners_applications;";

            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var data = await connection.QueryAsync<PartnersApplicationsEntityModel>(query) ?? null;
            if (data == null)
            {
                await connection.CloseAsync();
                return null;
            }
            await connection.CloseAsync();
            return data.ToList();
        }

        public async Task<PartnersApplicationsEntityModel> GetAsync(int id)
        {
            var query = $@"SELECT id AS Id, 
                                  sender_id AS SenderId, 
                                  recipient_id AS RecipientId, 
                                  application_text AS ApplicationText, 
                                  confirmed AS Confirmed, 
                                  confirmdate AS ConfirmDate, 
                                  unconfirmdate AS UnConfirmDate, 
                                  insertdate AS InsertDate, 
                                  removed AS Removed
	                        FROM public.partners_applications WHERE id=@id;";
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return default;
            var application = await connection.QueryFirstOrDefaultAsync<PartnersApplicationsEntityModel>(query, new { id });
            await connection.CloseAsync();
            return application;
        }

        public async Task UpdateAsync(PartnersApplicationsEntityModel entityModel)
        {
            var query = $@"UPDATE public.partners_applications
	                       SET application_text=@ApplicationText, 
                               confirmed=@Confirmed, 
                               confirmdate=@ConfirmDate, 
                               unconfirmdate=@UnConfirmDate, 
                               removed=@Removed
	                       WHERE id=@Id;";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, entityModel);
            await connection.CloseAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var query = $@"DELETE FROM public.partners_applications
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