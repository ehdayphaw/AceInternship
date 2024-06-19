namespace AceInternship.MiniKPayWebApi.Features.TransactionHistory
{
    public class TransactionHistoryRequestModel
    {
        public string? CustomerCode { get; set; }
    }
    public class TransactionHistoryResponseModel
    {
        public bool IsSuccess { get; set; }
        public string msg { get; set; }

        public List<TransactionHistoryResponseModel> Data { get; set; }
    }
     
}
