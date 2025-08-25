namespace Dealvana.ArgoShipping.Items
{
    public class ListItemsRequest : BaseRequest
    {
        internal override string Endpoint => "items";

        [QueryStringParameter("after")]
        public int? After { get; set; }

        [QueryStringParameter("category_id")]
        public int? CategoryId { get; set; }
    }
}
