using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AceInternship.MiniKPayWebApi.Features.TransactionHistory
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly TransactionHistoryBl _transactionHistoryBl;

        public TransactionHistoryController(TransactionHistoryBl transactionHistoryBl)
        {
            _transactionHistoryBl = transactionHistoryBl;
        }

        [HttpPost]
        public IActionResult TransactionHistory (TransactionHistoryRequestModel requestModel)
        {
            try  
            {
                if (string.IsNullOrEmpty(requestModel.CustomerCode))
                {
                    return BadRequest("Invalid Customer Code.");
                }
               var model= _transactionHistoryBl.TransactionHistory(requestModel);

                return Ok(model);
            }
            catch (Exception ex) 
            {
                    return StatusCode(500,ex.ToString());
            }
        }
    }
}
      