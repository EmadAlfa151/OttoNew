using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OttoNew.ApiClients.ApiModels.Response
{
	public class OttoUpdateQuantityResponse
	{
		public UpdateQuantityResult[] Results { get; set; }
		public UpdateQuantityError[] Errors { get; set; }
	}

	public class UpdateQuantityResult
	{
		public string Sku { get; set; }
		public int Quantity { get; set; }
	}

	public class UpdateQuantityError
	{
		public string Path { get; set; }
		public string Title { get; set; }
		public int Code { get; set; }
		public string Detail { get; set; }
		public string Logref { get; set; }
	}
}
