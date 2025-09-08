using CQRS.Core.Events;

namespace Post.Common.Events
{
    public class PostCreatedEvent : CQRS.Core.Events.BaseEvent
    {
        public PostCreatedEvent() : base(nameof(PostCreatedEvent))
        {
        }

        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime DatePosted { get; set; }
    }
}