﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Contracts
{
	internal static class Extensions
	{
		public static string GetSimpleClassName(this string p)
		{
			int last = p.LastIndexOf("<") - 1;
			return p.Substring(p.LastIndexOf(".", last > 0 ? last : p.Length) + 1);
		}

		public static string ToString(this IEnumerable<byte> bytes, Encoding encoding = null)
		{
			encoding = encoding ?? Encoding.UTF8;
			return encoding.GetString(bytes.Select(i => i).ToArray());
		}

		public static bool IsPrimitiveType(this object o)
		{
			Type returnType = o is Type ? (Type)o : o.GetType();
			return returnType.IsPrimitive || returnType == typeof(Decimal) || returnType == typeof(String) || returnType == typeof(byte[]);
		}
		public static bool IsParameterList(this ParameterInfo o)
		{
			return o.ParameterType.IsGenericType && o.ParameterType.FullName.StartsWith("System.Collections.Generic.List");
		}

		public static bool IsNullable(this Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}
	}
}
