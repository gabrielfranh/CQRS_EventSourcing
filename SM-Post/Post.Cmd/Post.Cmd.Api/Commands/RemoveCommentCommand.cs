using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public class RemoveCommentCommand : BaseCommand
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}