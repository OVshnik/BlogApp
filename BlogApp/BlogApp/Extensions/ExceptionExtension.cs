﻿using System.Security.Cryptography;
using System.Text;

namespace BlogApp.Extensions
{
	public static class ExceptionExtension
	{
		private const string ErrorCodeKey = "errorCode";
		public static Exception AddErrorCode(this Exception exception)
		{
			using var sha1 = SHA1.Create();
			var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(exception.Message));
			var errorCode = string.Concat(hash[..5].Select(b => b.ToString("x")));
			exception.Data[ErrorCodeKey] = errorCode;
			return exception;
		}
		public static string GetErrorCode(this Exception exception)
		{
			var errorCode = (string?)exception.Data[ErrorCodeKey];
			if (errorCode != null)
				return errorCode;
			return string.Empty;
		}
	}
}
