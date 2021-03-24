using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessSystem.Database.Interfaces;
using BusinessSystem.Database.Models.DataTransferObjects.Response;
using Dapper;
using Npgsql;

namespace BusinessSystem.Database.Contexts.PartnerApplication
{
    public class PartnersApplicationsResponseContext : DataBaseContext, IResponseRepository<PartnersApplicationsResponseModel>
    {
        public async Task<List<PartnersApplicationsResponseModel>> GetAllAsync()
        {
            var query = $@"SELECT pa.id AS Id, 
								  pa.sender_id AS SenderId, 
								  su.username AS SenderUserName,
								  pa.recipient_id AS RecipientId, 
								  ru.username AS RecipientUserName,
								  pa.application_text AS ApplicationText, 
								  pa.confirmed AS Confirmed, 
								  pa.confirmdate AS ConfirmDate, 
								  pa.unconfirmdate AS UnConfirmDate, 
								  pa.insertdate AS InsertDate, 
								  pa.removed AS Removed
							FROM public.partners_applications pa
							JOIN public.users su ON pa.sender_id = su.id
							JOIN public.users ru ON pa.recipient_id = ru.id
							ORDER BY pa.insertdate DESC;";
            await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
            await connection.OpenAsync();
            if (connection.State != System.Data.ConnectionState.Open)
	            return default;
            var data = await connection.QueryAsync<PartnersApplicationsResponseModel>(query) ?? null;
            if (data == null)
            {
	            await connection.CloseAsync();
	            return null;
            }
            await connection.CloseAsync();
            return data.ToList();
        }
        
        public async Task<List<PartnersApplicationsResponseModel>> GetInboxPartnerApplicationsAsync(int partnerId)
        {
	        var query = $@"SELECT pa.id AS Id, 
								  pa.sender_id AS SenderId, 
								  su.username AS SenderUserName,
								  pa.recipient_id AS RecipientId, 
								  ru.username AS RecipientUserName,
								  pa.application_text AS ApplicationText, 
								  pa.confirmed AS Confirmed, 
								  pa.confirmdate AS ConfirmDate, 
								  pa.unconfirmdate AS UnConfirmDate, 
								  pa.insertdate AS InsertDate, 
								  pa.removed AS Removed
							FROM public.partners_applications pa
							JOIN public.users su ON pa.sender_id = su.id
							JOIN public.users ru ON pa.recipient_id = ru.id
							WHERE pa.recipient_id=@partnerId ORDER BY pa.insertdate DESC;";
	        await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
	        await connection.OpenAsync();
	        if (connection.State != System.Data.ConnectionState.Open)
		        return default;
	        var data = await connection.QueryAsync<PartnersApplicationsResponseModel>(query, new { partnerId }) ?? null;
	        if (data == null)
	        {
		        await connection.CloseAsync();
		        return null;
	        }
	        await connection.CloseAsync();
	        return data.ToList();
        }

        public async Task<PartnersApplicationsResponseModel> GetAsync(int id)
        {
	        var query = $@"SELECT pa.id AS Id, 
								  pa.sender_id AS SenderId, 
								  su.username AS SenderUserName,
								  pa.recipient_id AS RecipientId, 
								  ru.username AS RecipientUserName,
								  pa.application_text AS ApplicationText, 
								  pa.confirmed AS Confirmed, 
								  pa.confirmdate AS ConfirmDate, 
								  pa.unconfirmdate AS UnConfirmDate, 
								  pa.insertdate AS InsertDate, 
								  pa.removed AS Removed
							FROM public.partners_applications pa
							JOIN public.users su ON pa.sender_id = su.id
							JOIN public.users ru ON pa.recipient_id = ru.id
							WHERE pa.id=@id ORDER BY pa.insertdate DESC;";
            
	        await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
	        await connection.OpenAsync();
	        if (connection.State != System.Data.ConnectionState.Open)
		        return default;
	        var applicationResponse = await connection.QueryFirstOrDefaultAsync<PartnersApplicationsResponseModel>(query, new { id }) ?? new PartnersApplicationsResponseModel();
	        await connection.CloseAsync();
	        return applicationResponse; 
        }
        public async Task<PartnersApplicationsResponseModel> GetInboxPartnerApplicationAsync(int partnerId, int appId)
        {
	        var query = $@"SELECT pa.id AS Id, 
								  pa.sender_id AS SenderId, 
								  su.username AS SenderUserName,
								  pa.recipient_id AS RecipientId, 
								  ru.username AS RecipientUserName,
								  pa.application_text AS ApplicationText, 
								  pa.confirmed AS Confirmed, 
								  pa.confirmdate AS ConfirmDate, 
								  pa.unconfirmdate AS UnConfirmDate, 
								  pa.insertdate AS InsertDate, 
								  pa.removed AS Removed
							FROM public.partners_applications pa
							JOIN public.users su ON pa.sender_id = su.id
							JOIN public.users ru ON pa.recipient_id = ru.id
							WHERE pa.id=@appId AND pa.recipient_id=@partnerId ORDER BY pa.insertdate DESC;";
            
	        await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
	        await connection.OpenAsync();
	        if (connection.State != System.Data.ConnectionState.Open)
		        return default;
	        var applicationResponse = await connection.QueryFirstOrDefaultAsync<PartnersApplicationsResponseModel>(query, new { appId, partnerId }) ?? new PartnersApplicationsResponseModel();
	        await connection.CloseAsync();
	        return applicationResponse; 
        }

