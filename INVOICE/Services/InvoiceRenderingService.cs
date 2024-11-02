//using INVOICE.Models;
//using QuestPDF.Companion;
//using QuestPDF.Fluent;
//using QuestPDF.Helpers;
//using QuestPDF.Infrastructure;

//namespace INVOICE.Services
//{
//    public class InvoiceRenderingService
//    {
//        public InvoiceRenderingService()
//        {
//            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
//        }
//        public byte[] GenerateInvoicePdf(Invoice invoice)
//        {
//            var document = Document.Create(container =>
//            {
//                container.Page(page =>
//                {
//                   page.Size(PageSizes.A4);
//                    page.Margin(2,QuestPDF.Infrastructure.Unit.Centimetre);

//                    page.Header()
//                      .Row(row =>
//                      {
//                          row.RelativeItem()
//                             .Column(column =>
//                             {
//                                 column.Item()
//                                  .Text("COMPANY NAME")
//                                  .FontFamily("Arial")
//                                  .FontSize(20)
//                                  .Bold();

//                                 column.Item()
//                                  .Text("Company Address")
//                                  .FontFamily("Arial");

//                                 row.RelativeItem()
//                                  .ShowOnce()
//                                  .Text("INVOICE")
//                                  .AlignRight()
//                                  .FontFamily("Arial")
//                                  .ExtraBlack()
//                                  .FontSize(30);

//                             }

//                              );
//                      });
//                    page.Content()
//                         .PaddingTop(50)
//                         .Column(column =>
//                         {
//                             column.Item().Row(row =>
//                             {
//                                 row.RelativeItem()
//                                 .Column(column2 =>
//                                 {
//                                     column2.Item()
//                                       .Text("Bill To:")
//                                       .Bold();
//                                     column2.Item()
//                                       .Text(invoice.Client.ClientName)
//                                       .FontFamily("Arial")
//                                       .FontSize(12)
//                                       .Bold();

//                                     column2.Item()
//                                           .Text(invoice.Client.ClientAddress);
//                                 });

//                                 row.RelativeItem().Column(column2 => 
//                                 {
//                                 column2.Item()
//                                     .Text($"Invoice #:{invoice.InvoiceNumber}")
//                                     .AlignRight()
//                                      .Bold();

//                                     column2.Item()
//                                     .PaddingTop(2)
//                                     .Text($"Date: {invoice.InvoiceDate: dd-MM-yyyy}")
//                                     .AlignRight();

//                                 });
//                             });

//                             column.Item().Table(table =>
//                             {
//                                 table.ColumnsDefinition(columns =>
//                                 {
//                                     columns.ConstantColumn(40);
//                                     columns.RelativeColumn();
//                                     columns.ConstantColumn(50);
//                                     columns.ConstantColumn(60);
//                                     columns.ConstantColumn(70);
//                                 });

//                                 table.Header(header =>
//                                 {
//                                     header.Cell().Text("#").Bold();
//                                     header.Cell().Text("Description").Bold();
//                                     header.Cell().Text("Qty").AlignRight().Bold();
//                                     header.Cell().Text("Price").AlignRight().Bold();
//                                     header.Cell().Padding(4).Text("Total").AlignRight().Bold();

//                                     header.Cell()
//                                     .ColumnSpan(5)
//                                     .PaddingVertical(5)
//                                     .BorderBottom(1)
//                                     .BorderColor(Colors.Black);

//                                 });

//                                 for(var i=0;i<invoice.InvoiceItems.Count; i++)
//                                 {
//                                     var backgroundColor = i % 2 == 0 ? 
//                                     Color.FromHex("#ffffff") :
//                                     Color.FromHex("#f0f0f0");
//                                     var item = invoice.InvoiceItems[i];
//                                    table.Cell().ShowEntire().Background(backgroundColor).Padding(4).Text((i + 1).ToString());
//                                     table.Cell().ShowEntire().Background(backgroundColor).Padding(4).Text(item.Description);
//                                     table.Cell().ShowEntire().Background(backgroundColor).Padding(4).Text(item.Quantity);
//                                     table.Cell().ShowEntire().Background(backgroundColor).Padding(4).Text(item.UnitPrice);
//                                 }

//                                 table.Cell()
//                                 .ColumnSpan(5)
//                                 .PaddingVertical(5)
//                                 .BorderBottom(1)
//                                 .BorderColor(Colors.Black);

//                                 table.Cell().ColumnSpan(3).Text("Total").Bold().AlignRight();
//                                 table.Cell().AlignRight().Text(invoice.InvoiceItems.Sum(x=>x.UnitPrice));


