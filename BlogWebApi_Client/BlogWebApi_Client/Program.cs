using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogWebApi_Client
{
    internal class Program
    {
		private static readonly Client PostClient = new Client("http://localhost:5000/", new HttpClient());

		static async Task Main(string[] args)
        {
			await GetAllPosts();
			await AddNewPost();
			await UpdatePost();
			await GetAllPosts();
			//await DeletePost();
			//await GetAllPosts();
		}

		private static async Task DeletePost()
		{
			var p = (await PostClient.PostsAllAsync()).First();

			await PostClient.PostsDELETEAsync(p.Id);

			Console.WriteLine("**** Post Deleted ****");
			Console.WriteLine($"Id: {p.Id}	Title: {p.Title}	Author: {p.Author}	Category: {p.Category}");

			await GetAllPosts();
		}

		private static async Task GetAllPosts()
		{
			var posts = await PostClient.PostsAllAsync();
			Console.WriteLine("**** Posts Listed ****");
			foreach (var p in posts)
			{
				Console.WriteLine($"Id: {p.Id}	Title: {p.Title}	Category: {p.Category}");
			}
		}

		private static async Task UpdatePost()
		{
			var p = (await PostClient.PostsAllAsync()).First();
			p.Title += " Updated";
			await PostClient.PostsPUTAsync(p);

			Console.WriteLine("**** Post Updated ****");
			Console.WriteLine($"Id: {p.Id}	Title: {p.Title}	Category: {p.Category}");

			await GetAllPosts();
		}

		private static async Task AddNewPost()
		{
			var now = DateTime.Now;
			Random rnd = new Random();
			var i = rnd.Next(100);
			var p = await PostClient.PostsPOSTAsync(new Post()
			{
				Id = Guid.NewGuid(),
				Title = $"Post {i}",
				Category = $"Category {i}",
				Author = $"Author {i}",
				Content = $"Content {i}",
				CoverImage = "",
				Slug = $"post-{i}",
				CreateDate = now,
				UpdatedDate = now,
			});
			Console.WriteLine("**** Post Added ****");
			Console.WriteLine($"Id: {p.Id}	Title: {p.Title}  Author: {p.Author}");
			await GetAllPosts();
		}
	}
}
