using ProcureRiskAnalyzer.Models;
using ProcureRiskAnalyzer.Services;

Console.WriteLine("=========================================");
Console.WriteLine("   PROCURE RISK ANALYZER (v1.0 foundation)");
Console.WriteLine("=========================================\n");


var tender = new Tender
{
    Id = "T001",
    BuyerName = "Міністерство освіти",
    ExpectedValue = 1000000m,
    PublishDate = DateTime.Now,
    SupplierCount = 3
};

var analyzer = new RiskAnalyzer();
double risk = analyzer.CalculateRisk(tender);

Console.WriteLine($"Tender ID: {tender.Id}");
Console.WriteLine($"Buyer: {tender.BuyerName}");
Console.WriteLine($"Expected Value: {tender.ExpectedValue:N0} ₴");
Console.WriteLine($"Suppliers: {tender.SupplierCount}");
Console.WriteLine($"Risk Score: {risk}");
Console.WriteLine("\nFoundation initialized successfully!");

