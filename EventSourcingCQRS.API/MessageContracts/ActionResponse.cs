using System.Collections.Generic;
using System.Linq;
using EventSourcingCQRS.Application.Common;

namespace EventSourcingCQRS.API.MessageContracts
{
    public class ActionResponse
    {
        public static ActionResponse Success()
        {
            return new ActionResponse();
        }

        public ActionResponse() : this(new List<BrokenRule>())
        {
        }

        public ActionResponse(BrokenRule brokenRule)
            : this(brokenRule == null ? new List<BrokenRule>() : new List<BrokenRule> { brokenRule })
        {
        }

        public ActionResponse(IEnumerable<BrokenRule> brokenRules)
        {
            BrokenRules = brokenRules ?? new List<BrokenRule>();
            WasSuccessful = !BrokenRules.Any();
        }

        public IEnumerable<BrokenRule> BrokenRules { get; private set; }

        public bool WasSuccessful { get; private set; }

        protected void Add(params IEnumerable<BrokenRule>[] brokenRulesSet)
        {
            BrokenRules = brokenRulesSet == null ? new List<BrokenRule>() : brokenRulesSet.SelectMany(rules => rules).ToList();
            WasSuccessful = !BrokenRules.Any();
        }
    }

    public class ActionResponse<T> : ActionResponse
    {
        public T Value { get; }
        public ActionResponse(T _value) : base()
        {
            Value = _value;
        }

        public ActionResponse(BrokenRule brokenRule)
            : base(brokenRule)
        {
        }

        public ActionResponse(IEnumerable<BrokenRule> brokenRules)
            : base(brokenRules)
        {
        }
    }
}
