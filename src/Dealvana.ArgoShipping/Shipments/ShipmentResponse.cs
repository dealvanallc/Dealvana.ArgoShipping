using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Shipments
{
    public class ShipmentResponse : BaseResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("shipment_date")]
        public DateTime? ShipmentDate { get; set; }

        [JsonPropertyName("ship_to_zip")]
        public string ShipToZipCode { get; set; }

        [JsonPropertyName("third_party_account")]
        public string ThirdPartyAccount { get; set; }

        [JsonPropertyName("tracking_number")]
        public string TrackingNumber { get; set; }

        [JsonPropertyName("ship_to_street_1")]
        public string ShipToStreet1 { get; set; }

        [JsonPropertyName("ship_to_phone")]
        public string ShipToPhone { get; set; }

        [JsonPropertyName("ship_to_state_code")]
        public string ShipToStateCode { get; set; }

        [JsonPropertyName("ship_to_state_name")]
        public string ShipToStateName { get; set; }

        [JsonPropertyName("freight")]
        public decimal FreightCost { get; set; }

        [JsonPropertyName("requested_by")]
        public string RequestedBy { get; set; }

        [JsonPropertyName("weight")]
        public string Weight { get; set; }

        [JsonPropertyName("packaging_sell")]
        public decimal PackagingCost { get; set; }

        [JsonPropertyName("ship_to_city")]
        public string ShipToCity { get; set; }

        [JsonPropertyName("argo_printed_mnarketing_sell")]
        public decimal ArgoPrintedMarketingCost { get; set; }

        [JsonPropertyName("requested_date")]
        public DateTime RequestedDate { get; set; }

        [JsonPropertyName("ship_to_street_3")]
        public string ShipToStreet3 { get; set; }

        [JsonPropertyName("ship_to_country_code")]
        public string ShipToCountryCode { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("is_reship")]
        public bool IsReship { get; set; }

        [JsonPropertyName("shipment_items")]
        public List<ShipmentItem> ShipmentItems { get; set; }

        [JsonPropertyName("ship_to_street_2")]
        public string ShipToStreet2 { get; set; }

        [JsonPropertyName("total_sell")]
        public decimal TotalCost { get; set; }

        [JsonPropertyName("ship_to_name")]
        public string ShipToName { get; set; }

        [JsonPropertyName("carrier_zone")]
        public string CarrierZone { get; set; }

        [JsonPropertyName("handling_sell")]
        public decimal HandlingCost { get; set; }

        [JsonPropertyName("ready_to_ship_date")]
        public DateTime? ReadyToShipDate { get; set; }

        [JsonPropertyName("ship_to_email")]
        public string ShipToEmail { get; set; }

        [JsonPropertyName("exceptions")]
        public List<string> Exceptions { get; set; }

        [JsonPropertyName("ship_to_country_name")]
        public string ShipToCountryName { get; set; }

        [JsonPropertyName("supplied_marketing_sell")]
        public decimal SuppliedMarketingCost { get; set; }

        [JsonPropertyName("dimensions")]
        public string Dimensions { get; set; }
    }
}
