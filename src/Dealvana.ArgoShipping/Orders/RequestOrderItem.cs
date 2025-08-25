using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Orders
{
    public class RequestOrderItem
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }

        [JsonPropertyName("quantity_ordered")]
        public int Quantity { get; set; }
    }
}