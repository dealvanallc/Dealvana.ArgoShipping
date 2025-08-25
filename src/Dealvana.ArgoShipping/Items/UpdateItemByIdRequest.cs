using System;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Items
{
    public class UpdateItemByIdRequest : BaseRequest
    {
        private Item _item;

        [JsonIgnore]
        internal override string Endpoint => "items/by_id/{id}/update";

        [PathParameter("id")]
        public int Id { get; set; }

        [JsonPropertyName("item")]
        public Item Item
        {
            get => _item ?? throw new InvalidOperationException($"{nameof(Item)} has not been initialized.");
            set => _item = value;
        }
    }
}
