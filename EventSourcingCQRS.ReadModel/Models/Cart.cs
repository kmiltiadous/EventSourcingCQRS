namespace EventSourcingCQRS.ReadModel.Models
{
    public class Cart : IReadEntity
    {
        public string Id { get; set; }

        public int TotalItems { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Name { get; set; }
    }
}
