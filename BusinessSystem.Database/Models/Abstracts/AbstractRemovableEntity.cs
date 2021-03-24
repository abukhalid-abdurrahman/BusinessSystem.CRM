namespace BusinessSystem.Database.Models.Abstracts
{
    public abstract class AbstractRemovableEntity
    {
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}