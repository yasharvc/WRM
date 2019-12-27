namespace FileWriter
{
	public class ElixirFinacleWriter : FileWriter<ElixirFinacleModel>
	{
		public ElixirFinacleWriter() : base("RecordType|OperationFlg|Request_Status|CIFID|SubforCC|SubForAccount|SubForRemittance|SubForDeposit|SubForInvestments|SubForFutureService1|SubForFutureService2|SubForFutureService3|DeliveryMode|FaxNum|PrimaryEmail|SecondaryEmail|RCreId|RCreTime")
		{ }
	}
}