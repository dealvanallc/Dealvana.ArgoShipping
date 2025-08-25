using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Items
{
    public class ItemResponse : BaseResponse
    {
        [JsonPropertyName("item")]
        public Item Item { get; set; }
    }
}
