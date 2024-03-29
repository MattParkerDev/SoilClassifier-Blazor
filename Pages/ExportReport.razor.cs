﻿using QuestPDF.Fluent;
using QuestPDF.Drawing;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Elements;
using System.Reflection;
using SoilClassifier_Blazor.Shared;
using SoilClassifier_Blazor.Models;
using System.Reflection.Emit;

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

                            Column.Item().Text(text =>
                            {
                                text.Span($"\nGrouped Samples: ").FontSize(9);
                                foreach (var group in Model.GroupedSamples)
                                {
                                    text.Span(Convert.ToString(group.BaseSample)).FontSize(9).SemiBold();
                                    foreach (var matched in group.MatchedSamples)
                                    {
                                        if (matched != null)
                                        {
                                            text.Span($",{Convert.ToString(matched.SingleMatchedSample)} ").FontSize(9);
                                        }
                                    }
                                    text.Span(" ").FontSize(9);
                                }
                            });
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
                    column.Item().ShowEntire().PaddingTop(10).Element(ComposeComments);
                    column.Item().Element(ComposeGraphs);
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
                        columns.ConstantColumn(25);
                        columns.ConstantColumn(40);
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

                    foreach (var sample in Model.SoilSamples)
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
                        table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.PIRemarks).Style(headerStyle);
                        table.Cell().Element(CellStyle).AlignCenter().PaddingVertical(2).Text(sample.WeightedPlasticityIndex).Style(headerStyle);
                        if (sample.SoilClassification != "")
                        {
                            if (sample.VisualOnly == false)
                            {
                                table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(4).PaddingVertical(2).Text(sample.SoilClassification + ", " + sample.Colour + ", moist").Style(headerStyle);
                            }
                            else
                            {
                                table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(4).PaddingVertical(2).Text(sample.SoilClassification + ", " + sample.Colour + ", moist").Style(headerStyle).Italic();

                            }
                        }
                        else if (sample.SurfaceType == "AC")
                        {
                            table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(4).PaddingVertical(2).Text("AC").Style(headerStyle);
                        }
                        else if (sample.SurfaceType == "Service")
                        {
                            table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(4).PaddingVertical(2).Text("Service").Style(headerStyle);
                        }
                        else if (sample.SurfaceType == "CON")
                        {
                            table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(4).PaddingVertical(2).Text("Concrete").Style(headerStyle);
                        }
                        else if (sample.SurfaceType == "CTB")
                        {
                            table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(4).PaddingVertical(2).Text("CTB").Style(headerStyle);
                        }
                        else
                        {
                            table.Cell().Element(CellStyle).AlignLeft().PaddingLeft(4).PaddingVertical(2).Text("").Style(headerStyle);
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
                            if (Model.NataLogo != null)
                            {
                                column.Item().AlignRight().Width(40).Image(Model.NataLogo);
                            }
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
            void ComposeGraphs(IContainer container)
            {
                container.Grid(grid =>
                {
                    grid.Columns(3);
                    foreach (var boreHole in Model.BoreHoleList)
                    {
                        grid.Item().ShowEntire().Border(1).PaddingBottom(20).Column(column =>
                        {
                            column.Item().AlignCenter().PaddingTop(10).Text($"Bore Hole {boreHole.BoreNumber}").FontSize(10).Bold();
                            column.Item().AlignCenter().Text($"Chainage {boreHole.Chainage} m").FontSize(9);
                            column.Item().AlignCenter().PaddingBottom(10).Text($"Offset {boreHole.Offset}").FontSize(9);
                            column.Item().Row(row =>
                            {
                                row.ConstantItem(48).Column(column =>
                                {
                                    foreach (var label in boreHole.GraphDepthLabels)
                                    {
                                        float translation = Convert.ToSingle(label) / boreHole.GraphScalingUnit;
                                        column.Item().Unconstrained().Width(70).TranslateY(translation).TranslateX(-22).AlignRight().Text($"{label} -").FontSize(9);
                                    }
                                });
                                row.ConstantItem(1).Column(column =>
                                {
                                    column.Item().Unconstrained().Height((boreHole.GraphMaxDepth / boreHole.GraphScalingUnit) + 13).LineVertical((float)0.7).LineColor(Colors.Black);
                                });
                                row.ConstantItem(35).Column(column =>
                                {
                                    foreach (var layer in boreHole.DCPData)
                                    {
                                        float translation = Convert.ToSingle(layer.Depth) / boreHole.GraphScalingUnit;
                                        column.Item().Unconstrained().Width(35).AlignRight().TranslateY(translation).Text($"{layer.CBR} -").FontSize(9).FontColor(Colors.Red.Medium);
                                    }
                                });
                                row.ConstantItem(180).Column(column =>
                                {
                                    column.Item().PaddingTop(7);
                                    bool hasRun = false;
                                    foreach (var layer in boreHole.LayerList)
                                    {
                                        if (!hasRun)
                                        {
                                            if (layer.StartingDepth != 0)
                                            {
                                                float tempHeight = Convert.ToSingle(layer.StartingDepth) / boreHole.GraphScalingUnit;
                                                column.Item().Row(row =>
                                                {
                                                    row.ConstantItem(45).MaxHeight(tempHeight).MinHeight(tempHeight).Background("FFFFFF").Border((float)0.7).BorderColor("FFFFFF").Height(tempHeight);
                                                });
                                                hasRun = true;
                                            }
                                            else
                                            {
                                                hasRun = true;
                                            }
                                        }

                                        column.Item().Row(row =>
                                        {
                                            float height = Convert.ToSingle(layer.Height) / boreHole.GraphScalingUnit;
                                            row.ConstantItem(45).MaxHeight(height).MinHeight(height).Background(layer.GraphColor).Border((float)0.7).Height(height);
                                            var translation = -11;
                                            var moistureLabel = "";
                                            if (layer.SurfaceType == "AC")
                                            {
                                                moistureLabel = "  AC";
                                            }
                                            else if (layer.SurfaceType == "AR")
                                            {
                                                moistureLabel = "  AR";
                                                translation = -6;
                                            }
                                            else if (layer.SurfaceType == "Service")
                                            {
                                                moistureLabel = "  Service";
                                                translation = -6;
                                            }
                                            else if (layer.MoistureContent != "")
                                            {
                                                moistureLabel = "- " + layer.MoistureContent + "%";
                                            }

                                            // TODO: Review 50px width for label
                                            row.ConstantItem(35).AlignMiddle().Unconstrained().Width(50).Height(20).TranslateY(translation).AlignMiddle().Text(moistureLabel).FontSize(8);


                                            if (layer.SoilClassification == "")
                                            {
                                                row.ConstantItem(95).AlignMiddle().Unconstrained().PaddingLeft(5).Text("").FontSize(7);
                                            }
                                            else
                                            {
                                                row.ConstantItem(95).AlignMiddle().Unconstrained().Width(95).Height(30).TranslateY(-15).AlignMiddle().Text($"{layer.SoilClassification}, {layer.SoilColor}, moist").FontSize(7);
                                            }
                                        });
                                    }
                                });
                            });
                        });
                    }
                });
            }
        }
    }
}
