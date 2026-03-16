using Microsoft.AspNetCore.Mvc;
using ApexInvest.Modules.Market.Services;
using ApexInvest.Infrastructure.Database;
using ApexInvest.Modules.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApexInvest.Modules.Market.Controllers;

[ApiController]
[Route("api/controller")]
public class MarketController : ControllerBase
{
    private readonly B3ParserService _b3Parser;
    private readonly ApexDbContext _context;

    public MarketController(B3ParserService b3Parser, ApexDbContext context)
    {
        _b3Parser = b3Parser;
        _context = context;
    }

    [HttpPost("upload-cotahist")]
    public async Task<IActionResult> UploadCotahist(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("Por favor, envie um ficheiro COTAHIST válido.");

        using var reader = new StreamReader(file.OpenReadStream());
        string? line;
        int count = 0;

        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (!line.StartsWith("01")) continue;

            var ticker = _b3Parser.ParseTicker(line);
            var price = _b3Parser.ParsePrice(line);

            var dateStr = line.Substring(2, 8);
            var refDate = DateTime.ParseExact(dateStr, "yyyyMMdd", null);

            var existingPrice = await _context.StockPrices.FirstOrDefaultAsync(sp => sp.Ticker == ticker && sp.ReferenceDate == refDate);

            if (existingPrice != null)
            {
                existingPrice.UpdatedPrice(price, refDate);
            }
            else
            {
                var newPrice = new StockPrice(ticker, price, refDate);
                _context.StockPrices.Add(newPrice);
            }
            count++;
        }
        await _context.SaveChangesAsync();
        return Ok(new { Message = $"Processamento concluído. {count} preços atualizados." });
    }
}