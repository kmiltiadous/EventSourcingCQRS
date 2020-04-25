using System;

namespace EventSourcingCQRS.Domain.Core
{
    public class BusinessRuleException<T> : Exception
    {
        public BusinessRuleException(string message) : this(message,null)
        { }

        public BusinessRuleException(string message, Exception inner) : base(message, inner)
        {
        }
        protected BusinessRuleException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
