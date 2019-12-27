using Contracts;
using System;

namespace Helper.ColumnConverter
{
	public class YesNoToBooleanConverter : IValueConverter
	{
		public object Convert(object input)
		{
			var str = input.ToString();
			if (str[0] == 'Y' || str[0] == 'y') return true;
			else
				return false;
		}

		public string ReverseConvert(object input)
		{
			return (bool)input ? "Y" : "N";
		}
	}
}