using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.ReceiptsOfMerchandise
{
    public class CreateReceiptOfMerchandiseRequest : BaseRequest
    {
        [JsonIgnore]
        internal override string Endpoint => "rom";

        [JsonPropertyName("rom")]
        public ReceiptOfMerchandise ReceiptOfMerchandise { get; set; }
    }
}
