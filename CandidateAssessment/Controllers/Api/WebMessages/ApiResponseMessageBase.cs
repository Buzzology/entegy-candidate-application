using CandidateAssessment.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CandidateAssessment.Controllers.Api.WebMessages
{
    public class ApiMessageResponseBase : ApiMessageBase
    {
        public bool Success { get; set; }
        private List<Message> _messages { get; set; }
        public List<Message> Messages { get { return _messages ?? (_messages = new List<Message>()); } set { _messages = value; } }
        public dynamic Data { get; set; }

        public void AddError(string text)
        {
            Messages.Add(new Message(text, Message.MessageType.Error));
            Success = false;
        }

        public bool AtLeastError()
        {
            return Messages.Any(x => x.Type.Equals(Message.MessageType.Error));
        }
    }
}
