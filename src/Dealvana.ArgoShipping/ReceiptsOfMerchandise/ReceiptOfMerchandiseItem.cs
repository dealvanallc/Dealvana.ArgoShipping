using System.Text.Json.Serialization;

using Dealvana.ArgoShipping.Json;

namespace Dealvana.ArgoShipping.ReceiptsOfMerchandise
{
    public class ReceiptOfMerchandiseItem : JsonPropertyWatcher<ReceiptOfMerchandiseItem>
    {
        private int? _itemId;
        private bool? _cartonCount;
        private bool? _isBarcoded;
        private int? _quantityExpected;
        private int? _quantityReceived;
        private bool _skidCount;

        [JsonPropertyName("id")]
        public int? ItemId
        {
            get => _itemId;
            set => _itemId = PropertyChanged(value);
        }

        [JsonPropertyName("carton_count")]
        public bool? CartonCount
        {
            get => _cartonCount;
            set => _cartonCount = PropertyChanged(value);
        }

        [JsonPropertyName("is_barcoded")]
        public bool? IsBarcoded
        {
            get => _isBarcoded;
            set => _isBarcoded = PropertyChanged(value);
        }

        [JsonPropertyName("quantity_expected")]
        public int? QuantityExpected
        {
            get => _quantityExpected;
            set => _quantityExpected = PropertyChanged(value);
        }

        [JsonPropertyName("quantity_received")]
        public int? QuantityReceived
        {
            get => _quantityReceived;
            set => _quantityReceived = PropertyChanged(value);
        }

        [JsonPropertyName("skid_count")]
        public bool SkidCount
        {
            get => _skidCount;
            set => _skidCount = PropertyChanged(value);
        }
    }
}