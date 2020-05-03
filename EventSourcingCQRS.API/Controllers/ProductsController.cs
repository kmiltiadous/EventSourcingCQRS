using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcingCQRS.API.MessageContracts;
using EventSourcingCQRS.Application.Queries;
using EventSourcingCQRS.ReadModel.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingCQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var command = new GetProductsQuery();
            var commandResult = await mediator.Send(command);
            return new OkObjectResult(new ActionResponse<IEnumerable<Product>>(commandResult));
        }
    }
}