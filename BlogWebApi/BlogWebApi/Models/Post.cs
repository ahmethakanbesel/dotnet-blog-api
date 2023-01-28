using System.ComponentModel.DataAnnotations;
namespace BlogWebApi.Models
{
    using System;

    public class Post
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }
        
        #nullable enable
        public string? Slug { get; set; }
        
        #nullable enable
        public string? Category { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }
        
        #nullable enable
        public string? Author { get; set; }
        
        #nullable enable
        public string? CoverImage { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}