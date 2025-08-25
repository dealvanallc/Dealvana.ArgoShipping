using System;

namespace Dealvana.ArgoShipping.Shipments
{
    public class ListShipmentsRequest : BaseRequest
    {
        internal override string Endpoint => "shipments";

        [QueryStringParameter("page")]
        public int Page { get; set; }

        [QueryStringParameter("per_page")]
        public int PerPage { get; set; }

        [QueryStringParameter("status")]
        public string Status { get; set; }

        [QueryStringParameter("tenant_id")]
        public int? TenantId { get; set; }

        [QueryStringParameter("shipping_method")]
        public string ShippingMethod { get; set; }

        [QueryStringParameter("q")]
        public string SearchQuery { get; set; }

        [QueryStringParameter("start_date")]
        public DateTime? StartDate { get; set; }

        [QueryStringParameter("end_date")]
        public DateTime? EndDate { get; set; }

        [QueryStringParameter("date_field")]
        public string DateField { get; set; }
    }
}
