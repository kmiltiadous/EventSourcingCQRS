namespace EventSourcingCQRS.API.MessageContracts
{
    public class CreateCartRequest
    {
        public string CustomerId { get; set; }
        public string CartName { get; set; }
    }
}
