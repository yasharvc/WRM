using System.Collections.Generic;
using System.Linq;

namespace FileParser
{
	public class ColumnMapper
	{
		Dictionary<int, string> Mapping { get; set; }

		public ColumnMapper()
		{
			Mapping = new Dictionary<int, string>();
		}

		public ColumnMapper(params string[] columnNames)
		{
			var i = 0;
			foreach (var item in columnNames)
				this[i++] = item;
		}

		public void AddToEnd(params string[] data)
		{
			var lastIndex = Mapping.Count > 0 ? Mapping.Max(m => m.Key) : 0;
			foreach (var item in data)
				Mapping[lastIndex++] = item;
		}

		public string this[int index]
		{
			get
			{
				return Mapping[index];
			}
			set
			{
				Mapping[index] = value;
			}
		}

		public int this[string name]
		{
			get
			{
				try
				{
					return Mapping.Single(m => m.Value == name).Key;
				}
				catch
				{
					throw new KeyNotFoundException(name);
				}
			}
		}

		public Dictionary<int, string> GetSortedByIndex()
		{
			return Mapping.OrderBy(m => m.Key).ToDictionary(t => t.Key, t => t.Value);
		}
	}
}