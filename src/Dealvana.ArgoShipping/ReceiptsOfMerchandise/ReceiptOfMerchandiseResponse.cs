using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.ReceiptsOfMerchandise
{
    public class ReceiptOfMerchandiseResponse : BaseResponse
    {
        [JsonPropertyName("rom")]
        public ReceiptOfMerchandise ReceiptOfMerchandise { get; set; }
    }
}
