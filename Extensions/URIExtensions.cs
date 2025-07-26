using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OttoNew.Extensions
{
	public static class URIExtensions
	{
		public static Uri AddQueryParam(this Uri uri, string key, string value)
		{
			var uriBuilder = new UriBuilder(uri);

			// Parse the existing query string into a NameValueCollection
			var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);

			// Add or update the query parameter
			queryParams[key] = value;

			// Rebuild the query string
			uriBuilder.Query = queryParams.ToString();

			// Return the modified URI
			return uriBuilder.Uri;
		}
	}
}
