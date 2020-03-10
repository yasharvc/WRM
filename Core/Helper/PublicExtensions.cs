using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Helper
{
	public static class PublicExtensions
	{
		public static string GetFirstLine(this string content, string delimeter)
		{
			return content.Split(new string[] { delimeter }, StringSplitOptions.RemoveEmptyEntries).First();
		}
		public static string GetLastLine(this string content, string delimeter)
		{
			return content.Split(new string[]{delimeter},  StringSplitOptions.RemoveEmptyEntries).Last();
		}
		public static string SkipFirstLine(this string content, string delimeter)
		{
			return content.Substring(content.IndexOf(delimeter) + delimeter.Length);
		}
		public static string SkipHeaderFooter(this string content, string delimeter)
		{
			content = content.Trim();
			return content.Substring(0, content.LastIndexOf(delimeter)).Trim().SkipFirstLine(delimeter);
		}

		public static string CalcMD5HashByPath(this string path)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(path))
				{
					return BitConverter.ToString(md5.ComputeHash(stream)).ToLowerInvariant();
				}
			}
		}

		public static Stream ToStream(this string s)
		{
			return new MemoryStream(Encoding.UTF8.GetBytes(s));
		}
	}
}