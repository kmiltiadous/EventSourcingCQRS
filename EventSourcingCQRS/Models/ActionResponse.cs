using System.Collections.Generic;

namespace EventSourcingCQRS.Models
{
    public class ActionResponse
    {

        public IEnumerable<BrokenRule> BrokenRules { get; set; }

        public bool WasSuccessful { get; set; }
    }

    public class ActionResponse<T> : ActionResponse
    {
        public T Value { get; set; }
    }
}
