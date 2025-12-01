using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Data;
using System.Linq;

namespace ProcureRiskAnalyzer.Web.Controllers;

public class StatisticsController : Controller
{
    private readonly AppDbContext _context;

    public StatisticsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Отримуємо статистику тендерів по категоріях (загальна вартість)
        var categoryStatistics = await _context.Tenders
            .Include(t => t.Category)
            .GroupBy(t => t.Category.Name)
            .Select(g => new { Category = g.Key, TotalValue = g.Sum(t => t.ExpectedValue) })
            .ToDictionaryAsync(x => x.Category, x => x.TotalValue);

        // Отримуємо статистику тендерів по постачальникам (кількість тендерів)
        var supplierStatistics = await _context.Tenders
            .Include(t => t.Supplier)
            .GroupBy(t => t.Supplier.Name)
            .Select(g => new { Supplier = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToDictionaryAsync(x => x.Supplier, x => x.Count);

        ViewBag.CategoryStatistics = categoryStatistics;
        ViewBag.SupplierStatistics = supplierStatistics;

        return View();
    }
}

