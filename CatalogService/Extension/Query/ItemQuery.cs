using Microsoft.AspNetCore.Mvc;

namespace ViewModel.Query
{
    public class ItemQuery : PagedQueryParams
    {
        [FromQuery(Name = "item-category-id")]
        public int? CategoryId { get; set; }
    }
}
