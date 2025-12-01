namespace ProcureRiskAnalyzer.Models;

public class Tender
{
    public string Id { get; set; } = string.Empty;
    public string BuyerName { get; set; } = string.Empty;
    public decimal ExpectedValue { get; set; }
    public DateTime PublishDate { get; set; }
    public int SupplierCount { get; set; }

    public double RiskScore { get; set; }
}
