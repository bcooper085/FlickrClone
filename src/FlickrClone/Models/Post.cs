using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlickrClone.Models
{
    [Table("Posts")]
    public class Post
    {
        public override bool Equals(System.Object otherPost)
        {
            if (!(otherPost is Post))
            {
                return false;
            }
            else
            {
                Post newPost = (Post)otherPost;
                return this.PostId.Equals(newPost.PostId);
            }
        }

        public override int GetHashCode()
        {
            return this.PostId.GetHashCode();
        }
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public virtual User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
