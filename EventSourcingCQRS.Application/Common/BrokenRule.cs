namespace EventSourcingCQRS.Application.Common
{
    public class BrokenRule
    {
        public BrokenRule(string message) : this(message, Severity.Unknown)
        {
        }

        public BrokenRule(string message, Severity severity)
        {
            Message = message;
            Severity = severity;
        }

        public string Message { get; private set; }

        public Severity Severity { get; private set; }

    }
}
