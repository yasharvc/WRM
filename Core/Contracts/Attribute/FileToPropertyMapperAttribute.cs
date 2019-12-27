using System;
using System.Runtime.CompilerServices;

namespace Contracts.Attribute
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple= true)]
	public sealed class FileToPropertyMapperAttribute : System.Attribute
	{
		public UploadFileTypes SupportedType { get; set; }
		public string PropertyName { get; set; }
		public string ColumnNameInFile { get; set; }
		public Type PropertyType { get; set; }
		public IValueConverter ValueConvertor { get; private set; }
		public int Index { get; private set; }
		public bool IsConverterExists
		{
			get
			{
				return ValueConvertor != null;
			}
		}

		public FileToPropertyMapperAttribute(UploadFileTypes type, int indexInFile, Type valueConverterType = null, [CallerMemberName] string propertyName = "")
		{
			PropertyName = propertyName;
			ColumnNameInFile = "";
			SupportedType = type;
			Index = indexInFile;
			if (IsConverterValid(valueConverterType))
				ValueConvertor = Activator.CreateInstance(valueConverterType) as IValueConverter;
		}

		//public PropertyToFileMapperAttribute(FileTypes type, string columnNameInFile, int indexInFile = -1, Type valueConverterType = null, [CallerMemberName] string propertyName = "")
		//{
		//	PropertyName = propertyName;
		//	ColumnNameInFile = columnNameInFile;
		//	SupportedType = type;
		//	Index = indexInFile;
		//	if (IsConverterValid(valueConverterType))
		//		ValueConvertor = Activator.CreateInstance(valueConverterType) as ValueConverter;
		//}

		public FileToPropertyMapperAttribute(UploadFileTypes type, string columnNameInFile, Type valueConverterType = null, [CallerMemberName] string propertyName = "")
		{
			PropertyName = propertyName;
			ColumnNameInFile = columnNameInFile;
			SupportedType = type;
			Index = -1;
			if (IsConverterValid(valueConverterType))
				ValueConvertor = Activator.CreateInstance(valueConverterType) as IValueConverter;
		}

		private static bool IsConverterValid(Type valueConvertorType)
		{
			return valueConvertorType != null && (valueConvertorType.GetInterface(typeof(IValueConverter).Name) != null);
		}
	}
}