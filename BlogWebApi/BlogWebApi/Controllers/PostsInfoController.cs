using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApi.Dto;

namespace BlogWebApi.Controllers
{
	[Route("odata/[controller]")]
	[ApiExplorerSettings(IgnoreApi = false)]
	public class PostsInfoController : ODataController
	{
		private readonly PostDbContext postDbContext;

		public PostsInfoController(PostDbContext postDbContext)
		{
			this.postDbContext = postDbContext;
		}

		//https://docs.microsoft.com/en-us/dynamics-nav/using-filter-expressions-in-odata-uris
		//https://www.odata.org/getting-started/basic-tutorial/

		//http://localhost:5000/odata/$metadata

		//http://localhost:5000/odata/PostsInfo?$filter=rate%20eq%205
		//http://localhost:5000/odata/PostsInfo?$orderby=rate%20desc
		[HttpGet]
		[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(IEnumerable<PostDto>))]
		[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
		public async Task<IQueryable<PostDto>> Get()
		{
			await Task.Yield();

			return postDbContext.Posts.Select(s => new PostDto()
			{
				Id = s.Id,
				Title = s.Title,
				Category = s.Category,
				Content = s.Content,
				Author = s.Author,
				CoverImage = s.CoverImage
			}).AsQueryable();
		}
	}
}