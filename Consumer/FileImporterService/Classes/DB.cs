using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileImporterService.Classes
{
	internal class DB
	{
		public string ConnectionString { get; private set; }
		public DB(string connectionString)
		{
			ConnectionString = connectionString;
		}
	}
}