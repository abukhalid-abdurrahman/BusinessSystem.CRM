using System;

namespace BusinessSystem.Database.Models.BusinessObjects
{
    public class PartnersApplicationsEntityModel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string ApplicationText { get; set; }
        public bool Confirmed { get; set; }
        public bool Removed { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime ConfirmDate { get; set; }
        public DateTime UnConfirmDate { get; set; }
    }
}