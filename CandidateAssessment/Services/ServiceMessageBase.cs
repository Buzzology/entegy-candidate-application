using CandidateAssessment.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessment.Services
{
    public class ServiceMessageBase
    {
        public ServiceMessageBase(ApplicationDbContext db)
        {
            Db = db;
        }

        public ApplicationDbContext Db { get; set; }

        public virtual string CurrentUserId { get; set; }

        public bool Success { get; set; }

        private List<Message> _messages { get; set; }
        public List<Message> Messages { get { return _messages ?? (_messages = new List<Message>()); } set { _messages = value; } }

        public void AddError(string text)
        {
            Messages.Add(new Message(text, Message.MessageType.Error));
            Success = false;
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);

            if (message.Type == Message.MessageType.Error)
            {
                Success = false;
            }
        }

        public void AddMessages(IEnumerable<Message> messages)
        {
            Messages.AddRange(messages);

            if (messages.Any(x => x.Type == Message.MessageType.Error))
            {
                Success = false;
            }
        }

        public void AddWarning(string text)
        {
            Messages.Add(new Message(text, Message.MessageType.Warning));
        }

        public void AddNotice(string text)
        {
            Messages.Add(new Message(text, Message.MessageType.Information));
        }

        public bool AtLeastError()
        {
            return Messages.Any(x => x.Type.Equals(Message.MessageType.Error));
        }

        public int Page { get; set; }

        public int MaxPageSize { get; set; }
    }
}