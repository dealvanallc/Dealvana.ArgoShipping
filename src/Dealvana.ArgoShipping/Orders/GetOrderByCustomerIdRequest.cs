namespace Dealvana.ArgoShipping.Orders
{
    public class GetOrderByCustomerIdRequest : BaseRequest
    {
        internal override string Endpoint => "orders/by_customer_order_id/{id}";

        [PathParameter("id")]
        public string CustomerOrderId { get; set; }
    }
}
