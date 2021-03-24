using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessSystem.Database;
using BusinessSystem.Database.Models.BusinessObjects;
using BusinessSystem.Database.Models.DataTransferObjects.Response;

namespace BusinessSystem.CRM.Logics.Services.PartnerApplication
{
    public class PartnerApplicationService : IPartnersApplicationsService
    {
        private readonly DataBaseContext _db;
        
        public PartnerApplicationService()
        {
            _db = new DataBaseContext();
        }
        
        public async Task<List<PartnersApplicationsResponseModel>> GetInboxApplicationsAsync(int partnerId)
        {
            return await _db.PartnersApplicationsResponse.GetInboxPartnerApplicationsAsync(partnerId);
        }

        public async Task<PartnersApplicationsResponseModel> GetInboxApplicationAsync(int partnerId, int applicationId)
        {
            return await _db.PartnersApplicationsResponse.GetInboxPartnerApplicationAsync(partnerId, applicationId);
        }

        public async Task<PartnersApplicationsResponseModel> GetApplicationAsync(int applicationId)
        {
            return await _db.PartnersApplicationsResponse.GetAsync(applicationId);
        }

        public async Task<List<PartnersApplicationsResponseModel>> GetSentApplicationsAsync(int partnerId)
        {
            return await _db.PartnersApplicationsResponse.GetSentPartnerApplicationsAsync(partnerId);
        }

        public async Task<List<PartnersApplicationsResponseModel>> GetRejectedApplicationsAsync(int partnerId)
        {
            return await _db.PartnersApplicationsResponse.GetRejectedPartnerApplicationsAsync(partnerId);
        }

        public async Task<List<PartnersApplicationsResponseModel>> GetConfirmedApplicationsAsync(int partnerId)
        {
            return await _db.PartnersApplicationsResponse.GetConfirmedPartnerApplicationsAsync(partnerId);
        }

        public async Task RejectInboxApplication(int partnerId, int applicationId)
        {
            await _db.PartnersApplicationsRequest.RejectApplicationAsync(partnerId, applicationId);
        }

        public async Task ConfirmInboxApplication(int partnerId, int applicationId)
        {
            await _db.PartnersApplicationsRequest.ConfirmApplicationAsync(partnerId, applicationId);
        }

        public async Task<int> SendPartnerApplicationAsync(PartnersApplicationsEntityModel entityModel)
        {
            return await _db.PartnersApplicationsRequest.CreateAsync(entityModel);
        }

        public async Task EditPartnerApplicationAsync(PartnersApplicationsEntityModel entityModel)
        {
            await _db.PartnersApplicationsRequest.UpdateAsync(entityModel);
        }

        public async Task RemovePartnerApplicationAsync(int partnerId, int applicationId)
        {
            await _db.PartnersApplicationsRequest.RemoveApplicationAsync(partnerId, applicationId);
        }
    }
}