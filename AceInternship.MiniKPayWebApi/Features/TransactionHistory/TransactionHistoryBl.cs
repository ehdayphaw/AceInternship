namespace AceInternship.MiniKPayWebApi.Features.TransactionHistory
{
    public class TransactionHistoryBl
    {
        private readonly TransactionHistoryDA _transactionHistoryDA;

        public TransactionHistoryBl(TransactionHistoryDA transactionHistoryDA)
        {
            _transactionHistoryDA = transactionHistoryDA;
        }

        public TransactionHistoryResponseModel  TransactionHistory(TransactionHistoryRequestModel requestModel)
        {
            TransactionHistoryResponseModel model = new TransactionHistoryResponseModel();
            //Exist CustomerCode
            bool isExist=_transactionHistoryDA.IsExistCustomerCode(requestModel.CustomerCode);
            if (!isExist)
            {
                model.IsSuccess = false;
                model.msg = "Customer Doesn't exist.";
                return model;
            }
            //Transaction History By Customer Code
            var lst=_transactionHistoryDA.TransactionHistoryByCustomerCode(requestModel.CustomerCode);  
            model.IsSuccess = true;
            model.msg = "Success";
            return model;
        }
    }
}
