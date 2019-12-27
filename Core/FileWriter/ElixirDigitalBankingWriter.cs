namespace FileWriter
{
    public class ElixirDigitalBankingWriter : FileWriter<ElixirDigitalBankingModel>
    {
        public ElixirDigitalBankingWriter() : base("RecordType|OperationFlg|Request_Status|CIFID|SubforCC|SubForAccount|SubForRemittance|SubForDeposit|DeliveryMode|FaxNum|PrimaryEmail|SecondaryEmail|RCreId|RCreTime|")
        { }
    }
}