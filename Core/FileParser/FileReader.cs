using Contracts;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileParser
{
	public abstract class FileReader
	{
		public ColumnMapper Mapper { get; protected set; }
		MapperRowConnector _connector = new MapperRowConnector();
		TypeConverter TypeConverter = new TypeConverter();
		protected MapperRowConnector Connector
		{
			get
			{
				_connector.Mapper = Mapper;
				return _connector;
			}
		}
		public int ExceptedColumnCount { get; protected set; }
		protected UploadFileTypes FileType { get; set; }
		public FileReader(int exceptedColumnCount,UploadFileTypes type)
		{
			ExceptedColumnCount = exceptedColumnCount;
			Mapper = new ColumnMapper();
			FileType = type;
		}

		public abstract bool IsEligableForFile(string fileName, string firstLineOfFile = null);
		public abstract IEnumerable<T> Parse<T>(string fileContent) where T : PersistableModel<T>, new();
		public virtual void ParseColumnNames(string firstLine) { }

		protected T ToT<T>(Dictionary<string, string> nameValue) where T : PersistableModel<T>, new()
		{
			T obj = new T();
			var properties = obj.GetMappingProperties(FileType);
			foreach (var property in properties)
			{
				try
				{
					object value = null;
					if (property.IsConverterExists)
						value = property.ValueConvertor.Convert(nameValue[property.ColumnNameInFile]);
					else if (property.Index >= 0)
						value = TypeConverter.Convert(nameValue[property.PropertyName], property.PropertyType);
					else
						value = TypeConverter.Convert(nameValue[property.ColumnNameInFile], property.PropertyType);
					obj.SetValue(property.PropertyName, value);
				}
				catch(System.Exception e)
				{
					Debug.Write(e.Message);
				}
			}
			return obj;
		}
	}
}