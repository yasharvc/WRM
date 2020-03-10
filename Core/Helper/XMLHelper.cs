using System.Xml;

namespace Helper
{
	public class XMLHelper
	{
		public string XML { get; set; }
		XmlElement current = null;
		XmlDocument xd = new XmlDocument();
		public XMLHelper(string xml)
		{
			XML = xml;
		}
		public XMLHelper() : this("") { }

		public void GotoPath(string path)
		{
			xd.Load(XML.ToStream());
			var parts = path.Split('/');
			current = GotoElementTemporary(path);
		}

		private XmlElement GotoElementTemporary(string path)
		{
			var parts = path.Split('/');
			XmlElement element = current;
			foreach (var part in parts)
			{
				if (element == null)
					element = xd.GetElementsByTagName(part).Item(0) as XmlElement;
				else
					element = element.GetElementsByTagName(part).Item(0) as XmlElement;
			}
			return element;
		}

		public string GetInnerText()
		{
			return current.InnerText;
		}

		public string GetInnerTextOf(string path)
		{
			return GotoElementTemporary(path).InnerText;
		}
	}
}