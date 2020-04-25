using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EventSourcingCQRS.Models;
using AutoMapper;
using MediatR;
using EventSourcingCQRS.Application.Commands;
using EventSourcingCQRS.Application.Queries;

namespace EventSourcingCQRS.Controllers
{
    public class CartsV2Controller : Controller
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CartsV2Controller(
            IMediator mediator,
            IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var viewModel = await GetIndexViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string customerId)
        {
            var command = new CreateCartCommand(customerId);
            var commandResult = await mediator.Send(command);
            if (commandResult.WasSuccessful)
            {
                return RedirectToAction(nameof(IndexAsync));
            }

            var viewModel = await GetIndexViewModel();
            viewModel.AddMessage(MessageModel.Alert(commandResult.BrokenRules.First().Message));

            return View(nameof(IndexAsync), viewModel);
        }

        [Route("{id:length(41)}")]
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var viewModel = await GetCartDetailsViewModel(id);
 
            return View(viewModel);
        }

        [Route("{id:length(41)}/AddProduct")]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync(string id, string productId, int quantity)
        {
            var command = new GetCartDetailsCommand(id, productId, quantity);
            var commandResult = await mediator.Send(command);
            if(commandResult.WasSuccessful)
            {
                return RedirectToAction(nameof(DetailsAsync), new { id });
            }

            var viewModel = await GetCartDetailsViewModel(id);
            viewModel.AddMessage(MessageModel.Alert(commandResult.BrokenRules.First().Message));

            return View(nameof(DetailsAsync), viewModel);
        }

        [Route("{id:length(41)}/ChangeProductQuantity")]
        [HttpPost]
        public async Task<IActionResult> ChangeProductQuantityAsync(string id, string productId, int quantity)
        {
            var command = new ChangeProductQuantityCommand(id, productId, quantity);
            var commandResult = await mediator.Send(command);
            if (commandResult.WasSuccessful)
            {
                return RedirectToAction(nameof(DetailsAsync), new { id });
            }

            var viewModel = await GetCartDetailsViewModel(id);
            viewModel.AddMessage(MessageModel.Alert(commandResult.BrokenRules.First().Message));

            return View(nameof(DetailsAsync), viewModel);
        }

        private async Task<CartDetailsViewModel> GetCartDetailsViewModel(string id)
        {
            var query = new GetCartDetailsQuery(id);
            var result = await mediator.Send(query);
            var viewModel = mapper.Map<CartDetailsViewModel>(result);

            return viewModel;
        }

        private async Task<CartIndexViewModel> GetIndexViewModel()
        {
            var query = new GetCartSummaryQuery();
            var result = await mediator.Send(query);
            var viewModel = mapper.Map<CartIndexViewModel>(result);

            return viewModel;
        }
    }
}