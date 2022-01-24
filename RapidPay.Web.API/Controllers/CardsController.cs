using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RapidPay.Domain.Adapters;
using RapidPay.Domain.Adapters.Interfaces;
using RapidPay.Domain.Commands;

namespace RapidPay.Web.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly ICardAdapter _cardAdapter;

        public CardsController(ILogger<CardsController> logger, ICardAdapter cardAdapter)
        {
            _logger = logger;
            _cardAdapter = cardAdapter;
        }

        [HttpGet(template:"{cardId:guid}/balance", Name = "Balance")]
        public async Task<IActionResult> Balance(Guid cardId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _cardAdapter.BalanceAsync(cardId);
            return Ok(result);
        }
        
        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create([FromBody] CreateCardCmd command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cardAdapter.CreateAsync(command);
            return Ok(result);

        }
        
        [HttpPost("{cardId:guid}/payment",Name = "Payment")]
        public async Task<IActionResult> Payment([FromBody] PaymentCardCmd command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cardAdapter.PaymentAsync(command);
            return Ok(result);

        }
    }
}