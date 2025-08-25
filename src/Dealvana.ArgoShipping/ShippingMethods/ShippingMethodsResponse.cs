using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.ShippingMethods
{
    public class ShippingMethodsResponse : BaseResponse
    {
        [JsonPropertyName("shipping_methods")]
        public List<ShippingMethod> ShippingMethods { get; set; }
    }
}
