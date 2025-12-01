using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Data;
using ProcureRiskAnalyzer.Web.Models;

namespace ProcureRiskAnalyzer.Web.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly AppDbContext _context;

        public SuppliersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Suppliers
        public async Task<IActionResult> Index()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return View(suppliers);
        }

        // GET: /Suppliers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.Tenders)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (supplier == null)
                return NotFound();

            return View(supplier);
        }
    }
}
