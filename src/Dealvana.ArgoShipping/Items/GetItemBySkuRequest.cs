namespace Dealvana.ArgoShipping.Items
{
    public class GetItemBySkuRequest : BaseRequest
    {
        internal override string Endpoint => "items/by_sku/{sku}";

        [PathParameter("sku")]
        public string Sku { get; set; }
    }
}