//                                 column.Item().Column(column =>
//                                 {
//                                     column.Item()
//                                     .PaddingTop(30)
//                                     .Text("Thank you for your business!")
//                                     .FontFamily("Arial")
//                                     .FontSize(15)
//                                     .Bold();
//                                 });
//                             });
//                         });
//                    page.Footer()
//                    .Column(column =>
//                    {
//                        column.Item()
//                        .PaddingVertical(10)
//                        .Text(text =>
//                        {
//                            text.Span("Page ");
//                            text.CurrentPageNumber();
//                            text.Span(" of ");
//                            text.TotalPages();
//                            text.AlignCenter();

//                        });
//                    });
//                });
//            });

//            //document.ShowInCompanion();
//            return document.GeneratePdf();
//        }
//    }
//}


using INVOICE.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace INVOICE.Services
{
    public class InvoiceRenderingService
    {
        public InvoiceRenderingService()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        }

        public byte[] GenerateInvoicePdf(Invoice invoice)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                    page.Header()
                        .Row(row =>
                        {
                            row.RelativeItem()
                                .Column(column =>
                                {
                                    column.Item()
                                        .Text("COMPANY NAME")
                                        .FontFamily("Arial")
                                        .FontSize(20)
                                        .Bold();

                                    column.Item()
                                        .Text("Company Address")
                                        .FontFamily("Arial");
                                });

                            row.RelativeItem()
                                .AlignRight()
                                .Column(column =>
                                {
                                    column.Item()
                                        .Text("INVOICE")
                                        .FontFamily("Arial")
                                        .ExtraBlack()
                                        .FontSize(30);
                                });
                        });

                    page.Content()
                        .PaddingTop(20)
                        .Column(column =>
                        {
                            column.Item()
                                .Row(row =>
                                {
                                    row.RelativeItem()
                                        .Column(column2 =>
                                        {
                                            column2.Item()
                                                .Text("Bill To:")
                                                .Bold();
                                            column2.Item()
                                                .Text(invoice.Client.ClientName)
                                                .FontFamily("Arial")
                                                .FontSize(12)
                                                .Bold();

                                            column2.Item()
                                                .Text(invoice.Client.ClientAddress);
                                        });

                                    row.RelativeItem()
                                        .AlignRight()
                                        .Column(column2 =>
                                        {
                                            column2.Item()
                                                .Text($"Invoice #: {invoice.InvoiceNumber}")
                                                .Bold();

                                            column2.Item()
                                                .Text($"Date: {invoice.InvoiceDate:dd-MM-yyyy}");
                                        });
                                });

                            column.Item()
                                .PaddingVertical(20)
                                .Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(40);
                                        columns.RelativeColumn();
                                        columns.ConstantColumn(50);
                                        columns.ConstantColumn(60);
                                        columns.ConstantColumn(70);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Text("#").Bold();
                                        header.Cell().Text("Description").Bold();
                                        header.Cell().Text("Qty").AlignRight().Bold();
                                        header.Cell().Text("Price").AlignRight().Bold();
                                        header.Cell().Text("Total").AlignRight().Bold();

                                        header.Cell()
                                            .ColumnSpan(5)
                                            .PaddingVertical(5)
                                            .BorderBottom(1)
                                            .BorderColor(Colors.Black);
                                    });

                                    for (var i = 0; i < invoice.InvoiceItems.Count; i++)
                                    {
                                        var backgroundColor = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten3;
                                        var item = invoice.InvoiceItems[i];

                                        table.Cell().Background(backgroundColor).Padding(4).Text((i + 1).ToString());
                                        table.Cell().Background(backgroundColor).Padding(4).Text(item.Description);
                                        table.Cell().Background(backgroundColor).AlignRight().Padding(4).Text(item.Quantity.ToString());
                                        table.Cell().Background(backgroundColor).AlignRight().Padding(4).Text($"{item.UnitPrice:C}");
                                        table.Cell().Background(backgroundColor).AlignRight().Padding(4).Text($"{(item.Quantity * item.UnitPrice):C}");
                                    }

                                    table.Cell()
                                        .ColumnSpan(5)
                                        .PaddingVertical(5)
                                        .BorderBottom(1)
                                        .BorderColor(Colors.Black);

                                    table.Cell().ColumnSpan(3).Text("Total").Bold().AlignRight();
                                    table.Cell().ColumnSpan(2).AlignRight().Text($"{invoice.InvoiceItems.Sum(x => x.Quantity * x.UnitPrice):C}");
                                });

                            column.Item()
                                .PaddingTop(30)
                                .Text("Thank you for your business!")
                                .FontFamily("Arial")
                                .FontSize(15)
                                .Bold();
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Page ");
                            text.CurrentPageNumber();
                            text.Span(" of ");
                            text.TotalPages();
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}

