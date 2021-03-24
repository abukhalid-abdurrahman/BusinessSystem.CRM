using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessSystem.Database.Models.DataTransferObjects.Response
{
    public class PartnersApplicationsResponseModel
    {
        public int Id { get; set; }
        [Display(Name = "Sender User Name")]
        [DataType(DataType.Text)]
        public string SenderUserName { get; set; }
        public int SenderId { get; set; }
        [Display(Name = "Recipient User Name")]
        [DataType(DataType.Text)]
        public string RecipientUserName { get; set; }
        public int RecipientId { get; set; }
        [Display(Name = "Application Content")]
        [DataType(DataType.MultilineText)]
        public string ApplicationText { get; set; }
        [Display(Name = "Confirmed Status")]
        public bool Confirmed { get; set; }
        [Display(Name = "Removed Status")]
        public bool Removed { get; set; }
        [Display(Name = "Application Creation Date")]
        [DataType(DataType.DateTime)]
        public DateTime InsertDate { get; set; }
        [Display(Name = "Application Confirm Date")]
        [DataType(DataType.DateTime)]
        public DateTime ConfirmDate { get; set; }
        [Display(Name = "Application Unconfirmed Date")]
        [DataType(DataType.DateTime)]
        public DateTime UnConfirmDate { get; set; }
    }
}