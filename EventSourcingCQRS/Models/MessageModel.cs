namespace EventSourcingCQRS.Models
{
    public class MessageModel
    {
        public static MessageModel Information(string text)
        {
            return new MessageModel(text, MessageType.Information);
        }

        public static MessageModel Warning(string text)
        {
            return new MessageModel(text, MessageType.Warning);
        }

        public static MessageModel Alert(string text)
        {
            return new MessageModel(text, MessageType.Alert);
        }

        public static MessageModel Confirmation(string text)
        {
            return new MessageModel(text, MessageType.Confirmation);
        }

        public string Text { get; set; }

        public MessageType Type { get; set; }

        public MessageModel(string text, MessageType type)
        {
            Text = text;
            Type = type;
        }

        public enum MessageType
        {
            Warning,
            Information,
            Alert,
            Confirmation
        }
    }
}
