using System.Collections.Generic;

namespace BusinessSystem.Database.Models.DataTransferObjects.Response
{
    public class PartnerContentResponseModel
    {
        public PartnerResponseModel Partner { get; set; }
        public List<CategoryResponseModel> Categories { get; set; }
        public List<GoodResponseModel> Goods { get; set; }
    }
}