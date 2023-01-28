using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApi.Models;
using BlogWebApi.DataAccess;

namespace BlogWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly IPostRepository repository;

		public PostsController(IPostRepository repository)
		{
			this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		// GET: api/posts
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
		{
			var posts = await repository.GetAllAsync();
			return Ok(posts);
		}

		// GET: api/posts/5
		[HttpGet("{id}")]
		public async Task<ActionResult> GetPost(Guid id)
		{
			var post = await repository.GetAsync(id);
			return post != null ? Ok(post) : NotFound();
		}

		// POST: api/posts
		[HttpPost]
		public async Task<ActionResult> PostPost(Post post)
		{
			bool result = await repository.CreateAsync(post);
			return result ? Ok(post) : BadRequest();
		}

		// PUT: api/posts
		[HttpPut]
		public async Task<ActionResult> PutPost([FromBody] Post post)
		{
			bool result = await repository.UpdateAsync(post);
			return result ? Ok(post) : BadRequest();
		}

		// DELETE: api/posts/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeletePost(Guid id)
		{
			bool result = await repository.DeleteAsync(id);
			return result ? Ok() : NotFound();
		}
	}
}
