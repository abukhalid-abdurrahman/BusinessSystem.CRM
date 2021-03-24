using BusinessSystem.Database.Contexts;
using BusinessSystem.Database.Contexts.Category;
using BusinessSystem.Database.Contexts.Good;
using BusinessSystem.Database.Contexts.PartnerApplication;
using BusinessSystem.Database.Contexts.User;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using BusinessSystem.Database.Models.DataTransferObjects.Response;

namespace BusinessSystem.Database
{
    public class DataBaseContext : Interfaces.IDataBaseContext
    {
        private const string ConnectionString = "Host=localhost;Username=postgres;Password=faridun;Database=crm_data";
        private DescriptionContext _descriptionContext;
        private ImageContext _imageContext;
        
        private UserContext _userContext;
        private UserResponseContext _userResponseContext;

        private CategoryContext _categoryContext;
        private CategoryResponseContext _categoryResponseContext;
        private CategoryRequestContext _categoryRequestContext;
        
        private GoodContext _goodContext;
        private GoodResponseContext _goodResponseContext;

        private PartnersApplicationsContext _partnersApplicationsContext;
        private PartnersApplicationsResponseContext _partnersApplicationsResponseContext;
        private PartnersApplicationsRequestContext _partnersApplicationsRequestContext;


        public PartnersApplicationsContext PartnersApplications =>
            _partnersApplicationsContext ??= new PartnersApplicationsContext();

        public PartnersApplicationsResponseContext PartnersApplicationsResponse =>
            _partnersApplicationsResponseContext ??= new PartnersApplicationsResponseContext();

        public PartnersApplicationsRequestContext PartnersApplicationsRequest =>
            _partnersApplicationsRequestContext ??= new PartnersApplicationsRequestContext();
        
        public DescriptionContext Descriptions => _descriptionContext ??= new DescriptionContext();
        public ImageContext Images => _imageContext ??= new ImageContext();
        
        public UserContext Users => _userContext ??= new UserContext();
        public UserResponseContext UsersResponse => _userResponseContext ??= new UserResponseContext();
        
        public CategoryContext Categories => _categoryContext ??= new CategoryContext();
        public CategoryResponseContext CategoriesResponse => _categoryResponseContext ??= new CategoryResponseContext();
        public CategoryRequestContext CategoriesRequest => _categoryRequestContext ??= new CategoryRequestContext();
        
        public GoodContext Goods => _goodContext ??= new GoodContext();
        public GoodResponseContext GoodsResponse => _goodResponseContext ??= new GoodResponseContext();
        public string GetDataBaseConnectionString()
        {
            return ConnectionString;
        }
    }
}