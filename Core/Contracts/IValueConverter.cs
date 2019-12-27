namespace Contracts
{
	public interface IValueConverter
	{
		object Convert(object input);
		string ReverseConvert(object input);
	}
}