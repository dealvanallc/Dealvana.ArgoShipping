using System.Text.Json.Serialization;

using Dealvana.ArgoShipping.Json;

namespace Dealvana.ArgoShipping.Items
{
    public class Item : JsonPropertyWatcher<Item>
    {
        private int? _id;
        private string _name;
        private bool? _combine;
        private string _description;
        private string _imageUrl;
        private string _isbn;
        private bool? _active;
        private int? _categoryId;
        private string _sku;
        private int? _sellValueInCents;
        private int? _quantityAllocated;
        private int? _quantityAvailable;
        private int? _quantityOnHand;
        private int? _quantityOrdered;
        private int? _quantityPending;
        private int? _quantityReceivedOrBuilt;
        private string _customsCountryOfOrigin;
        private int? _customsValueInCents;
        private bool? _isDigitalItem;
        private int? _leadTimeDays;
        private bool? _notifyReorder;
        private int? _reorderQuantity;
        private int? _reorderThreshold;
        private int? _replacementCostInCents;
        private string _returnInstructions;
        private string _serialNumber;
        private bool? _serialNumberRequired;
        private string _upcCode;

        [JsonPropertyName("id")]
        public int? Id
        {
            get => _id;
            set => _id = PropertyChanged(value);
        }

        [JsonPropertyName("name")]
        public string Name 
        {
            get => _name;
            set => _name = PropertyChanged(value);
        }

        [JsonPropertyName("combine")]
        public bool? Combine
        {
            get => _combine;
            set => _combine = PropertyChanged(value);
        }

        [JsonPropertyName("description")]
        public string Description
        {
            get => _description;
            set => _description = PropertyChanged(value);
        }

        [JsonPropertyName("image_url")]
        public string ImageUrl
        {
            get => _imageUrl;
            set => _imageUrl = PropertyChanged(value);
        }

        [JsonPropertyName("isbn")]
        public string Isbn
        {
            get => _isbn;
            set => _isbn = PropertyChanged(value);
        }

        [JsonPropertyName("is_active")]
        public bool? Active
        {
            get => _active;
            set => _active = PropertyChanged(value);
        }

        [JsonPropertyName("category_id")]
        public int? CategoryId
        {
            get => _categoryId;
            set => _categoryId = PropertyChanged(value);
        }

        [JsonPropertyName("sku")]
        public string Sku
        {
            get => _sku;
            set => _sku = PropertyChanged(value);
        }

        [JsonPropertyName("sell_value_cents")]
        public int? SellValueInCents
        {
            get => _sellValueInCents; 
            set => _sellValueInCents = PropertyChanged(value);
        }

        [JsonPropertyName("counter_allocated")]
        public int? QuantityAllocated
        {
            get => _quantityAllocated;
            set => _quantityAllocated = PropertyChanged(value);
        }

        [JsonPropertyName("counter_available")]
        public int? QuantityAvailable
        {
            get => _quantityAvailable;
            set => _quantityAvailable = PropertyChanged(value);
        }

        [JsonPropertyName("counter_on_hand")]
        public int? QuantityOnHand
        {
            get => _quantityOnHand;
            set => _quantityOnHand = PropertyChanged(value);
        }

        [JsonPropertyName("counter_ordered")]
        public int? QuantityOrdered
        { 
            get => _quantityOrdered;
            set => _quantityOrdered = PropertyChanged(value);
        }

        [JsonPropertyName("counter_pending")]
        public int? QuantityPending
        {
            get => _quantityPending;
            set => _quantityPending = PropertyChanged(value);
        }

        [JsonPropertyName("counter_received_or_built")]
        public int? QuantityReceivedOrBuilt
        {
            get => _quantityReceivedOrBuilt;
            set => _quantityReceivedOrBuilt = PropertyChanged(value);
        }

        [JsonPropertyName("customs_country_of_origin")]
        public string CustomsCountryOfOrigin
        {
            get => _customsCountryOfOrigin;
            set => _customsCountryOfOrigin = PropertyChanged(value);
        }

        [JsonPropertyName("customs_value_cents")]
        public int? CustomsValueInCents
        {
            get => _customsValueInCents;
            set => _customsValueInCents = PropertyChanged(value);
        }

        [JsonPropertyName("is_digital_item")]
        public bool? IsDigitalItem
        {
            get => _isDigitalItem;
            set => _isDigitalItem = PropertyChanged(value);
        }

        [JsonPropertyName("lead_time_days")]
        public int? LeadTimeDays
        {
            get => _leadTimeDays;
            set => _leadTimeDays = PropertyChanged(value);
        }

        [JsonPropertyName("notify_reorder")]
        public bool? NotifyReorder
        {
            get => _notifyReorder;
            set => _notifyReorder = PropertyChanged(value);
        }

        [JsonPropertyName("reorder_quantity")]
        public int? ReorderQuantity
        {
            get => _reorderQuantity;
            set => _reorderQuantity = PropertyChanged(value);
        }

        [JsonPropertyName("reorder_threshold")]
        public int? ReorderThreshold
        {
            get => _reorderThreshold;
            set => _reorderThreshold = PropertyChanged(value);
        }

        [JsonPropertyName("replacement_cost_cents")]
        public int? ReplacementCostInCents
        {
            get => _replacementCostInCents;
            set => _replacementCostInCents = PropertyChanged(value);
        }

        [JsonPropertyName("return_instructions")]
        public string ReturnInstructions
        {
            get => _returnInstructions;
            set => _returnInstructions = PropertyChanged(value);
        }

        [JsonPropertyName("serial_number")]
        public string SerialNumber
        {
            get => _serialNumber;
            set => _serialNumber = PropertyChanged(value);
        }

        [JsonPropertyName("serial_number_required")]
        public bool? SerialNumberRequired
        {
            get => _serialNumberRequired;
            set => _serialNumberRequired = PropertyChanged(value);
        }

        [JsonPropertyName("upc")]
        public string UpcCode
        {
            get => _upcCode;
            set => _upcCode = PropertyChanged(value);
        }
    }
}
