using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using BlogWebApi.Dto;

namespace BlogWebApi
{
	public static class ODataExtensions
	{
		public static IEdmModel GetEdmModel()
		{
			var builder = new ODataConventionModelBuilder();
			builder
				.EntitySet<PostDto>("PostInfo")
				.EntityType
				.HasKey(s => s.Id);

			return builder.GetEdmModel();
		}
	}
}