        public async Task<List<PartnersApplicationsResponseModel>> GetConfirmedPartnerApplicationsAsync(int partnerId)
        {
	        var query = $@"SELECT pa.id AS Id, 
								  pa.sender_id AS SenderId, 
								  su.username AS SenderUserName,
								  pa.recipient_id AS RecipientId, 
								  ru.username AS RecipientUserName,
								  pa.application_text AS ApplicationText, 
								  pa.confirmed AS Confirmed, 
								  pa.confirmdate AS ConfirmDate, 
								  pa.unconfirmdate AS UnConfirmDate, 
								  pa.insertdate AS InsertDate, 
								  pa.removed AS Removed
							FROM public.partners_applications pa
							JOIN public.users su ON pa.sender_id = su.id
							JOIN public.users ru ON pa.recipient_id = ru.id
							WHERE pa.sender_id=@partnerId AND pa.confirmed=true ORDER BY pa.insertdate DESC;";
	        await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
	        await connection.OpenAsync();
	        if (connection.State != System.Data.ConnectionState.Open)
		        return default;
	        var data = await connection.QueryAsync<PartnersApplicationsResponseModel>(query, new { partnerId }) ?? null;
	        if (data == null)
	        {
		        await connection.CloseAsync();
		        return null;
	        }
	        await connection.CloseAsync();
	        return data.ToList();        
        }

        public async Task<List<PartnersApplicationsResponseModel>> GetRejectedPartnerApplicationsAsync(int partnerId)
        {
	        var query = $@"SELECT pa.id AS Id, 
								  pa.sender_id AS SenderId, 
								  su.username AS SenderUserName,
								  pa.recipient_id AS RecipientId, 
								  ru.username AS RecipientUserName,
								  pa.application_text AS ApplicationText, 
								  pa.confirmed AS Confirmed, 
								  pa.confirmdate AS ConfirmDate, 
								  pa.unconfirmdate AS UnConfirmDate, 
								  pa.insertdate AS InsertDate, 
								  pa.removed AS Removed
							FROM public.partners_applications pa
							JOIN public.users su ON pa.sender_id = su.id
							JOIN public.users ru ON pa.recipient_id = ru.id
							WHERE pa.sender_id=@partnerId AND pa.confirmed=false ORDER BY pa.insertdate DESC;";
	        await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
	        await connection.OpenAsync();
	        if (connection.State != System.Data.ConnectionState.Open)
		        return default;
	        var data = await connection.QueryAsync<PartnersApplicationsResponseModel>(query, new { partnerId }) ?? null;
	        if (data == null)
	        {
		        await connection.CloseAsync();
		        return null;
	        }
	        await connection.CloseAsync();
	        return data.ToList();
        }

        public async Task<List<PartnersApplicationsResponseModel>> GetSentPartnerApplicationsAsync(int partnerId)
        {
	        var query = $@"SELECT pa.id AS Id, 
								  pa.sender_id AS SenderId, 
								  su.username AS SenderUserName,
								  pa.recipient_id AS RecipientId, 
								  ru.username AS RecipientUserName,
								  pa.application_text AS ApplicationText, 
								  pa.confirmed AS Confirmed, 
								  pa.confirmdate AS ConfirmDate, 
								  pa.unconfirmdate AS UnConfirmDate, 
								  pa.insertdate AS InsertDate, 
								  pa.removed AS Removed
							FROM public.partners_applications pa
							JOIN public.users su ON pa.sender_id = su.id
							JOIN public.users ru ON pa.recipient_id = ru.id
							WHERE pa.sender_id=@partnerId ORDER BY pa.insertdate DESC;";
	        await using var connection = new NpgsqlConnection(GetDataBaseConnectionString());
	        await connection.OpenAsync();
	        if (connection.State != System.Data.ConnectionState.Open)
		        return default;
	        var data = await connection.QueryAsync<PartnersApplicationsResponseModel>(query, new { partnerId }) ?? null;
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