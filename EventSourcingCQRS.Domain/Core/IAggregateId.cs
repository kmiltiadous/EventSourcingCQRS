using System;

namespace EventSourcingCQRS.Domain.Core
{
    public interface IAggregateId
    {
        Guid Id { get;}
        string IdAsString();
    }
}
