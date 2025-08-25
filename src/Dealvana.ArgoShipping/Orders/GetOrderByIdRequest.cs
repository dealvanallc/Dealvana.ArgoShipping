namespace Dealvana.ArgoShipping.Orders
{
    public class GetOrderByIdRequest : BaseRequest
    {
        internal override string Endpoint => "orders/by_id/{id}";

        [PathParameter("id")]
        public int Id { get; set; }
    }
}
