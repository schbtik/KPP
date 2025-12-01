using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Data;

namespace ProcureRiskAnalyzer.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? buyer, string? supplier, DateTime? from, DateTime? to)
        {
            var query = _context.Tenders.Include(t => t.Supplier).AsQueryable();

            if (!string.IsNullOrEmpty(buyer))
                query = query.Where(t => t.Buyer.Contains(buyer));

            if (!string.IsNullOrEmpty(supplier))
                query = query.Where(t => t.Supplier.Name.Contains(supplier));

            if (from.HasValue)
                query = query.Where(t => t.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(t => t.Date <= to.Value);

            var results = await query.ToListAsync();
            return View("Results", results);
        }
    }
}
