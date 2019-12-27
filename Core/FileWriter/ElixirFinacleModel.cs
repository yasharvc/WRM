using Contracts;
using Contracts.Attribute;
using Helper.ColumnConverter;
using System;
using System.Linq.Expressions;

namespace FileWriter
{
	public class ElixirFinacleModel : PersistableModel<ElixirFinacleModel>
	{
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 0)]
		public string RecordType { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 1)]
		public string OperationFlag { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 2)]
		public string Request_Status { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 3)]
		public string CIFID { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 4, typeof(YesNoToBooleanConverter))]
		public bool SubforCC { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 5, typeof(YesNoToBooleanConverter))]
		public bool SubForAccount { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 6, typeof(YesNoToBooleanConverter))]
		public bool SubForRemittance { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 7, typeof(YesNoToBooleanConverter))]
		public bool SubForDeposit { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 8, typeof(YesNoToBooleanConverter))]
		public bool SubForInvestments { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 9, typeof(YesNoToBooleanConverter))]
		public bool SubForFutureService1 { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 10, typeof(YesNoToBooleanConverter))]
		public bool SubForFutureService2 { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 11, typeof(YesNoToBooleanConverter))]
		public bool SubForFutureService3 { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 12)]
		public string DeliveryMode { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 13)]
		public string FaxNum { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 14)]
		public string PrimaryEmail { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 15)]
		public string SecondaryEmail { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 16)]
		public string RCreId { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.FinacleBanking, 17)]
		public string RCreTime { get; set; }

		public override Expression<Func<ElixirFinacleModel, bool>> GetFindPredicate()
		{
			return m => m.CIFID == CIFID;
		}

		public override void SetDefaultValues()
		{
			throw new NotImplementedException();
		}

		public override void UpdateValues(ElixirFinacleModel data)
		{
			throw new NotImplementedException();
		}
	}
}