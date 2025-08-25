using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Shipments
{
    public class ShipmentsResponse : BaseResponse
    {
        [JsonPropertyName("entries")]
        public List<ShipmentResponse> Shipments { get; set; }

        [JsonPropertyName("page_number")]
        public int Page { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("total_entries")]
        public int TotalEntries { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
    }
}
