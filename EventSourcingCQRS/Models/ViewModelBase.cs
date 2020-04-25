using System.Collections.Generic;
using System.Linq;

namespace EventSourcingCQRS.Models
{
    public class ViewModelBase
    {
        private readonly List<MessageModel> messages = new List<MessageModel>();

        public IEnumerable<MessageModel> Messages => messages;

        public void AddMessage(MessageModel message)
        {
            if (message != null)
            {
                messages.Add(message);
            }
        }

        public bool WasSuccessful => Messages == null || !Messages.Any();
    }
}
