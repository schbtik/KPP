using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Data;

namespace ProcureRiskAnalyzer.Web.Controllers
{
    public class TendersController : Controller
    {
        private readonly AppDbContext _context;

        public TendersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Tenders
        public async Task<IActionResult> Index()
        {
            var tenders = await _context.Tenders
                .Include(t => t.Supplier)
                .ToListAsync();

            return View(tenders);
        }

        // GET: /Tenders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tender = await _context.Tenders
                .Include(t => t.Supplier)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tender == null)
                return NotFound();

            return View(tender);
        }
    }
}
