using System;
using System.Runtime.Serialization;
using EventSourcingCQRS.Domain.Core;

namespace EventSourcingCQRS.Domain.CartModule
{

    [Serializable]
    public class CartException : BusinessRuleException<Cart>
    {
        public CartException(string message) : base(message)
        {
        }

        public CartException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CartException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
