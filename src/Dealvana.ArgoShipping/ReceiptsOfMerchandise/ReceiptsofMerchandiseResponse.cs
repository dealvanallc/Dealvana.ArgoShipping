using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.ReceiptsOfMerchandise
{
    public class ReceiptsofMerchandiseResponse : BaseResponse
    {
        [JsonPropertyName("entries")]
        public List<ReceiptOfMerchandise> ReceiptsOfMerchandise { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; }

        [JsonPropertyName("total_entries")]
        public int TotalEntries { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
    }
}
