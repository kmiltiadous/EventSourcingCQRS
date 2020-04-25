using EventSourcingCQRS.Domain.Core;
using System;

namespace EventSourcingCQRS.Domain.Tests.Utility
{
    public class TestAggregateId : IAggregateId
    {
        public Guid Id => throw new NotImplementedException();

        public string IdAsString()
        {
            return "";
        }
    }
}
