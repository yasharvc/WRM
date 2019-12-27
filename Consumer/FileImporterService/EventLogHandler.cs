using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileImporterService
{
	internal class EventLogHandler
	{
		public const string EventLogName = "DTECH File Reader";
		public const string LogName = "Service log";
		public EventLogHandler()
		{
			if (!EventLog.SourceExists(EventLogName))
				EventLog.CreateEventSource(EventLogName, LogName);
		}

		public void InitEventLogComponent(EventLog e)
		{
			e.Source = EventLogName;
			e.Log = LogName;
		}
	}
}