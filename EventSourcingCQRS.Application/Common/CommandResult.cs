using System.Collections.Generic;
using System.Linq;

namespace EventSourcingCQRS.Application.Common
{
    public class CommandResult
    {
        public CommandResult() : this(new List<BrokenRule>())
        {
        }

        public CommandResult(BrokenRule brokenRule)
            : this(brokenRule == null ? new List<BrokenRule>() : new List<BrokenRule> { brokenRule })
        {
        }

        public CommandResult(IEnumerable<BrokenRule> brokenRules)
        {
            BrokenRules = brokenRules ?? new List<BrokenRule>();
            WasSuccessful = brokenRules?.Count() <= 0;
        }

        public IEnumerable<BrokenRule> BrokenRules { get; }

        public bool WasSuccessful { get; }
    }

}
