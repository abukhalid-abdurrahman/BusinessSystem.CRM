using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using BusinessSystem.Database.Models.DataTransferObjects.Response;

namespace BusinessSystem.CRM.Logics.Services.User
{
    public interface IUserService
    {
        Task CreateNewPartner(PartnerRequestModel partnerRequestModel, string cdnPath, string webRootPath);
        Task<PartnerResponseModel> GetPartner(int partnerId);
        Task<List<PartnerResponseModel>> GetPartners();
        Task<List<PartnerResponseModel>> GetUsers();
        Task<PartnerContentResponseModel> GetPartnerContentViewModel(int partnerId);
        Task<bool> EditPartner(PartnerRequestModel partnerRequestModel, string cdnPath, string webRootPath);
        Task DeactivatePartner(RemovePartnerRequestModel partnerRequestModel);
    }
}