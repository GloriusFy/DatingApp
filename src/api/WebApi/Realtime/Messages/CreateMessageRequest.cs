namespace WebApi.Realtime.Messages
{
    public class CreateMessageRequest
    {
        public CreateMessageRequest(int recipientId, string content)
        {
            RecipientId = recipientId;
            Content = content;
        }

        public int RecipientId { get; set; }

        public string Content { get; set; }
    }
}
