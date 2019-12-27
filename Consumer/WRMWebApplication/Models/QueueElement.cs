using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WRMWebApplication.Models
{
	public class QueueElement
	{
		public string Path { get; set; }
		public string Hash { get; set; }
		public DateTime CreationDateTime { get; set; } = DateTime.Now;

	}
}