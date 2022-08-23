using QuestPDF.Fluent;
using QuestPDF.Drawing;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Elements;
using System.Reflection;

namespace SoilClassifier_Blazor.Pages
{
    public partial class ExportReport
    {
        public class SoilReport : IDocument
        {
            public DocumentMetadata GetMetadata() => DocumentMetadata.Default;


            public void Compose(IDocumentContainer container)
            {
                container
                    .Page(page =>
                    {
                        page.Margin(20);
                        page.DefaultTextStyle(x => x.FontFamily("Arial"));
                        page.Size(PageSizes.A4.Landscape());
                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);

                        page.Footer().AlignCenter().Text(text =>
                        {
                            text.Span("Page ");
                            text.CurrentPageNumber();
                            text.Span(" of ");
                            text.TotalPages();
                        });
                    });
            }
            void ComposeHeader(IContainer container)
            {
                container.Column(column =>
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem(2).Column(Column =>
                        {
                            Column
                                .Item().Text("Pavement and Subgrade Evaluation Report")
                                .FontSize(18).SemiBold().FontColor(Colors.Black);

                        });
                        row.RelativeItem(1).Column(Column =>
                        {
                            Column.Item().Text("Laboratory Services").Bold().FontSize(12);
                            Column.Item().Text("Department").Bold().FontSize(10);
                            Column.Item().Text("Company Name").Bold().FontSize(10);
                            Column.Item().Padding(2);
                            Column.Item().Text("20 Template Drive Suburb").FontSize(9);
                            Column.Item().Text("Ph (07) 3400 0000").FontSize(9);
                        });
                        row.ConstantItem(60).Height(60).Placeholder();
                    });
                    column.Item().Row(row =>
                    {
                        row.RelativeItem(2).Column(Column =>
                        {
                            Column.Item().Text("Test Procedures: AS1289 2.1.1, 3.1.2, 3.2.1, 3.3.1, 3.4.1, 3.6.1").Bold().FontSize(10);
                        });
                        row.RelativeItem(2).Column(Column =>
                        {
                            Column.Item().Text("Work Package No. MP99").Bold().FontSize(10);
                        });
                        row.RelativeItem(1).Column(Column =>
                        {
                            Column.Item().Text("Report No. WR 1234").Bold().FontSize(10);
                        });
                    });
                });

            }

            void ComposeContent(IContainer container)
            {
                container.PaddingVertical(40).Column(column =>
                {
                    column.Spacing(20);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem();
                        row.ConstantItem(50);
                        row.RelativeItem();
                    });
                    column.Item().Text("Content");
                    column.Item().Element(ComposeTable);

                    column.Item().PaddingRight(5).AlignRight().Text("Total Price").SemiBold();
                    column.Item().PaddingTop(25).Element(ComposeComments);
                });
            }

            void ComposeTable(IContainer container)
            {
                var headerStyle = TextStyle.Default.SemiBold();

                container.Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(25);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("#");
                        header.Cell().Text("Product").Style(headerStyle);
                        header.Cell().AlignRight().Text("Unit price").Style(headerStyle);
                        header.Cell().AlignRight().Text("Quantity").Style(headerStyle);
                        header.Cell().AlignRight().Text("Total").Style(headerStyle);

                        header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                    });

                    //foreach (var item in Model.Items)
                    //{
                    table.Cell().Element(CellStyle).Text("1");
                    table.Cell().Element(CellStyle).Text("name");
                    table.Cell().Element(CellStyle).AlignRight().Text("Price");
                    table.Cell().Element(CellStyle).AlignRight().Text("Quantity");
                    table.Cell().Element(CellStyle).AlignRight().Text("Total");

                    static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    //}
                });
            }

            void ComposeComments(IContainer container)
            {
                container.ShowEntire().Padding(10).Column(column =>
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Item().Text("Notes:").Italic().FontSize(8);
                        });
                        row.RelativeItem(18).Column(column =>
                        {
                            column.Item().Text("1. Plasticity Index remarks NP = Non Plastic, SC Slipped in cup, CR = Linear Shrinkage crumbled CL = Linear Shrinkage curled").Italic().FontSize(8);
                            column.Item().Text("2. Atterberg samples were oven dried and dry sieved during preparation").Italic().FontSize(8);
                            column.Item().Text("3. Surface Type; CON = Concrete, AC = Asphalt, AR = Auger Refusal, CTB = Cement Treated Base").Italic().FontSize(8);
                            column.Item().Text("4. Sampling Details as reported are not covered by this facilities scope of accreditation or NATA endorsement").Italic().FontSize(8);
                            column.Item().Text("5. Test results reported herein relate only to the tested sample identified in this report").Italic().FontSize(8);
                        });
                        row.RelativeItem(8).AlignBottom().Column(column =>
                        {
                            column.Item().AlignRight().Width(40).Height(40).Placeholder();
                            column.Item().Text("NATA Accreditation Number: ").FontSize(8);
                            column.Item().Text("Accredited for compliance with ISO/IEC 17025 - Testing").Bold().FontSize(8);
                        });
                    });
                    column.Item().PaddingTop(10).Row(row =>
                    {
                        row.RelativeItem().AlignCenter().Column(column =>
                        {
                            column.Item().Text("Authorised Signatory: John Smith").Bold().FontSize(10);
                        });
                        row.RelativeItem(1).AlignCenter().Column(column =>
                        {
                            column.Item().Text("Date: 13/07/2022").Bold().FontSize(10);
                        });
                    });
                    column.Item().Background(Colors.Grey.Lighten1).Row(row =>
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Item().AlignCenter().Text("Company Name").Bold().FontColor(Colors.White);
                        });
                    });
                    column.Spacing(5);
                });
            }
        }
    }
}
