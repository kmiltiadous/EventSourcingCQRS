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
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;
        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var command = new GetCustomersQuery();
            var commandResult = await mediator.Send(command);
            return new OkObjectResult(new ActionResponse<IEnumerable<Customer>>(commandResult));
        }
    }
}