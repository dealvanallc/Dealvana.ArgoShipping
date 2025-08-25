namespace Dealvana.ArgoShipping.ReceiptsOfMerchandise
{
    public class GetReceiptOfMerchandiseByIdRequest : BaseRequest
    {
        internal override string Endpoint => "rom/{id}";

        [PathParameter("id")]
        public int ReceiptOfMerchandiseId { get; set; }
    }
}
