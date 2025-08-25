namespace Dealvana.ArgoShipping.Items
{
    public class GetItemByIdRequest : BaseRequest
    {
        internal override string Endpoint => "items/by_id/{id}";

        [PathParameter("id")]
        public int Id { get; set; }
    }
}
