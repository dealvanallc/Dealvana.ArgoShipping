namespace Dealvana.ArgoShipping.Orders
{
    public class DeleteOrderRequest : BaseRequest
    {
        internal override string Endpoint => "orders/{id}";

        [PathParameter("id")]
        public int OrderId { get; set; }
    }
}
