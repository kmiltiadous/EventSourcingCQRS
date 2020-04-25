﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventSourcingCQRS.ReadModel.Models;

namespace EventSourcingCQRS.Application.Services
{
    public interface ICartReader
    {
        Task<Cart> GetByIdAsync(string id);

        Task<IEnumerable<Cart>> FindAllAsync(Expression<Func<Cart, bool>> predicate);

        Task<IEnumerable<CartItem>> GetItemsOfAsync(string cartId);
    }
}
