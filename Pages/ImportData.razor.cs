
namespace SoilClassifier_Blazor.Pages
{
    public partial class ImportData
    {
        static string Classification(double finePercent, double gravelPercent,
                                double plasticLimit, double liquidLimit)
        {
            
            double sandPercent = 100 - gravelPercent - finePercent;
            double plasticityIndex = liquidLimit - plasticLimit;

            string fineOrCoarse;
            string fineMaterial = "";
            string plasticitySymbol = "";

            if (finePercent >= 35)
            {
                fineOrCoarse = "fine";
            }
            else
            {
                fineOrCoarse = "coarse";
            }

            void CalcPlasticitySymbol()
            {
                switch (liquidLimit)
                {
                    case <= 26:
                        if (plasticityIndex < 8)
                        {
                            fineMaterial = "silt";
                            plasticitySymbol = "ML";

                        }
                        else
                        {
                            fineMaterial = "clay";
                            plasticitySymbol = "CL";
                        }
                        break;
                    case > 26 and <= 50:
                        if (plasticityIndex < 0.73 * (liquidLimit - 20))
                        {
                            fineMaterial = "silt";
                            plasticitySymbol = "ML";
                        }
                        else if (plasticityIndex > 0.73 * (liquidLimit - 20) && liquidLimit <= 35)
                        {
                            fineMaterial = "clay";
                            plasticitySymbol = "CL";
                        }
                        else if (plasticityIndex > 0.73 * (liquidLimit - 20) && liquidLimit > 35)
                        {
                            fineMaterial = "clay";
                            plasticitySymbol = "CI";
                        }
                        break;
                    case > 50:
                        if (plasticityIndex < 0.73 * (liquidLimit - 20))
                        {
                            fineMaterial = "silt";
                            plasticitySymbol = "MH";
                        }
                        else if (plasticityIndex > 0.73 * (liquidLimit - 20))
                        {
                            fineMaterial = "clay";
                            plasticitySymbol = "CH";
                        }
                        break;
                    default:
                        break;
                }
            }

            CalcPlasticitySymbol();

            string primary = "";
            string[] secondary = new string[] { };
            string[] prefix = new string[] { };
            string[] trace = new string[] { };

            if (fineOrCoarse == "fine")
            {
                primary = fineMaterial.ToUpper();
            }
            else if (fineOrCoarse == "coarse")
            {
                if (gravelPercent >= sandPercent)
                {
                    primary = "GRAVEL";
                }
                else if (sandPercent > gravelPercent)
                {
                    primary = "SAND";
                }
            }

            switch (primary)
            {
                case "GRAVEL":
                    AppendSand();
                    AppendFine();
                    break;
                case "SAND":
                    AppendGravel();
                    AppendFine();
                    break;
                case "CLAY" or "SILT":
                    AppendGravel();
                    AppendSand();
                    break;
                default:
                    break;
            }

            void AppendGravel()
            {
                switch (gravelPercent)
                {
                    case <= 15 and > 0:
                        trace = trace.Append("gravel").ToArray();
                        break;

                    case > 15 and <= 30:
                        secondary = secondary.Append("gravel").ToArray();
                        break;

                    case > 30:
                        prefix = prefix.Append("gravelly").ToArray();
                        break;

                    default:
                        break;
                }
            }

            void AppendSand()
            {
                switch (sandPercent)
                {
                    case <= 15 and > 0:
                        trace = trace.Append("sand").ToArray();
                        break;

                    case > 15 and <= 30:
                        secondary = secondary.Append("sand").ToArray();
                        break;

                    case > 30:
                        prefix = prefix.Append("sandy").ToArray();
                        break;

                    default:
                        break;
                }
            }

            void AppendFine()
            {
                switch (finePercent)
                {
                    case <= 5 and > 0:
                        trace = trace.Append(fineMaterial).ToArray();
                        break;

                    case > 5 and <= 12:
                        secondary = secondary.Append(fineMaterial).ToArray();
                        break;

                    case > 12:
                        if (fineMaterial == "clay")
                        {
                            prefix = prefix.Append("clayey").ToArray();
                        }
                        else if (fineMaterial == "silt")
                        {
                            prefix = prefix.Append("silty").ToArray();
                        }
                        break;

                    default:
                        break;
                }
            }


            // Group Symbol Calculation
            string groupSymbol = "";

            void SymbolCalculation()
            {
                switch (primary)
                {
                    case "GRAVEL":
                        switch (finePercent)
                        {
                            // TODO: Add coeficient of Uniformity and Curvature check
                            case <= 5 and >= 0:
                                groupSymbol = "GP";
                                break;

                            case > 5 and < 12:
                                switch (fineMaterial)
                                {
                                    case "clay":
                                        groupSymbol = "GP-GC";
                                        break;

                                    case "silt":
                                        groupSymbol = "GP-GM";
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case >= 12:
                                switch (fineMaterial)
                                {
                                    case "clay":
                                        groupSymbol = "GC";
                                        break;

                                    case "silt":
                                        groupSymbol = "GM";
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            default:
                                break;
                        }
                        break;

                    case "SAND":
                        switch (finePercent)
                        {
                            // TODO: Add coeficient of Uniformity and Curvature check
                            case <= 5 and >= 0:
                                groupSymbol = "SP";
                                break;

                            case > 5 and < 12:
                                switch (fineMaterial)
                                {
                                    case "clay":
                                        groupSymbol = "SP-SC";
                                        break;

                                    case "silt":
                                        groupSymbol = "SP-SM";
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case >= 12:
                                switch (fineMaterial)
                                {
                                    case "clay":
                                        groupSymbol = "SC";
                                        break;

                                    case "silt":
                                        groupSymbol = "SM";
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            default:
                                break;
                        }
                        break;

                    case "CLAY":
                        groupSymbol = plasticitySymbol;
                        break;

                    case "SILT":
                        groupSymbol = plasticitySymbol;
                        break;

                    default:
                        break;
                }
            }

            SymbolCalculation();


            string prefixPrimarySeparator = "";
            string primarySecondarySeparator = "";
            string secondaryTraceSeparator = "";

            if (prefix.Length != 0)
            {
                prefixPrimarySeparator = " ";
            }

            if (secondary.Length != 0)
            {
                primarySecondarySeparator = " with ";
            }

            if (trace.Length != 0)
            {
                secondaryTraceSeparator = ", trace ";
            }


            string result = "(" + groupSymbol + ") " + String.Join(" ", prefix) + prefixPrimarySeparator +
                     primary + primarySecondarySeparator + String.Join(" & ", secondary) +
                     secondaryTraceSeparator + String.Join(" & ", trace);

            return result;
        }
    }
}
