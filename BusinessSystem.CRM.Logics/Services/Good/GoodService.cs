using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessSystem.CRM.Logics.Contexts.CdnContext;
using BusinessSystem.Database;
using BusinessSystem.Database.Models.BusinessObjects;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using BusinessSystem.Database.Models.DataTransferObjects.Response;

namespace BusinessSystem.CRM.Logics.Services.Good
{
    public class GoodService : DataBaseContext, IGoodService
    {
        private readonly DataBaseContext _db;
        public GoodService()
        {
            _db = new DataBaseContext();
        }
        
        public async Task<GoodResponseModel> CreateNewGood(GoodRequestModel model, string cdnPath, string webRootPath)
        {
            if(model == null || string.IsNullOrEmpty(cdnPath) || string.IsNullOrEmpty(webRootPath))
                throw new ArgumentNullException();
            
            var descriptionModel = new DescriptionEntityModel()
            {
                Description = model.GoodDescription
            };
            descriptionModel.Id = await _db.Descriptions.CreateAsync(descriptionModel);
            
            var newGoodImage = new ImageEntityModel();
            var cdnContext = new CdnContext(cdnPath, webRootPath);
            newGoodImage.FileName = await cdnContext.Upload(model.GoodImage);
            newGoodImage.InsertDate = DateTime.Now;
            newGoodImage.Id = await _db.Images.CreateAsync(newGoodImage);

            var entityModel = new GoodEntityModel()
            {
                CategoryId = model.CategoryId,
                DescriptionId = descriptionModel.Id,
                ImageId = newGoodImage.Id,
                InsertDate = DateTime.Now,
                Name = model.GoodName, 
                Removed = model.Active != true,
                UserId = model.PartnerId
            };
            entityModel.Id = await _db.Goods.CreateAsync(entityModel);
            var responseModel = await _db.GoodsResponse.GetAsync(entityModel.Id);
            return responseModel;
        }

        public async Task EditGood(GoodRequestModel model, string cdnPath, string webRootPath)
        {
            if(model == null || string.IsNullOrEmpty(cdnPath) || string.IsNullOrEmpty(webRootPath))
                throw new ArgumentNullException();
            if(model.GoodId == null)
                throw new ArgumentException();
            var goodToUpdate = await _db.Goods.GetAsync(model.GoodId.Value);
            if(goodToUpdate == null)
                return;
            
            var descriptionModel = new DescriptionEntityModel()
            {
                Id = goodToUpdate.DescriptionId,
                Description = model.GoodDescription
            };
            await _db.Descriptions.UpdateAsync(descriptionModel);

            goodToUpdate.CategoryId = model.CategoryId;
            goodToUpdate.DescriptionId = descriptionModel.Id;
            goodToUpdate.InsertDate = DateTime.Now;
            goodToUpdate.Name = model.GoodName;
            goodToUpdate.Removed = !model.Active;
            goodToUpdate.UserId = model.PartnerId;
            goodToUpdate.Id = model.GoodId.Value;
            
            if (model.GoodImage != null)
            {
                var newGoodImage = new ImageEntityModel();
                var cdnContext = new CdnContext(cdnPath, webRootPath);
                newGoodImage.FileName = await cdnContext.Upload(model.GoodImage);
                newGoodImage.InsertDate = DateTime.Now;
                newGoodImage.Id = await _db.Images.CreateAsync(newGoodImage);
                goodToUpdate.ImageId = newGoodImage.Id;
            }
            await _db.Goods.UpdateAsync(goodToUpdate);        
        }

        public async Task<List<GoodResponseModel>> GetPartnerGoodsList(int userId)
        {
            if(userId <= 0)
                throw new ArgumentException();
            return await _db.GoodsResponse.GetPartnerGoodsAsync(userId);
        }

        public async Task<GoodResponseModel> GetGood(int goodId)
        {
            if(goodId <= 0)
                throw new ArgumentException();
            return await _db.GoodsResponse.GetAsync(goodId);
        }
    }
}