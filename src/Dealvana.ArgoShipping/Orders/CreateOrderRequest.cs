using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Orders
{
    public class CreateOrderRequest : BaseRequest
    {
        [JsonIgnore]
        internal override string Endpoint => "orders";

        [JsonPropertyName("order")]
        public RequestOrder Order { get; set; }
    }
}
