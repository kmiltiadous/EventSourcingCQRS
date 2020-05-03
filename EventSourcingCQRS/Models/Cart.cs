namespace EventSourcingCQRS.Models
{

    public class Cart 
    {
        public string Id { get; set; }

        public int TotalItems { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Name { get; set; }

    }
}
