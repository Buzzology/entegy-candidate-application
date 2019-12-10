namespace CandidateAssessment.Utilities
{
    public class Message
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public MessageType Type { get; set; }


        public Message() { }

        public Message(string text, MessageType type)
        {
            Text = text;
            Type = type;
        }

        public Message(string id, string text, MessageType type)
        {
            Id = id;
            Text = text;
            Type = type;
        }

        public enum MessageType
        {
            Error = 0,
            Success = 1,
            Information = 2,
            Warning = 3
        }
    }
}
