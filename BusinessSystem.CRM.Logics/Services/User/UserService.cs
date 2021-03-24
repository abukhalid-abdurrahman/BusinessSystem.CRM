using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessSystem.CRM.Logics.Contexts;
using BusinessSystem.CRM.Logics.Contexts.Authorization;
using BusinessSystem.CRM.Logics.Contexts.CdnContext;
using BusinessSystem.Database;
using BusinessSystem.Database.Models.BusinessObjects;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using BusinessSystem.Database.Models.DataTransferObjects.Response;

namespace BusinessSystem.CRM.Logics.Services.User
{
    public class UserService : IUserService
    {
        private readonly DataBaseContext _db;

        public UserService()
        {
            _db = new DataBaseContext();
        }
        
        public async Task CreateNewPartner(PartnerRequestModel partnerRequestModel, string cdnPath, string webRootPath)
        {
            if(partnerRequestModel == null)
                throw new ArgumentNullException(nameof(partnerRequestModel));
            var newPartnerDescriptionEntity = new DescriptionEntityModel
            {
                Description = partnerRequestModel.Description,
            };
            newPartnerDescriptionEntity.Id = await _db.Descriptions.CreateAsync(newPartnerDescriptionEntity);

            var newPartnerImageEntity = new ImageEntityModel();
            var cdnContext = new CdnContext(cdnPath, webRootPath);
            newPartnerImageEntity.FileName = await cdnContext.Upload(partnerRequestModel.PartnerImage);
            newPartnerImageEntity.InsertDate = DateTime.Now;
            newPartnerImageEntity.Id = await _db.Images.CreateAsync(newPartnerImageEntity);
            
            var hashContext = new HashContext();
            var hashedPassword = hashContext.MD5(partnerRequestModel.Password);

            var newPartner = new UserEntityModel()
            {
                DescriptionId = newPartnerDescriptionEntity.Id,
                ImageId = newPartnerImageEntity.Id,
                InsertDate = DateTime.Now,
                Login = partnerRequestModel.Login,
                Password = hashedPassword,
                Removed = false,
                RoleId = RolesContext.Partner,
                UserName = partnerRequestModel.Username,
                Email = partnerRequestModel.Email,
                PhoneNumber = partnerRequestModel.PhoneNumber
            };
            newPartner.Id = await _db.Users.CreateAsync(newPartner);
        }

        public async Task<PartnerResponseModel> GetPartner(int partnerId)
        {
            if (partnerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(partnerId));
            return await _db.UsersResponse.GetAsync(partnerId);
        }

        public async Task<List<PartnerResponseModel>> GetPartners()
        {
            return await _db.UsersResponse.GetByRoleAsync(RolesContext.Partner);
        }

        public async Task<List<PartnerResponseModel>> GetUsers()
        {
            return await _db.UsersResponse.GetAllAsync();
        }

        public async Task<PartnerContentResponseModel> GetPartnerContentViewModel(int partnerId)
        {
            var categoriesResponseModel = await _db.CategoriesResponse.GetPartnerCategories(partnerId);
            var goodsResponseModel = await _db.GoodsResponse.GetPartnerGoodsAsync(partnerId);
            var partnerResponseModel = await _db.UsersResponse.GetAsync(partnerId);
            return new PartnerContentResponseModel()
            {
                Categories = categoriesResponseModel,
                Goods = goodsResponseModel,
                Partner = partnerResponseModel
            };
        }

        public async Task<bool> EditPartner(PartnerRequestModel partnerModel, string cdnPath, string webRootPath)
        {
            if(partnerModel == null)
                throw new ArgumentNullException(nameof(partnerModel));
            
            var userEntityToUpdate = await _db.Users.GetAsync(partnerModel.PartnerId);
            if (userEntityToUpdate == null)
                return false;

            var updatedPartnerDescriptionEntity = new DescriptionEntityModel
            {
                Id = userEntityToUpdate.DescriptionId,
                Description = partnerModel.Description,
            };
            await _db.Descriptions.UpdateAsync(updatedPartnerDescriptionEntity);

            if(partnerModel.PartnerImage != null)
            {
                var newPartnerImageEntity = new ImageEntityModel();
                var cdnContext = new CdnContext(cdnPath, webRootPath);
                newPartnerImageEntity.FileName = await cdnContext.Upload(partnerModel.PartnerImage);
                newPartnerImageEntity.InsertDate = DateTime.Now;
                newPartnerImageEntity.Id = await _db.Images.CreateAsync(newPartnerImageEntity);
                userEntityToUpdate.ImageId = newPartnerImageEntity.Id;
            }

            if(!string.IsNullOrEmpty(partnerModel.Password) || !string.IsNullOrWhiteSpace(partnerModel.Password))
            {
                var hashContext = new HashContext();
                var newHashedPassword = hashContext.MD5(partnerModel.Password);
                userEntityToUpdate.Password = newHashedPassword;
            }
            userEntityToUpdate.Login = partnerModel.Login;
            userEntityToUpdate.UserName = partnerModel.Username;
            userEntityToUpdate.Email = partnerModel.Email;
            userEntityToUpdate.PhoneNumber = partnerModel.PhoneNumber;
            await _db.Users.UpdateAsync(userEntityToUpdate);
            return true;
        }

        public async Task DeactivatePartner(RemovePartnerRequestModel partnerRequestModel)
        {
            if(partnerRequestModel == null)
                throw new ArgumentNullException(nameof(partnerRequestModel));
            await _db.Users.RemoveAsync(partnerRequestModel);
        }
    }
}