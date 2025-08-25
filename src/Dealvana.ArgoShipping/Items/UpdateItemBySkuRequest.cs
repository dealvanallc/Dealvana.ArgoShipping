using System;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Items
{
    public class UpdateItemBySkuRequest : BaseRequest
    {
        private Item _item;

        [JsonIgnore]
        internal override string Endpoint => "items/by_sku/{sku}/update";

        [PathParameter("sku")]
        public string Sku { get; set; }

        [JsonPropertyName("item")]
        public Item Item
        {
            get => _item ?? throw new InvalidOperationException($"{nameof(Item)} has not been initialized.");
            set => _item = value;
        }
    }
}
