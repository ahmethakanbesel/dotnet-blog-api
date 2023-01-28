using Microsoft.Data.Sqlite;
using BlogWebApi.Models;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace BlogWebApi.DataAccess
{
    public class PostRepository : IPostRepository
    {
        private readonly string connectionString;
        public PostRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<bool> CreateAsync(Post post)
        {
            int affectedRow = 0;
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string query = @"INSERT INTO Posts (Id, Title, Author, Category, Content, CoverImage, Slug, CreateDate, UpdatedDate)
                VALUES (@Id, @Title, @Author, @Category, @Content, @CoverImage, @Slug, @CreateDate, @UpdatedDate)";
                SqliteCommand cmd = new SqliteCommand(query, connection);
                var id = Guid.NewGuid();
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Title", post.Title);
                cmd.Parameters.AddWithValue("@Author", post.Author);
                cmd.Parameters.AddWithValue("@Category", post.Category);
                cmd.Parameters.AddWithValue("@Content", post.Content);
                cmd.Parameters.AddWithValue("@CoverImage", post.CoverImage);
                cmd.Parameters.AddWithValue("@Slug", post.Slug);
                cmd.Parameters.AddWithValue("@CreateDate", post.CreateDate);
                cmd.Parameters.AddWithValue("@UpdatedDate", post.UpdatedDate);
                await connection.OpenAsync();
                affectedRow = await cmd.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
            return affectedRow > 0;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            List<Post> posts = new List<Post>();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string query = "SELECT * FROM Posts";
                SqliteCommand cmd = new SqliteCommand(query, connection);
                await connection.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    Post post = new Post
                    {
                        Id = reader.GetGuid("Id"),
                        Title = reader.GetString("Title"),
                        Slug = reader.GetString("Slug"),
                        Category = reader.GetString("Category"),
                        Author = reader.GetString("Author"),
                        Content = reader.GetString("Content"),
                        CoverImage = reader.GetString("CoverImage"),
                        CreateDate = reader.GetDateTime("CreateDate"),
                        UpdatedDate = reader.GetDateTime("UpdatedDate"),
                    };
                    posts.Add(post);
                }
                await connection.CloseAsync();
            }
            return posts;
        }

        public async Task<Post> GetAsync(Guid id)
        {
            Post post = null;

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string query = "SELECT * FROM Posts WHERE Id = @Id";
                SqliteCommand cmd = new SqliteCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                var reader = await cmd.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    reader.Read();
                    post = new Post
                    {
                        Id = reader.GetGuid("Id"),
                        Title = reader.GetString("Title"),
                        Slug = reader.GetString("Slug"),
                        Category = reader.GetString("Category"),
                        Author = reader.GetString("Author"),
                        Content = reader.GetString("Content"),
                        CoverImage = reader.GetString("CoverImage"),
                        CreateDate = reader.GetDateTime("CreateDate"),
                        UpdatedDate = reader.GetDateTime("UpdatedDate"),
                    };

                    await connection.CloseAsync();
                }
            }

            return post;
        }

        public async Task<bool> UpdateAsync(Post post)
        {
            int affectedRow = 0;

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string query = @"UPDATE Posts SET 
                Title = @Title, 
                Author = @Author, 
                Category = @Category, 
                Content = @Content, 
                CoverImage = @CoverImage, 
                Slug = @Slug,
                CreateDate = @CreateDate,
                UpdatedDate = @UpdatedDate
                WHERE Id = @Id";

                SqliteCommand cmd = new SqliteCommand(query, connection);

                cmd.Parameters.AddWithValue("@Title", post.Title);
                cmd.Parameters.AddWithValue("@Author", post.Author);
                cmd.Parameters.AddWithValue("@Category", post.Category);
                cmd.Parameters.AddWithValue("@Content", post.Content);
                cmd.Parameters.AddWithValue("@CoverImage", post.CoverImage);
                cmd.Parameters.AddWithValue("@Slug", post.Slug);
                cmd.Parameters.AddWithValue("@CreateDate", post.CreateDate);
                cmd.Parameters.AddWithValue("@UpdatedDate", post.UpdatedDate);
                cmd.Parameters.AddWithValue("@Id", post.Id);

                await connection.OpenAsync();
                affectedRow = await cmd.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return affectedRow > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            int affectedRow = 0;
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                string query = "DELETE FROM Posts WHERE Id = @Id";
                SqliteCommand cmd = new SqliteCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                affectedRow = await cmd.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
            return affectedRow > 0;
        }
    }
}
