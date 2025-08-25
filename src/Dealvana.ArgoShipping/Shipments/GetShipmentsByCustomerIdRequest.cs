using System;

namespace Dealvana.ArgoShipping.Shipments
{
    public class GetShipmentsByCustomerIdRequest : BaseRequest
    {
        internal override string Endpoint => "shipments/by_customer_order_id/{id}";

        [PathParameter("id")]
        public string CustomerOrderId { get; set; }

        internal override void Validate()
        {
            if (string.IsNullOrEmpty(CustomerOrderId))
            {
                throw new InvalidOperationException($"{nameof(CustomerOrderId)} must not be null or empty");
            }
        }
    }
}
