using ForeignExchange.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForeignExchange.Api.Controllers;

[ApiController]
[Route("forex")]
public class FxController : ControllerBase
{
    private readonly IQuoteService _quoteService;

    public FxController(IQuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    [HttpGet("quotes/{baseCurrency}/{quoteCurrency}/{amount:decimal}")]
    public async Task<IActionResult> GetQuote(
        string baseCurrency, string quoteCurrency, decimal amount)
    {
        var quote = await _quoteService.GetQuoteAsync(baseCurrency, quoteCurrency, amount);

        if (quote is null)
        {
            return NotFound();
        }

        return Ok(quote);
    }
}
