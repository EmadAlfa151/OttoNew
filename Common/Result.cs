using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OttoNew.Common
{
	public class Result<T>
	{
		public bool IsSuccess { get; init; }
		public T? Data { get; init; }
		public string? ErrorMessage { get; init; }
		public Exception? Exception { get; init; }

		private Result() { }

		public static Result<T> Success(T data)
		{
			return new Result<T>
			{
				IsSuccess = true,
				Data = data
			};
		}

		public static Result<T> Failure(string errorMessage)
		{
			return new Result<T>
			{
				IsSuccess = false,
				ErrorMessage = errorMessage
			};
		}

		public static Result<T> Failure(Exception exception)
		{
			return new Result<T>
			{
				IsSuccess = false,
				ErrorMessage = exception.Message,
				Exception = exception
			};
		}
	}

	public class Result
	{
		public bool IsSuccess { get; init; }
		public string? ErrorMessage { get; init; }
		public Exception? Exception { get; init; }

		private Result() { }

		public static Result Success()
		{
			return new Result
			{
				IsSuccess = true
			};
		}

		public static Result Failure(string errorMessage)
		{
			return new Result
			{
				IsSuccess = false,
				ErrorMessage = errorMessage
			};
		}

		public static Result Failure(Exception exception)
		{
			return new Result
			{
				IsSuccess = false,
				ErrorMessage = exception.Message,
				Exception = exception
			};
		}
	}
}