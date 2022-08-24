using QuestPDF.Fluent;
using QuestPDF.Drawing;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Elements;
using System.Reflection;
using SoilClassifier_Blazor.Shared;

namespace SoilClassifier_Blazor.Pages
{
    public partial class ExportReport
    {
        public class SoilReport : IDocument
        {
            public ListState Model { get; }
            public SoilReport(ListState listState)
            {
                Model = listState;
            }
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
                            text.Span("Page ").FontSize(8);
                            text.CurrentPageNumber().FontSize(8);
                            text.Span(" of ").FontSize(8);
                            text.TotalPages().FontSize(8);
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
                            Column.Item().Text("Laboratory Services").Bold().FontSize(10);
                            Column.Item().Text("Department").Bold().FontSize(9);
                            Column.Item().Text("Company Name").Bold().FontSize(9);
                            Column.Item().Padding(2);
                            Column.Item().Text("20 Template Drive Suburb").FontSize(8);
                            Column.Item().Text("Ph (07) 3400 0000").FontSize(8);
                        });
                        row.ConstantItem(55).Height(55).Placeholder();

                    });
                    column.Item().Row(row =>
                    {
                        row.RelativeItem(2).Column(Column =>
                        {
                            Column.Item().Text("Test Procedures: AS1289 2.1.1, 3.1.2, 3.2.1, 3.3.1, 3.4.1, 3.6.1").Bold().FontSize(9);
                        });
                        row.RelativeItem(2).Column(Column =>
                        {
                            Column.Item().Text($"Work Package No. {Model.ProjectNumber}").Bold().FontSize(9);
                        });
                        row.RelativeItem(1).Column(Column =>
                        {
                            Column.Item().Text($"Report No. WR {Model.ReportNumber}").Bold().FontSize(9);
                        });
                    });

                    column.Item().Border(1).PaddingVertical(2).PaddingLeft(5).PaddingRight(5).Row(row =>
                    {
                        row.RelativeItem((float)2.8).Column(Column =>
                        {
                            Column.Item().Text($"Client: {Model.ClientName}").FontSize(9);
                            Column.Item().Text($"Project: {Model.ProjectName}").Bold().FontSize(10);
                            Column.Item().Text($"Limits: {Model.Limits}").FontSize(9);
                            Column.Item().Text($"Chainage measured from: {Model.ChainageMeasuredFrom}").FontSize(9);
                        });

                        row.RelativeItem((float)1.9).AlignBottom().Column(Column =>
                        {
                            Column.Item().Text($"Notes: {Model.Notes}").FontSize(9);
                        });
                        //TODO: Fix Sampled by and Date spacing and dynamic Column size
                        row.RelativeItem((float)1.6).Column(Column =>
                        {
                            Column.Item().Row(row =>
                            {
                                row.AutoItem().Text($"Sampled by: {Model.SampledBy}").FontSize(9);
                                row.AutoItem().PaddingLeft(5).Text($"Date: {Model.SampledDate}").FontSize(9);
                            });
                            Column.Item().Row(row =>
                            {
                                row.AutoItem().AlignLeft().Text($"Prepared by: {Model.PreparedBy}").FontSize(9);
                                row.AutoItem().AlignRight().PaddingLeft(5).Text($"Date: {Model.PreparedDate}").FontSize(9);
                            });

                            Column.Item().Text($"\nGrouped Samples: {Model.GroupedSamples}").FontSize(9);
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
                    column.Item().PaddingTop(10).Element(ComposeComments);
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
                        header.Cell().ColumnSpan(6).Element(BoldCellStyle).AlignCenter().Text("Location").Style(headerStyle);
                        header.Cell().ColumnSpan(6).Element(BoldCellStyle).AlignCenter().Text("Particle Size Distribution").Style(headerStyle);
                        header.Cell().RowSpan(3).Element(BoldCellStyle).AlignCenter().AlignBottom().Text("Field Moisture\n\n\n(%)").Style(headerStyle);
                        header.Cell().ColumnSpan(6).Element(BoldCellStyle).AlignCenter().Text("Plasticity Index").Style(headerStyle);
                        header.Cell().RowSpan(3).Element(BoldCellStyle).AlignCenter().AlignMiddle().Text("Sample Description").Style(headerStyle);

                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignMiddle().Text("Sample Number").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignBottom().Text("RSI Chainage\n\n(m)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignBottom().Text("Offset From CL\n\n(m)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignMiddle().Text("Bore Hole No.").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignBottom().Text("\nDepth Below Existing Surface\n\n(mm)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignMiddle().Text("Surface Type").Style(subHeaderStyle);

                        header.Cell().ColumnSpan(6).Element(BoldCellStyle).AlignCenter().Text("Percent Passing").Style(headerStyle);

                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignBottom().Text("Plastic Limit\n\n(%)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignBottom().Text("Plasticity Index\n\n(%)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignBottom().Text("Linear Shrinkage\n\n(%)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignBottom().Text("Liquid Limit\n\n(%)").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignMiddle().Text("PI Test Remarks").Style(subHeaderStyle);
                        header.Cell().RowSpan(2).Element(BoldCellStyle).AlignCenter().AlignMiddle().Text("WPI").Style(subHeaderStyle);


                        header.Cell().Element(BoldCellStyle).AlignCenter().AlignBottom().Text("53\n\n\nmm").Style(subHeaderStyle);
                        header.Cell().Element(BoldCellStyle).AlignCenter().AlignBottom().Text("19\n\n\nmm").Style(subHeaderStyle);
                        header.Cell().Element(BoldCellStyle).AlignCenter().AlignBottom().Text("4.75\n\n\nmm").Style(subHeaderStyle);
                        header.Cell().Element(BoldCellStyle).AlignCenter().AlignBottom().Text("2.36\n\n\nmm").Style(subHeaderStyle);
                        header.Cell().Element(BoldCellStyle).AlignCenter().AlignBottom().Text("425\n\n\nμm").Style(subHeaderStyle);
                        header.Cell().Element(BoldCellStyle).AlignCenter().AlignBottom().Text("75\n\n\nμm").Style(subHeaderStyle);

                    });
                    if (Model.soilSamples != null)
                    {
                        foreach (var sample in Model.soilSamples)
                        {
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.SampleID).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.Chainage).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.Offset).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.BoreNumber).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.Depth).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.SurfaceType).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.Sieve53).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.Sieve19).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.Sieve475).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.Sieve236).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.Sieve425).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.Sieve075).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.MoistureContent).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.PlasticLimit).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.PlasticityIndex).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.LinearShrinkage).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.LiquidLimit).Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text("").Style(headerStyle);
                            table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text("").Style(headerStyle);
                            if (sample.SoilClassification != "")
                            {
                                table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(4).PaddingVertical(2).Text(sample.SoilClassification + "," + sample.Colour + ", moist").Style(headerStyle);
                            }
                            else if (sample.SurfaceType == "AC")
                            {
                                table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(4).PaddingVertical(2).Text("AC").Style(headerStyle);
                            }
                            else
                            {
                                table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(4).PaddingVertical(2).Text("").Style(headerStyle);
                            }
                            

                        }
                    }
                    
                    static IContainer CellStyle(IContainer container) => container.Border((float)0.5).BorderColor(Colors.Black);
                    static IContainer BoldCellStyle(IContainer container) => container.Border((float)1).BorderColor(Colors.Black);
                });
            }

            void ComposeComments(IContainer container)
            {
                container.Column(column =>
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
                            column.Item().Text($"NATA Accreditation Number: {Model.NataNumber}").FontSize(8);
                            column.Item().Text("Accredited for compliance with ISO/IEC 17025 - Testing").Bold().FontSize(8);
                        });
                    });
                    column.Item().PaddingTop(3).Row(row =>
                    {
                        row.RelativeItem().AlignCenter().Column(column =>
                        {
                            column.Item().Text($"Authorised Signatory: {Model.SignatoryName}").Bold().FontSize(10);
                        });
                        row.RelativeItem(1).AlignCenter().Column(column =>
                        {
                            column.Item().Text($"Date: {Model.PreparedDate}").Bold().FontSize(10);
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
