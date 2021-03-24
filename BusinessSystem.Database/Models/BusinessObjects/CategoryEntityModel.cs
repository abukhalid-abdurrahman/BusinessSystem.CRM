using System;

namespace BusinessSystem.Database.Models.BusinessObjects
{
    public class CategoryEntityModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool Removed { get; set; }
        public DateTime InsertDate { get; set; }
    }
}