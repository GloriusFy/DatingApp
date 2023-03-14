using NodaTime;

namespace Application.Messages
{
    public class MessageDto
    {
        public MessageDto(string content, Instant sendDate, Instant? readDate, int senderId, int recipientId)
        {
            Content = content;
            SendDate = sendDate;
            ReadDate = readDate;
            SenderId = senderId;
            RecipientId = recipientId;
        }

        public int SenderId { get; private set; }

        public int RecipientId { get; private set; }

        public string Content { get; private set; }

        public Instant SendDate { get; private set; }

        public Instant? ReadDate { get; private set; }
    }
}
