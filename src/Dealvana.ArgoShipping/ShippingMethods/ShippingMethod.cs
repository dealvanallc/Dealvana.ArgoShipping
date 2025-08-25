using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.ShippingMethods
{
    public class ShippingMethod
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("description")]
        public string Name { get; set; }
    }
}