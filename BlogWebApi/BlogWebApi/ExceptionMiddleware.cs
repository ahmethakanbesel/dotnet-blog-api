using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace BlogWebApi
{
	using System;
	using System.Threading.Tasks;

	public class ExceptionMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMiddleware> logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			this.logger = logger;
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (ArgumentNullException ex)
			{
				logger.LogError($"Something went wrong: {ex}");
				await HandleExceptionAsync(httpContext, ex);
			}
			catch (Exception ex)
			{
				logger.LogError($"Something went wrong: {ex}");
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var message = "Internal Server Error from the custom middleware.";
			var error = new ErrorDetails()
			{
				StatusCode = context.Response.StatusCode,
				Message = message + Environment.NewLine + exception.Message.ToString()
			};
			await context.Response.WriteAsync(JsonSerializer.Serialize(error));
		}
	}

	public class ErrorDetails
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
	}
}