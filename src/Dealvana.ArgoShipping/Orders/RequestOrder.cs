using System.Collections.Generic;
using System.Text.Json.Serialization;

using Dealvana.ArgoShipping.Json;

namespace Dealvana.ArgoShipping.Orders
{
    public class RequestOrder : JsonPropertyWatcher<RequestOrder>
    {
        private string _customerOrderId;
        private List<RequestOrderItem> _items;
        private string _shipToName;
        private string _shipToEmail;
        private string _shipToStreet1;
        private string _shipToStreet2;
        private string _shipToCity;
        private string _shipToSate;
        private string _shipToZipCode;
        private string _shipToCountry;
        private string _shippingMethod;

        [JsonPropertyName("customer_order_id")]
        public string CustomerOrderId
        {
            get => _customerOrderId;
            set => _customerOrderId = PropertyChanged(value);
        }

        [JsonPropertyName("items")]
        public List<RequestOrderItem> Items
        {
            get => _items;
            set => _items = PropertyChanged(value);
        }

        [JsonPropertyName("ship_to_name")]
        public string ShipToName
        {
            get => _shipToName;
            set => _shipToName = PropertyChanged(value);
        }

        [JsonPropertyName("ship_to_email")]
        public string ShipToEmail
        {
            get => _shipToEmail;
            set => _shipToEmail = PropertyChanged(value);
        }

        [JsonPropertyName("ship_to_street_1")]
        public string ShipToStreet1
        {
            get => _shipToStreet1;
            set => _shipToStreet1 = PropertyChanged(value);
        }

        [JsonPropertyName("ship_to_street_2")]
        public string ShipToStreet2
        {
            get => _shipToStreet2;
            set => _shipToStreet2 = PropertyChanged(value);
        }

        [JsonPropertyName("ship_to_city")]
        public string ShipToCity
        {
            get => _shipToCity;
            set => _shipToCity = PropertyChanged(value);
        }

        [JsonPropertyName("ship_to_state_code")]
        public string ShipToSate
        {
            get => _shipToSate;
            set => _shipToSate = PropertyChanged(value);
        }

        [JsonPropertyName("ship_to_zip")]
        public string ShipToZipCode
        {
            get => _shipToZipCode;
            set => _shipToZipCode = PropertyChanged(value);
        }

        [JsonPropertyName("ship_to_country_code")]
        public string ShipToCountry
        {
            get => _shipToCountry;
            set => _shipToCountry = PropertyChanged(value);
        }

        [JsonPropertyName("shipping_method")]
        public string ShippingMethod
        {
            get => _shippingMethod;
            set => _shippingMethod = PropertyChanged(value);
        }
    }
}