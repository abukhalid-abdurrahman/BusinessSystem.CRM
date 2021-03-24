using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessSystem.Database.Models;
using BusinessSystem.Database.Models.BusinessObjects;
using BusinessSystem.Database.Models.DataTransferObjects;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using BusinessSystem.Database.Models.DataTransferObjects.Response;

namespace BusinessSystem.CRM.Logics.Services.Good
{
    public interface IGoodService
    {
        Task<GoodResponseModel> CreateNewGood(GoodRequestModel model, string cdnPath, string webRootPath);
        Task EditGood(GoodRequestModel model, string cdnPath, string webRootPath);
        Task<List<GoodResponseModel>> GetPartnerGoodsList(int userId);
        Task<GoodResponseModel> GetGood(int goodId);
    }
}