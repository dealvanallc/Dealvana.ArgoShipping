using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Shipments
{
    public class ShipmentItem
    {
        [JsonPropertyName("isbn")]
        public string Isbn { get; set; }

        [JsonPropertyName("harmonized_code")]
        public string HarmonizedCode { get; set; }

        [JsonPropertyName("upc")]
        public string UpcCode { get; set; }

        [JsonPropertyName("quantity_ordered")]
        public int QuantityOrdered { get; set; }

        [JsonPropertyName("quantity_shipped")]
        public int QuantityShipped { get; set; }

        [JsonPropertyName("ci_description")]
        public string CustomsDescription { get; set; }

        [JsonPropertyName("country_of_manufacture")]
        public string CountryOfManufacture { get; set; }

        [JsonPropertyName("package_shipment_line_id")]
        public int PackageShipmentLineId { get; set; }

        [JsonPropertyName("product_code")]
        public string Sku { get; set; }

        [JsonPropertyName("quantity_unit_measure")]
        public string QuantityUnitMeasure { get; set; }

        [JsonPropertyName("scan_id")]
        public int ScanId { get; set; }

        [JsonPropertyName("unit_value")]
        public int? UnitValue { get; set; }

        [JsonPropertyName("unit_weight")]
        public decimal? UnitWeight { get; set; }
    }
}