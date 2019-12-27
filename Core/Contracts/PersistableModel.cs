using Contracts.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Contracts
{
	public abstract class PersistableModel<T>
	{
		private OperationFlag OperationFlag = OperationFlag.Insert;
		public OperationFlag GetOperation()
		{
			return OperationFlag;
		}
		public void SetOperation(OperationFlag flag)
		{
			OperationFlag = flag;
		}
		public PersistableModel()
		{
			OperationFlag = Contracts.OperationFlag.Insert;
		}
		public void SetValue(string propName, object value)
		{
			GetType().GetProperty(propName).SetValue(this, value);
		}
		public void SetValue(int indexInFile, DownloadFileTypes fileType, object value)
		{
			PropertyToFileMapperAttribute prop = GetPropertyByIndexInFileAndType(fileType, indexInFile);
			SetValue(prop.PropertyName, value);
		}

		private PropertyToFileMapperAttribute GetPropertyByIndexInFileAndType(DownloadFileTypes fileType,int index)
		{
			return GetType().GetProperties().Where(m => m.GetCustomAttributes<PropertyToFileMapperAttribute>().Count() > 0).Cast<PropertyToFileMapperAttribute>()
							.Single(m => m.SupportedType == fileType && m.Index == index);
		}

		public Y GetValue<Y>(string propName)
		{
			return (Y)GetValueAsObject(propName);
		}

		public Y GetValue<Y>(DownloadFileTypes fileTypes, int indexInFile)
		{
			return GetValue<Y>(GetPropertyByIndexInFileAndType(fileTypes, indexInFile).PropertyName);
		}

		public object GetValueAsObject(string name)
		{
			return GetType().GetProperty(name).GetValue(this);
		}

		public string Flattern(DownloadFileTypes fileType)
		{
			Dictionary<int, string> res = new Dictionary<int, string>();
			var props = GetType().GetProperties().Where(m => m.GetCustomAttributes<PropertyToFileMapperAttribute>().Count() > 0);

			foreach (var prop in props)
			{
				var property = prop.GetCustomAttributes<PropertyToFileMapperAttribute>().FirstOrDefault(m=>m.SupportedType == fileType);
				if (property == null)
					continue;
				object value = null;
				if (property.IsConverterExists)
					value = property.ValueConvertor.ReverseConvert(GetValueAsObject(property.PropertyName));
				else
					value = GetValueAsObject(property.PropertyName);
				res[property.Index] = value.ToString();
			}
			AdditionalPropertySetting(res);
			return string.Join(FileContentConsts.ColumnDelimeter, res.OrderBy(m => m.Key).Select(m => m.Value));
		}

		protected virtual void AdditionalPropertySetting(Dictionary<int, string> res)
		{
			
		}

		public IEnumerable<FileToPropertyMapperAttribute> GetMappingProperties(UploadFileTypes fileType)
		{
			var res = new List<FileToPropertyMapperAttribute>();
			var props = GetType().GetProperties().Where(m => m.GetCustomAttributes<FileToPropertyMapperAttribute>().Count() > 0);
			foreach (var item in props)
			{
				var attr = item.GetCustomAttributes<FileToPropertyMapperAttribute>().FirstOrDefault(m => m.SupportedType == fileType);
				if (attr != null)
				{
					attr.PropertyType = item.PropertyType;
					res.Add(attr);
				}
			}
			return res;
		}
		public abstract Expression<Func<T, bool>> GetFindPredicate();
		public abstract void SetDefaultValues();
		public abstract void UpdateValues(T data);
	}
}