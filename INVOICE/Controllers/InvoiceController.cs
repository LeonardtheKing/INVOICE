using Bogus;
using INVOICE.Models;
using INVOICE.Services;
using Microsoft.AspNetCore.Mvc;

namespace INVOICE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController(InvoiceRenderingService invoiceRenderingService) : ControllerBase
{
    [HttpGet("Generatepdf")]
    public IActionResult GeneratePdf()
    {
        var invoice = new Faker<Invoice>()
            .RuleFor(i=>i.InvoiceDate,f=>f.Date.Recent(30))
            .RuleFor(i=>i.InvoiceNumber,f=>f.Random.Number(10000,99999).ToString())
            .Generate();

        invoice.Client = new Faker<Client>()
            .RuleFor(c=>c.ClientName,f=>f.Company.CompanyName())
            .RuleFor(c=>c.ClientAddress,f=>f.Address.FullAddress())
            .Generate();

        invoice.InvoiceItems = new();

        for(var i=0;i<15; i++)
        {
            invoice.InvoiceItems.Add(new Faker<InvoiceItem>()
                .RuleFor(i => i.Description, f => f.Commerce.ProductName())
                .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(i => i.UnitPrice, f => decimal.Parse(f.Commerce.Price()))
                .Generate());
        }

        var document = invoiceRenderingService.GenerateInvoicePdf(invoice);
        return File(document, "application/pdf", "invoice.pdf");



    }
}
