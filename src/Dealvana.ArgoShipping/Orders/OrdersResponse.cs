using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Orders
{
    public class OrdersResponse : BaseResponse
    {
        [JsonPropertyName("orders")]
        public List<OrderResponse> Orders { get; set; }
    }
}
