using AccountingAssistantBackend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AccountingAssistantBackend.Controllers
{
    /// <summary>
    /// Accounting Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingController : ControllerBase
    {

        private readonly IAccountingManager _accountingManager;

        /// <summary>
        /// Initializes a new instance of <see cref="AccountingController"/>
        /// </summary>
        /// <param name="accountingManager">The accounting manager</param>
        public AccountingController(IAccountingManager accountingManager)
        {
            _accountingManager = accountingManager;
        }

        /// <summary>
        /// Get response from OpenAI for accounting query
        /// </summary>
        /// <param name="query">The accounting query</param>
        /// <returns></returns>
        [HttpGet, Route("query/{query}")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetResponseForAccountingQuery(string query)
        {
            var result = await _accountingManager.GetResponseForAccountingQuery(query);
            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }       
}
