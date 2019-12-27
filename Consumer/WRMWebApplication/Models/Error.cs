namespace WRMWebApplication.Models
{
	public class Error
	{
		public Error(int v1, string v2)
		{
			Code = v1;
			Meaning = v2;
		}

		public int Code { get; set; }
		public string Meaning { get; set; }
	}
}