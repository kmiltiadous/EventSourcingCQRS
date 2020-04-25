namespace EventSourcingCQRS.ReadModel.Models
{
    public class Customer : IReadEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
