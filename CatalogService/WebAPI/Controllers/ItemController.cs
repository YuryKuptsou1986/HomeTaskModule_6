using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ViewModel.Insert;
using ViewModel.Query;
using ViewModel.Update;
using WebAPI.Resources;

namespace WebAPI.Controllers
{
    [Route("items")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json", "application/xml")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ItemResourceFactory _itemResourceFactory;

        public ItemController(IItemService itemService, ItemResourceFactory itemResourceFactory)
        {
            _itemService = itemService;
            _itemResourceFactory = itemResourceFactory;
        }

        /// <summary>
        /// Get a single item by id
        /// </summary>
        /// <response code="200">The item was found</response>
        /// <response code="404">The item was not found</response>
        /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
        /// <response code="415">When a response is specified in an unsupported content type</response>
        /// <response code="422">If query params structure is valid, but the values fail validation</response>
        /// <response code="500">A server fault occurred</response>
        [HttpGet("{itemId:int}", Name = nameof(GetItemById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItemById([FromRoute]int itemId)
        {
            var item = await _itemService.GetAsync(itemId);
            return Ok(_itemResourceFactory.CreateItemResource(item));
        }

        /// <summary>
        /// Get a paginated list of items. The pagination metadata is contained within 'x-pagination' header of response.
        /// </summary>
        /// <response code="200">A paginated list of items</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
        /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
        /// <response code="415">When a response is specified in an unsupported content type</response>
        /// <response code="422">If query params structure is valid, but the values fail validation</response>
        /// <response code="500">A server fault occurred</response>
        [HttpGet(Name = nameof(GetItemList))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItemList([FromQuery] ItemQuery itemQuery)
        {
            var items = await _itemService.ListAsync(itemQuery);

            var metadata = new {
                items.ItemCount,
                items.PageSize,
                items.CurrentPageNumber,
                items.PageCount,
                items.HasNext,
                items.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(_itemResourceFactory.CreateUserResourceList(items, itemQuery));
        }

        /// <summary>
        /// Update an existing item
        /// </summary>
        /// <response code="204">The item was updated successfully</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
        /// <response code="404">The item was not found for specified category id</response>
        /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
        /// <response code="415">When a response is specified in an unsupported content type</response>
        /// <response code="422">If query params structure is valid, but the values fail validation</response>
        [HttpPut("{itemId:int}", Name = nameof(UpdateItem))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateItem([FromRoute]int itemId, [FromBody]ItemUpdateModel item)
        {
            await _itemService.UpdateAsync(itemId, item);
            return Ok();
        }

        /// <summary>
        /// Create a new item
        /// </summary>
        /// <response code="201">The item was created successfully/response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
        /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
        /// <response code="415">When a response is specified in an unsupported content type</response>
        /// <response code="422">If query params structure is valid, but the values fail validation</response>
        /// <response code="500">A server fault occurred</response>
        [HttpPost(Name = nameof(CreateItem))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateItem([FromBody]ItemInsertModel item)
        {
            await _itemService.AddAsync(item);
            return Ok(item);
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <response code="204">The item was deleted successfully.</response>
        /// <response code="404">A item having specified item id was not found</response>
        /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
        /// <response code="415">When a response is specified in an unsupported content type</response>
        /// <response code="422">If query params structure is valid, but the values fail validation</response>
        /// <response code="500">A server fault occurred</response>
        [HttpDelete("{itemId:int}", Name = nameof(DeleteItem))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteItem([FromRoute]int itemId)
        {
            await _itemService.DeleteAsync(itemId);
            return Ok();
        }
    }
}
