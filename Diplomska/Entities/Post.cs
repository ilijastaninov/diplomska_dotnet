using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplomska.Entities
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }
        [Required(ErrorMessage = "Text field is required.")]
        [MaxLength(500,ErrorMessage = "The maximum length is 500 characters long.")]
        public string Text { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        
        /*public ICollection<Comment> Comments { get; set; }
            = new List<Comment>();*/
    }
}