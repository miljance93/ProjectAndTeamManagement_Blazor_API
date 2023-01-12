using Domain.IdentityAuth;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Body { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
