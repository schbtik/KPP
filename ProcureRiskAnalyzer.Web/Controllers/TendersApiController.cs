using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Data;
using ProcureRiskAnalyzer.Web.Models;

namespace ProcureRiskAnalyzer.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TendersApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public TendersApiController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tender>>> GetTenders()
    {
        return await _context.Tenders
            .Include(t => t.Supplier)
            .Include(t => t.Category)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tender>> GetTender(int id)
    {
        var tender = await _context.Tenders
            .Include(t => t.Supplier)
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tender == null)
        {
            return NotFound();
        }

        return tender;
    }

    [HttpPost]
    public async Task<ActionResult<Tender>> CreateTender(Tender tender)
    {
        _context.Tenders.Add(tender);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTender), new { id = tender.Id }, tender);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTender(int id, Tender tender)
    {
        if (id != tender.Id)
        {
            return BadRequest();
        }

        _context.Entry(tender).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TenderExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTender(int id)
    {
        var tender = await _context.Tenders.FindAsync(id);
        if (tender == null)
        {
            return NotFound();
        }

        _context.Tenders.Remove(tender);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("statistics")]
    public async Task<ActionResult<Dictionary<string, decimal>>> GetStatistics()
    {
        var statistics = await _context.Tenders
            .Include(t => t.Category)
            .GroupBy(t => t.Category.Name)
            .Select(g => new { Category = g.Key, TotalValue = g.Sum(t => t.ExpectedValue) })
            .ToDictionaryAsync(x => x.Category, x => x.TotalValue);

        return statistics;
    }

    private bool TenderExists(int id)
    {
        return _context.Tenders.Any(e => e.Id == id);
    }
}


