using System;
using System.Threading.Tasks;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.BusinessObjects;
using Dapper;
using Npgsql;

namespace BusinessSystem.Database.Contexts.PartnerApplication
{
    public class PartnersApplicationsRequestContext : DataBaseContext, IRequestRepository<PartnersApplicationsEntityModel>
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

        public async Task UpdateAsync(PartnersApplicationsEntityModel entityModel)
        {
            var query = $@"UPDATE public.partners_applications
	                       SET application_text=@ApplicationText
	                       WHERE id=@Id;";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, entityModel);
            await connection.CloseAsync();
        }
        
        public async Task RejectApplicationAsync(int partnerId, int appId)
        {
            var unconfirmedDate = DateTime.Now;
            var query = $@"UPDATE public.partners_applications
	                       SET confirmed=false, 
                               unconfirmdate=@unconfirmedDate 
	                       WHERE id=@appId; AND recipient_id=@partnerId;";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, new { partnerId, appId, unconfirmedDate });
            await connection.CloseAsync();
        }
        
        public async Task ConfirmApplicationAsync(int partnerId, int appId)
        {
            var confirmDate = DateTime.Now;
            var query = $@"UPDATE public.partners_applications
	                       SET confirmed=true, 
                               confirmdate=@confirmDate 
	                       WHERE id=@appId AND recipient_id=@partnerId;";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, new { partnerId, appId, confirmDate });
            await connection.CloseAsync();
        }
        
        public async Task RemoveApplicationAsync(int partnerId, int appId)
        {
            var query = $@"UPDATE public.partners_applications
	                       SET removed=true
	                       WHERE id=@appId AND sender_id=@partnerId;";
            
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
                return;
            await connection.ExecuteAsync(query, new { partnerId, appId });
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