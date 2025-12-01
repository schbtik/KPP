using Microsoft.AspNetCore.Mvc;
using ProcureRiskAnalyzer.Web.Data;
using ProcureRiskAnalyzer.Web.Models;
using System.Linq;

namespace ProcureRiskAnalyzer.Web.Controllers
{
    [ApiController]
    [Route("api/v2/suppliers")]
    public class SuppliersApiV2Controller : ControllerBase
    {
        private readonly AppDbContext _context;

        public SuppliersApiV2Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/v2/suppliers
        [HttpGet]
        public IActionResult GetAll()
        {
            // У версії 2 ми додаємо нову інформацію — кількість тендерів у кожного постачальника
            var suppliers = _context.Suppliers
                .Select(s => new
                {
                    Ідентифікатор = s.Id,
                    Назва = s.Name,
                    Країна = s.Country,
                    Кількість_тендерів = s.Tenders.Count
                })
                .ToList();

            return Ok(suppliers);
        }

        // GET: api/v2/suppliers/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var supplier = _context.Suppliers
                .Where(s => s.Id == id)
                .Select(s => new
                {
                    Ідентифікатор = s.Id,
                    Назва = s.Name,
                    Країна = s.Country,
                    Кількість_тендерів = s.Tenders.Count
                })
                .FirstOrDefault();

            if (supplier == null)
                return NotFound(new { message = "Постачальника не знайдено" });

            return Ok(supplier);
        }
    }
}
