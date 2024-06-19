using AceInternship.MiniKPayWebApi.Models;
using Dapper;
using System.Data;

namespace AceInternship.MiniKPayWebApi.Features.TransactionHistory
{
    public class TransactionHistoryDA
    {
        private readonly IDbConnection _db;

        public TransactionHistoryDA(IDbConnection db)
        {
            _db = db;
        }

        public bool IsExistCustomerCode(string customercode)
        {
           
            string query = "select * from Tbl_Customer with (nolock) where CustomerCode=@CustomerCode";
            var result = _db.Query<CustomerModel>(query, new { CustomerCode = customercode }).FirstOrDefault();
            return result is not null;
        }
        public List<CustomerTransactionHistoryModel> TransactionHistoryByCustomerCode(string customercode)
        {

            var lst = new List<CustomerTransactionHistoryModel>();
            string query = @"SELECT cth.* FROM [dbo].[Tbl_CustomerTransactionHistory] cth
inner join Tbl_Customer c on cth.FromMobileNo =c.MobileNo
WHERE CustomerCode =@CustomerCode";
            lst = _db.Query<CustomerTransactionHistoryModel>(query, new { CustomerCode = customercode }).ToList();
            return lst;
        }
    }
}
