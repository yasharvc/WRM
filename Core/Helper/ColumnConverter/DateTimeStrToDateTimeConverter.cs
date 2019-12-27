using Contracts;
using System;

namespace Helper.ColumnConverter
{
	public class DateTimeStrToDateTimeConverter : IValueConverter
	{
		public object Convert(object input)
		{
			var str = input.ToString();
			var month = int.Parse(str.Substring(0, 2));
			var day = int.Parse(str.Substring(3, 2));
			var year = int.Parse(str.Substring(6, 4));

			var hour = int.Parse(str.Substring(11, 2));
			var min = int.Parse(str.Substring(14, 2));
			var sec = int.Parse(str.Substring(17, 2));

			return new DateTime(year, month, day, hour, min, sec);
		}

		public string ReverseConvert(object input)
		{
			var date = (DateTime)input;
			return string.Format("{0:00}-{1:00}-{2} {3:00}:{4:00}:{5:00}", date.Month
				, date.Day, date.Year, date.Hour, date.Minute, date.Second);
		}
	}
}
