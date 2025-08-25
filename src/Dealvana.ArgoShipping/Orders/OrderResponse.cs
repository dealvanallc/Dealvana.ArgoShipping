using System.Collections.Generic;
using System.Text.Json.Serialization;

using Dealvana.ArgoShipping.Shipments;

namespace Dealvana.ArgoShipping.Orders
{
    public class OrderResponse : BaseResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("customer_order_id")]
        public string CustomerOrderId { get; set; }

        [JsonPropertyName("shipments")]
        public List<ShipmentResponse> Shipments { get; set; }

        [JsonPropertyName("json_data")]
        public RequestOrder OriginalRequest { get; set; }
    }
}
