using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KooliProjekt.WebAPI.Controllers
{
 [Authorize, ApiController, Route("api/[controller]")]
 public class PublicController : ControllerBase
 {
  private readonly ApplicationDbContext _db; private readonly IUserProvider _userProvider; public PublicController(ApplicationDbContext db, IUserProvider userProvider) { _db=db; _userProvider=userProvider; }
  private int? CurrentUserId => _userProvider.UserId();
  public sealed class CreatePortfolioRequest { public string Name { get; set; } }
  public sealed class AddAssetRequest { public string Name { get; set; } public string Ticker { get; set; } public int AssetClassId { get; set; } }
  public sealed class MonthlyValueRequest { public int AssetId { get; set; } public int Year { get; set; } public int Month { get; set; } public decimal Quantity { get; set; } public decimal Value { get; set; } }
  [HttpGet("my/portfolios")]
  public async Task<IActionResult> MyPortfolios() => Ok(await _db.Portfolios.AsNoTracking().Where(x=>x.OwnerId==CurrentUserId).Select(x=>new { x.Id,x.Name, AssetCount=x.Assets.Count }).ToListAsync());
  [HttpGet("asset-classes")]
  public async Task<IActionResult> AssetClasses() => Ok(await _db.AssetClasses.AsNoTracking().OrderBy(x=>x.Name).Select(x=>new { x.Id,x.Name }).ToListAsync());
  [HttpGet("my/portfolios/{id:int}")]
  public async Task<IActionResult> MyPortfolio(int id)
  {
   var item=await _db.Portfolios.AsNoTracking().Where(x=>x.Id==id && x.OwnerId==CurrentUserId).Select(x=>new { x.Id,x.Name, Assets=x.Assets.Select(a=>new { a.Id,a.Name,a.Ticker,a.AssetClassId,AssetClass=a.AssetClass.Name }) }).FirstOrDefaultAsync();
   return item==null ? NotFound() : Ok(item);
  }
  [HttpPost("my/portfolios")]
  public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioRequest request)
  {
   if (string.IsNullOrWhiteSpace(request?.Name) || request.Name.Length>100) return BadRequest("Portfelli nimi on kohustuslik (kuni 100 märki).");
   var item=new Portfolio { Name=request.Name.Trim(),OwnerId=CurrentUserId.Value }; _db.Portfolios.Add(item); await _db.SaveChangesAsync(); return CreatedAtAction(nameof(MyPortfolio),new { id=item.Id },new { item.Id,item.Name });
  }
  [HttpDelete("my/portfolios/{id:int}")]
  public async Task<IActionResult> DeletePortfolio(int id) { var item=await _db.Portfolios.SingleOrDefaultAsync(x=>x.Id==id && x.OwnerId==CurrentUserId); if(item==null) return NotFound(); _db.Portfolios.Remove(item); await _db.SaveChangesAsync(); return NoContent(); }
  [HttpPost("my/portfolios/{id:int}/assets")]
  public async Task<IActionResult> AddAsset(int id,[FromBody] AddAssetRequest request)
  {
   var portfolio=await _db.Portfolios.SingleOrDefaultAsync(x=>x.Id==id && x.OwnerId==CurrentUserId); if(portfolio==null) return NotFound("Portfelli ei leitud.");
   if(string.IsNullOrWhiteSpace(request?.Name) || string.IsNullOrWhiteSpace(request.Ticker) || request.Name.Length>100 || request.Ticker.Length>20) return BadRequest("Vara nimi ja ticker on kohustuslikud.");
   if(!await _db.AssetClasses.AnyAsync(x=>x.Id==request.AssetClassId)) return BadRequest("Varaklassi ei leitud.");
   var asset=new Asset { Name=request.Name.Trim(),Ticker=request.Ticker.Trim().ToUpperInvariant(),PortfolioId=portfolio.Id,AssetClassId=request.AssetClassId }; _db.Assets.Add(asset); await _db.SaveChangesAsync(); return Ok(new { asset.Id,asset.Name,asset.Ticker });
  }
  [HttpDelete("my/assets/{id:int}")]
  public async Task<IActionResult> RemoveAsset(int id) { var asset=await _db.Assets.SingleOrDefaultAsync(x=>x.Id==id && x.Portfolio.OwnerId==CurrentUserId); if(asset==null) return NotFound(); _db.Assets.Remove(asset); await _db.SaveChangesAsync(); return NoContent(); }
  [HttpGet("my/monthly-history")]
  public async Task<IActionResult> MonthlyHistory() => Ok(await _db.MonthlyValues.AsNoTracking().Where(x=>x.Asset.Portfolio.OwnerId==CurrentUserId).OrderByDescending(x=>x.Year).ThenByDescending(x=>x.Month).Select(x=>new { x.Id,x.AssetId,Asset=x.Asset.Name,x.Year,x.Month,x.Quantity,x.Value,x.Total }).ToListAsync());
  [HttpPost("my/monthly-history")]
  public async Task<IActionResult> AddMonthlyValue([FromBody] MonthlyValueRequest request)
  {
   if(request==null || request.Year<1 || request.Month<1 || request.Month>12 || request.Quantity<0 || request.Value<0) return BadRequest("Kontrolli perioodi, kogust ja väärtust.");
   var asset=await _db.Assets.SingleOrDefaultAsync(x=>x.Id==request.AssetId && x.Portfolio.OwnerId==CurrentUserId); if(asset==null) return NotFound("Sinu portfellist vara ei leitud.");
   var row=new MonthlyValue { AssetId=asset.Id,Year=request.Year,Month=request.Month,Quantity=request.Quantity,Value=request.Value,Total=System.Math.Round(request.Quantity*request.Value,2) }; _db.MonthlyValues.Add(row); await _db.SaveChangesAsync(); return Ok(new { row.Id,row.Year,row.Month,row.Quantity,row.Value,row.Total });
  }
 }
}
