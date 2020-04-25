using EventSourcingCQRS.ReadModel.Models;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCartSummaryQuery : IRequest<CartSummary>
    {}
}
