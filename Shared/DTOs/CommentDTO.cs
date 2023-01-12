using Domain;
using Domain.IdentityAuth;

namespace Shared.DTOs
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Project Project { get; set; }
        public ApplicationUser Author { get; set; }
        public string Body { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }
}
