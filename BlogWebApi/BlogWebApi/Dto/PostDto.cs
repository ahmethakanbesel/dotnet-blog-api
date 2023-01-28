namespace BlogWebApi.Dto
{
	using System;
    using System.ComponentModel.DataAnnotations;

    public class PostDto
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
	}
}