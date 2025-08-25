namespace Dealvana.ArgoShipping.Shipments
{
    public class GetShipmentByIdRequest : BaseRequest
    {
        internal override string Endpoint => "shipments/{id}";

        [PathParameter("id")]
        public int Id { get; set; }
    }
}
