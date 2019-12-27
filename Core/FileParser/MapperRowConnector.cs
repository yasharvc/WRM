using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileParser
{
	public sealed class MapperRowConnector
	{
		public ColumnMapper Mapper { get; set; }

		public Dictionary<string, string> Connect(IEnumerable<string> Slices)
		{
			Dictionary<string, string> res = new Dictionary<string, string>();
			var sortedMapper = Mapper.GetSortedByIndex();
			foreach (var item in sortedMapper)
			{
				res[item.Value] = Slices.ElementAt(item.Key);
			}
			return res;
		}
	}
}