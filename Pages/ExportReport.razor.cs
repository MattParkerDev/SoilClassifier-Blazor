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
                            Column.Item().Text("Test Procedures: AS1289 2.1.1, 3.1.2, 3.2.1, 3.3.1, 3.4.1, 3.6.1").Bold().FontSize(9);
                        });
                        row.RelativeItem(2).Column(Column =>
                        {
                            Column.Item().Text("Work Package No. MP99").Bold().FontSize(9);
                        });
                        row.RelativeItem(1).Column(Column =>
                        {
                            Column.Item().Text("Report No. WR 1234").Bold().FontSize(9);
                        });
                    });

                    column.Item().Border(1).PaddingVertical(2).PaddingLeft(5).PaddingRight(5).Row(row =>
                    {
                        row.RelativeItem((float)2.8).Column(Column =>
                        {
                            Column.Item().Text("Client: Engineering Department").FontSize(9);
                            Column.Item().Text("Project: Elizabeth Street, Brisbane City").Bold().FontSize(10);
                            Column.Item().Text("Limits: Edward Street to Albert Street").FontSize(9);
                            Column.Item().Text("Chainage measured from: Edward Street").FontSize(9);
                        });

                        row.RelativeItem((float)1.9).AlignBottom().Column(Column =>
                        {
                            Column.Item().Text("Notes:").FontSize(9);
                        });
                        //TODO: Fix Sampled by and Date spacing and dynamic Column size
                        row.RelativeItem((float)1.6).Column(Column =>
                        {
                            Column.Item().Row(row =>
                            {
                                row.AutoItem().Text("Sampled by : AB, BC").FontSize(9);
                                row.AutoItem().PaddingLeft(5).Text("Date: 21/07/22").FontSize(9);
                            });
                            Column.Item().Row(row =>
                            {
                                row.AutoItem().AlignLeft().Text("Prepared by: CD").FontSize(9);
                                row.AutoItem().AlignRight().PaddingLeft(5).Text("Date: 02/08/22").FontSize(9);
                            });

                            Column.Item().Text("\nGrouped Samples: 2,5,17  8,12").FontSize(9);
                        });
                    });
                });

            }

            void ComposeContent(IContainer container)
            {
                container.Column(column =>
                {
                    //column.Spacing(20);
                    column.Item().Element(ComposeTable);
                    column.Item().PaddingTop(25).Element(ComposeComments);
                });
            }

            void ComposeTable(IContainer container)
            {
                var headerStyle = TextStyle.Default.SemiBold().FontSize(8);
                var subHeaderStyle = TextStyle.Default.FontSize(8);

                container.Table(table =>
                {

                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(40);
                        columns.ConstantColumn(30);
                        columns.ConstantColumn(35);
                        columns.ConstantColumn(32);

                        columns.ConstantColumn(22);
                        columns.ConstantColumn(22);
                        columns.ConstantColumn(22);
                        columns.ConstantColumn(22);
                        columns.ConstantColumn(22);
                        columns.ConstantColumn(22);

                        columns.ConstantColumn(35);
                        columns.ConstantColumn(35);
                        columns.ConstantColumn(35);
                        columns.ConstantColumn(38);
                        columns.ConstantColumn(35);
                        columns.ConstantColumn(35);
                        columns.ConstantColumn(25);
                        columns.RelativeColumn();

                    });

                    table.Header(header =>
                    {
                        header.Cell().ColumnSpan(6).Element(CellStyle).AlignCenter().Text("Location").Style(headerStyle);
                        header.Cell().ColumnSpan(6).Element(CellStyle).AlignCenter().Text("Particle Size Distribution").Style(headerStyle);
                        header.Cell().RowSpan(3).Element(CellStyle).AlignCenter().AlignBottom().Text("Field Moisture\n\n\n(%)").Style(headerStyle);
                        header.Cell().ColumnSpan(6).Element(CellStyle).AlignCenter().Text("Plasticity Index").Style(headerStyle);
                        header.Cell().RowSpan(3).Element(CellStyle).AlignCenter().AlignMiddle().Text("Sample Description").Style(headerStyle);

                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignMiddle().Text("Sample Number").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignBottom().Text("RSI Chainage\n\n(m)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignBottom().Text("Offset From CL\n\n(m)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignMiddle().Text("Bore Hole No.").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignBottom().Text("\nDepth Below Existing Surface\n\n(mm)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignMiddle().Text("Surface Type").Style(subHeaderStyle);

                        header.Cell().ColumnSpan(6).AlignCenter().Text("Percent Passing").Style(headerStyle);

                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignBottom().Text("Plastic Limit\n\n(%)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignBottom().Text("Plasticity Index\n\n(%)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignBottom().Text("Linear Shrinkage\n\n(%)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignBottom().Text("Liquid Limit\n\n(%)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignMiddle().Text("PI Test Remarks").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(CellStyle).AlignCenter().AlignMiddle().Text("WPI").Style(subHeaderStyle);


                        header.Cell().Element(CellStyle).AlignCenter().AlignBottom().Text("53\n\n\nmm").Style(subHeaderStyle);
                        header.Cell().Element(CellStyle).AlignCenter().AlignBottom().Text("19\n\n\nmm").Style(subHeaderStyle);
                        header.Cell().Element(CellStyle).AlignCenter().AlignBottom().Text("4.75\n\n\nmm").Style(subHeaderStyle);
                        header.Cell().Element(CellStyle).AlignCenter().AlignBottom().Text("2.36\n\n\nmm").Style(subHeaderStyle);
                        header.Cell().Element(CellStyle).AlignCenter().AlignBottom().Text("425\n\n\nμm").Style(subHeaderStyle);
                        header.Cell().Element(CellStyle).AlignCenter().AlignBottom().Text("75\n\n\nμm").Style(subHeaderStyle);

                    });

                    //foreach (var item in Model.Items)
                    //{
                    table.Cell().Element(CellStyle).AlignCenter().Text("MP99-1").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("235").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("1.5 R").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("1").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("0-60").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("100").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("85").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("65").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("40").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("20").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("15").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("5.7").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("18").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("30").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("13.5").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("48").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("").Style(headerStyle);
                    table.Cell().Element(CellStyle).AlignCenter().Text("(GP-GM) sandy GRAVEL with silt, brown, moist").Style(headerStyle);

                    //}
                    static IContainer CellStyle(IContainer container) => container.Border(1).BorderColor(Colors.Black);
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
