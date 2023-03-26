using System;
namespace Domain.Models
{
	public class ErrorDetail
	{
		public int ErrorCode { get; }
		public string Message { get; }
		public ErrorDetail(int errorCode, string message)
		{
			ErrorCode = errorCode;
			Message = message ?? "";
		}
	}
}

