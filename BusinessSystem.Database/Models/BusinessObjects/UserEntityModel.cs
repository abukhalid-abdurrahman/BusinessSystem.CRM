namespace BusinessSystem.Database.Models.BusinessObjects
{
    public class UserEntityModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ImageId { get; set; }
        public int DescriptionId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Removed { get; set; }
        public System.DateTime InsertDate { get; set; }
    }
}