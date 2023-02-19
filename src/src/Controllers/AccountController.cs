using Microsoft.AspNetCore.Mvc;
using src.Filters;
using src.Models.Account;
using src.Repositories.Account;
using src.Repositories.Transfers;
using src.Requests;

namespace src.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly ITransferRepository transferRepository;

        public AccountController(IAccountRepository repository, ITransferRepository transferRepository)
        {
            _repository = repository;
            this.transferRepository = transferRepository;
        }
        [Cached]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var result = await _repository.FindAll();

            return Ok(result.ToList());
        }
        [Cached(timeToLive:120)] //seconds
        [ValidateRequest]
        [HttpGet("GetById")]
        public async Task<ActionResult<Account>> GetAccountById([FromQuery]int id)
        {
            var result = await _repository.Find(id);
            
            return Ok(result);
        }
        [Cached]
        [ValidateRequest]
        [HttpGet("GetBy")]
        public async Task<ActionResult<IEnumerable<Account>>> FindAccounts([FromQuery] FindAccountRequest request)
        {
            bool orderBy = request.orderBy == "ASC" ? true : false;

            var result = await _repository.Find(null, request.size, request.offset, orderBy);

            return Ok(result);
        }
        [Cached(dataChanged: true)] //cache evict
        [ValidateRequest]
        [HttpPost]
        public async Task<ActionResult<string>> InsertAccount([FromBody] UpsertAccountRequest request)
        {
            var result = await _repository.Insert(new Account() { username = request.username, password = request.password});

            if (result < 1)
                throw new Exception("For some reason your update request was not compleated.");

            return Ok("Account was succesfully inserted");

        }
        [Cached(dataChanged: true)] //cache evict
        [ValidateRequest]
        [HttpPatch]
        public async Task<ActionResult<bool>> UpdateAccount([FromBody] UpsertAccountRequest request)
        {
            var result = await _repository.Update(new Account() { id = (int)request.id, username = request.username, password = request.password });

            if (!result)
                throw new Exception("For some reason your update request was not compleated.");

            return Ok(result);

        }
        [Cached(dataChanged: true)] //cache evict
        [ValidateRequest]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteAccount([FromQuery] int id)
        {
            var result = await _repository.Delete(id);

            if (!result)
                throw new Exception("For some reason your delete request was not compleated.");
            
            return Ok(result);
        }

        [Cached(dataChanged: true)] //cache evict
        [ValidateRequest]
        [HttpPost("Transfer")]
        public async Task<ActionResult<bool>> MakeTransfer([FromBody] TransferCashRequest request)
        {
            var result = await transferRepository.Transfer(request.fromId,request.toId, request.ammount);

            return Ok(result);
        }
    }
}
