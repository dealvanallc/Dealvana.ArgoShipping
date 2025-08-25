using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Dealvana.ArgoShipping.Json;

namespace Dealvana.ArgoShipping.ReceiptsOfMerchandise
{
    public class ReceiptOfMerchandise : JsonPropertyWatcher<ReceiptOfMerchandise>
    {
        private int? _id;
        private string _status;
        private string _quote;
        private string _department;
        private DateTime? _expectedDate;
        private string _carrier;
        private string _notes;
        private string _trackingNumber;
        private List<ReceiptOfMerchandiseItem> _items;
        private string _billOfLading;
        private bool? _repeat;
        private bool? _rush;

        [JsonPropertyName("id")]
        public int? Id
        {
            get => _id;
            set => _id = PropertyChanged(value);
        }

        [JsonPropertyName("status")]
        public string Status
        {
            get => _status;
            set => _status = PropertyChanged(value);
        }

        [JsonPropertyName("quote")]
        public string Quote
        {
            get => _quote;
            set => _quote = PropertyChanged(value);
        }

        [JsonPropertyName("department")]
        public string Department
        {
            get => _department;
            set => _department = PropertyChanged(value);
        }

        [JsonPropertyName("expected_date")]
        public DateTime? ExpectedDate
        {
            get => _expectedDate;
            set => _expectedDate = PropertyChanged(value);
        }

        [JsonPropertyName("carrier")]
        public string Carrier
        {
            get => _carrier;
            set => _carrier = PropertyChanged(value);
        }

        [JsonPropertyName("notes")]
        public string Notes
        {
            get => _notes;
            set => _notes = PropertyChanged(value);
        }

        [JsonPropertyName("tracking_number")]
        public string TrackingNumber
        {
            get => _trackingNumber;
            set => _trackingNumber = PropertyChanged(value);
        }

        [JsonPropertyName("rom_items")]
        public List<ReceiptOfMerchandiseItem> Items
        {
            get => _items;
            set => _items = PropertyChanged(value);
        }

        [JsonPropertyName("bill_of_lading")]
        public string BillOfLading
        {
            get => _billOfLading;
            set => _billOfLading = PropertyChanged(value);
        }

        [JsonPropertyName("repeat")]
        public bool? Repeat
        {
            get => _repeat;
            set => _repeat = PropertyChanged(value);
        }

        [JsonPropertyName("rush")]
        public bool? Rush
        {
            get => _rush;
            set => _rush = PropertyChanged(value);
        }
    }
}
