using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Items
{
    public class ItemsResponse : BaseResponse
    {
        [JsonPropertyName("items")]
        public IList<Item> Items { get; set; }
    }
}
