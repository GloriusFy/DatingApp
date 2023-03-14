using NodaTime;

namespace Domain.Aggregates.Users.Entities
{
    public class Message
    {
        private Message(int id, int senderId, int recipientId, string content,
            Instant sendDate, Instant? readDate, bool senderDeleted, bool recipientDeleted)
        {
            Id = id;
            SenderId = senderId;
            RecipientId = recipientId;
            Content = content;
            SendDate = sendDate;
            ReadDate = readDate;
            SenderDeleted = senderDeleted;
            RecipientDeleted = recipientDeleted;
        }

        public Message(int senderId, int recipientId, string content, Instant now)
        {
            SenderId = senderId;
            RecipientId = recipientId;
            Content = content;
            SendDate = now;
        }

        public int Id { get; private set; }

        public int SenderId { get; private set; }

        public int RecipientId { get; private set; }

        public string Content { get; private set; }

        public Instant SendDate { get; private set; }

        public Instant? ReadDate { get; private set; }

        public User Sender { get; private set; } = null!;

        public User Recipient { get; private set; } = null!;

        public bool SenderDeleted { get; set; }

        public bool RecipientDeleted { get; set; }
    }
}
