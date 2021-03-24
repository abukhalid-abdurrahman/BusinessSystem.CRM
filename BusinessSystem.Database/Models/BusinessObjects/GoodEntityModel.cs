using System;

namespace BusinessSystem.Database.Models.BusinessObjects
{
    public class GoodEntityModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public int DescriptionId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool Removed { get; set; }
        public DateTime InsertDate { get; set; }
    }
}