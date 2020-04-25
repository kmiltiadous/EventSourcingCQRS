﻿using Microsoft.AspNetCore.Mvc;
using EventSourcingCQRS.ReadModel.Persistence;
using System.Threading.Tasks;
using EventSourcingCQRS.Models;
using System.Linq;
using EventSourcingCQRS.Application.Services;
using EventSourcingCQRS.ReadModel.Models;

namespace EventSourcingCQRS.Controllers
{

    public class CartsController : Controller
    {
        private readonly ICartReader cartReader;
        private readonly ICartWriter cartWriter;
        private readonly IReadOnlyRepository<Customer> customerRepository;
        private readonly IReadOnlyRepository<Product> productRepository;

        public CartsController(ICartReader cartReader, ICartWriter cartWriter, 
            IReadOnlyRepository<Customer> customerRepository, IReadOnlyRepository<Product> productRepository)
        {
            this.cartReader = cartReader;
            this.cartWriter = cartWriter;
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(new CartIndexViewModel(
                await cartReader.FindAllAsync(x => true),
                (await customerRepository.FindAllAsync(x => true)).ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string customerId)
        {
            await cartWriter.CreateAsync(customerId);
            return RedirectToAction(nameof(IndexAsync));
        }

        [Route("Carts/{id:length(41)}")]
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var viewModel = new CartDetailsViewModel(
                await cartReader.GetByIdAsync(id),
                (await cartReader.GetItemsOfAsync(id)).ToList(),
                (await productRepository.FindAllAsync(x => true)).ToList());
            return View(viewModel);
        }

        [HttpPost]
        [Route("Carts/{id:length(41)}/AddProduct")]
        public async Task<IActionResult> AddProductAsync(string id, string productId, int quantity)
        {
            await cartWriter.AddProductAsync(id, productId, quantity);
            return RedirectToAction(nameof(DetailsAsync), new { id });
        }

        [Route("Carts/{id:length(41)}/ChangeProductQuantity")]
        [HttpPost]
        public async Task<IActionResult> ChangeProductQuantityAsync(string id, string productId, int quantity)
        {
            await cartWriter.ChangeProductQuantityAsync(id, productId, quantity);
            return RedirectToAction(nameof(DetailsAsync), new { id });
        }
    }
}