using Contracts;
using Contracts.Attribute;
using Helper.ColumnConverter;
using System;
using System.Linq.Expressions;

namespace FileWriter
{
	public class ElixirDigitalBankingModel : PersistableModel<ElixirDigitalBankingModel>
	{
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 0)]
		public string RecordType { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 1)]
		public string OperationFlag { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 2)]
		public string Request_Status { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 3)]
		public string CIFID { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 4, typeof(YesNoToBooleanConverter))]
		public bool SubforCC { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 5)]
		public string SubForAccount { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 6)]
		public string SubForRemittance { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 7)]
		public string SubForDeposit { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 8)]
		public string DeliveryMode { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 9)]
		public string FaxNum { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 10)]
		public string PrimaryEmail { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 11)]
		public string SecondaryEmail { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 12)]
		public string RCreId { get; set; }
		[PropertyToFileMapper(DownloadFileTypes.DigitalBanking, 13)]
		public string RCreTime { get; set; }

		public override Expression<Func<ElixirDigitalBankingModel, bool>> GetFindPredicate()
		{
			return m => m.CIFID == CIFID;
		}

		public override void SetDefaultValues()
		{
			throw new NotImplementedException();
		}

		public override void UpdateValues(ElixirDigitalBankingModel data)
		{
			throw new NotImplementedException();
		}
	}
}