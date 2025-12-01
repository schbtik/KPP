using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Data;
using ProcureRiskAnalyzer.Web.Models;

namespace ProcureRiskAnalyzer.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SuppliersApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        // GET: api/v1/suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }

        // POST: api/v1/suppliers
        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSupplier), new { id = supplier.Id }, supplier);
        }

        // PUT: api/v1/suppliers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest();
            }

            _context.Entry(supplier).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/v1/suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<Dictionary<string, int>>> GetStatistics()
        {
            var statistics = await _context.Suppliers
                .GroupBy(s => s.Country)
                .Select(g => new { Country = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Country ?? "Unknown", x => x.Count);

            return statistics;
        }
    }
}
