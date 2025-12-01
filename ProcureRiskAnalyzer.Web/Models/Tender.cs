namespace ProcureRiskAnalyzer.Web.Models;

public class Tender
{
    public int Id { get; set; }
    public string TenderCode { get; set; } = string.Empty;
    public string Buyer { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal ExpectedValue { get; set; }

    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
