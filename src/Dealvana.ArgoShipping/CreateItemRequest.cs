using System;
using System.Text.Json.Serialization;

using Dealvana.ArgoShipping.Items;

namespace Dealvana.ArgoShipping
{
    public class CreateItemRequest : BaseRequest
    {
        private Item _item;

        [JsonIgnore]
        internal override string Endpoint => "items";

        [JsonPropertyName("item")]
        public Item Item
        {
            get => _item ?? throw new InvalidOperationException($"{nameof(Item)} has not been initialized");
            set => _item = value;
        }

        internal override void Validate()
        {
            if (string.IsNullOrEmpty(Item.Sku))
            {
                throw new InvalidOperationException($"{nameof(Item)}.{nameof(Item.Sku)} is required");
            }

            if (string.IsNullOrEmpty(Item.Name))
            {
                throw new InvalidOperationException($"{nameof(Item)}.{nameof(Item.Name)} is required");
            }
        }
    }
}
