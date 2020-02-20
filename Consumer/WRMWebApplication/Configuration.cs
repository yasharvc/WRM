using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WRMWebApplication
{
	internal class Configuration
	{
		public int ConfigurationFileCheckerInterval { get; set; }
		public int TimerInterval { get; set; }
		public string UploadPath { get; set; }
		public string LogPath { get; set; }
	}
}