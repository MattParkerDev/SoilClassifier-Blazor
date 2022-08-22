using QuestPDF.Fluent;
using QuestPDF.Drawing;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
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
                        page.Margin(50);

                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);

                        page.Footer().AlignCenter().Text(text =>
                        {
                            text.CurrentPageNumber();
                            text.Span(" / ");
                            text.TotalPages();
                        });
                    });
            }
            void ComposeHeader(IContainer container)
            {
                container.Row(row =>
                {
                    row.RelativeItem().Column(Column =>
                    {
                        Column
                            .Item().Text("Goat")
                            .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                        Column.Item().Text(text =>
                        {
                            text.Span("Yong").SemiBold();
                            text.Span("Test");
                        });

                        Column.Item().Text(text =>
                        {
                            text.Span("RRRRR").SemiBold();
                            text.Span("Yonk");
                        });
                    });

                    row.ConstantItem(100).Height(50).Placeholder();
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

                    column.Item().Element(ComposeTable);

                    //var totalPrice = 0.0;
                    column.Item().PaddingRight(5).AlignRight().Text("Total Price").SemiBold();
                    var testString = "Test";
                    if (!string.IsNullOrWhiteSpace(testString))
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
                container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
                {
                    column.Spacing(5);
                    column.Item().Text("Comments").FontSize(14).SemiBold();
                    column.Item().Text("Example Comment");
                });
            }
        }

        public class AddressComponent : IComponent
        {
            public void Compose(IContainer container)
            {
                container.ShowEntire().Column(column =>
                {
                    column.Spacing(2);

                    column.Item().Text("Title").SemiBold();
                    column.Item().PaddingBottom(5).LineHorizontal(1);

                    column.Item().Text("Company Name");
                    column.Item().Text("Street Address");
                    column.Item().Text("City, State");
                    column.Item().Text("email");
                    column.Item().Text("phone");
                });
            }
        }
    }
}
