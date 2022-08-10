using BasketService.BLL.Entities.Insert;
using BasketService.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using ServiceMessaging.MessageQueue;

namespace BasketService.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json", "application/xml")]
    public class CartServiceController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartServiceController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Get a single cart by id
        /// </summary>
        /// <response code="200">The cart was found</response>
        /// <response code="404">The cart was not found</response>
        /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
        /// <response code="415">When a response is specified in an unsupported content type</response>
        /// <response code="422">If query params structure is valid, but the values fail validation</response>
        /// <response code="500">A server fault occurred</response>
        [MapToApiVersion("1.0")]
        [HttpGet("{cartId}", Name = nameof(GetCartInfo))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCartInfo([FromRoute] string cartId)
        {
            return Ok(_cartService.GetCartById(cartId));
        }

        /// <summary>
        /// Add a new item
        /// </summary>
        /// <response code="201">The item was added successfully/response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
        /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
        /// <response code="415">When a response is specified in an unsupported content type</response>
        /// <response code="422">If query params structure is valid, but the values fail validation</response>
        /// <response code="500">A server fault occurred</response>
        [MapToApiVersion("1.0")]
        [HttpPost(Name = nameof(AddItem))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddItem([FromBody] CartInsertViewModel cart)
        {
            _cartService.AddItem(cart);
            return Ok();
        }

        /// <summary>
        /// Delete cart
        /// </summary>
        /// <response code="204">The cart was deleted successfully.</response>
        /// <response code="404">A cart having specified cart id was not found</response>
        /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
        /// <response code="415">When a response is specified in an unsupported content type</response>
        /// <response code="422">If query params structure is valid, but the values fail validation</response>
        /// <response code="500">A server fault occurred</response>
        [MapToApiVersion("1.0")]
        [HttpDelete("{cartId}/item/{itemId:int}", Name = nameof(DeleteItem))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteItem([FromRoute] string cartId, [FromRoute] int itemId)
        {
            _cartService.DeleteItem(cartId, itemId);
            return Ok();
        }
    }
}
