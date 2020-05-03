namespace EventSourcingCQRS.API.MessageContracts
{
    public class CartItemRequest
    {
        public string CartId { get; set; }
        public string ProductId { get; set; }
        public int Quantity{ get; set; }
    }
}
