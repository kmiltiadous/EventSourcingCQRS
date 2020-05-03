using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventSourcingCQRS.API.MessageContracts;
using EventSourcingCQRS.Application.Commands;
using EventSourcingCQRS.Application.Queries;
using EventSourcingCQRS.ReadModel.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EventSourcingCQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IMediator mediator;
        public CartsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var query = new GetCartsQuery();
            var result = await mediator.Send(query);

            return new OkObjectResult(new ActionResponse<IEnumerable<Cart>>(result));
        }

        [HttpGet]
        [Route("{id:regex(^Cart-.+$)}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var query = new GetCartQuery(id);
            var result = mediator.Send(query).Result;

            return new OkObjectResult(new ActionResponse<Cart>(result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCartRequest request)
        {
            var command = new CreateCartCommand(request.CustomerId, request.CartName);
            var commandResult = await mediator.Send(command);
            if (commandResult.WasSuccessful)
            {
                return new OkObjectResult(new ActionResponse<string>(commandResult.Value.ToString()));
            }

            return new ObjectResult(new ActionResponse(commandResult.BrokenRules.First()))
            {
                StatusCode = 402
            };
        }
        
        [HttpGet]
        [Route("{id:regex(^Cart-.+$)}/items")]
        public async Task<IActionResult> GetItemsAsync(string id)
        {
            var query = new GetCartItemsQuery(id);
            var result = await mediator.Send(query);
        
            return new OkObjectResult(new ActionResponse<IEnumerable<CartItem>>(result));
        }


        [Route("{id:length(41)}/items")]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync(CartItemRequest request)
        {
            var command = new AddProductCommand(request.CartId, request.ProductId, request.Quantity);
            var commandResult = await mediator.Send(command);
            if (commandResult.WasSuccessful)
            {
                return new OkObjectResult(new ActionResponse());
            }

            return new ObjectResult(new ActionResponse(commandResult.BrokenRules.First()))
            {
                StatusCode =  (int)HttpStatusCode.Conflict
            };
        }

        [Route("{id:length(41)}")]
        [HttpPut]
        public async Task<IActionResult> ChangeProductQuantityAsync(CartItemRequest request)
        {
            var command = new ChangeProductQuantityCommand(request.CartId, request.ProductId, request.Quantity);
            var commandResult = await mediator.Send(command);
            if (commandResult.WasSuccessful)
            {
                return new OkObjectResult(new ActionResponse());
            }

            return new ObjectResult(new ActionResponse(commandResult.BrokenRules.First()))
            {
                StatusCode = (int)HttpStatusCode.Conflict
            };
        }

    }
}