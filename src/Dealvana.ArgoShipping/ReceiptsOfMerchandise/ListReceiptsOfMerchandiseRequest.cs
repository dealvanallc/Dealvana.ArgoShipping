namespace Dealvana.ArgoShipping.ReceiptsOfMerchandise
{
    public class ListReceiptsOfMerchandiseRequest : BaseRequest
    {
        internal override string Endpoint => "roms";

        [QueryStringParameter("page")]
        public int? Page { get; set; }

        [QueryStringParameter("per_page")]
        public int? PerPage { get; set; }

        [QueryStringParameter("status")]
        public string Status { get; set; }

        [QueryStringParameter("q")]
        public string SearchQuery { get; set; }
    }
}
