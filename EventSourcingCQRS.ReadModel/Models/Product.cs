namespace EventSourcingCQRS.ReadModel.Models
{
    public class Product : IReadEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
