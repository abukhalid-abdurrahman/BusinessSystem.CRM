namespace BusinessSystem.Database.Models.BusinessObjects
{
    public class MessagingEntity
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public bool State { get; set; }
    }
}