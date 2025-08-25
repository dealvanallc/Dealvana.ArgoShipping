using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping.Categories
{
    public class CategoriesResponse : BaseResponse
    {
        [JsonPropertyName("categories")]
        public IList<Category> Categories { get; set; }
    }
}
