
using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Vinaio.Reports
{
    public class NewJerseyReport : BaseReport
    {

        protected override void AddCustomHeaders(ISheet worksheet, out int startRow, out int startColumn)
        {
            // Add specific headers for New York Report
            worksheet.GetRow(2).CreateCell(1).SetCellValue("WHOLESALER");
            worksheet.GetRow(2).CreateCell(2).SetCellValue("Vinaio Imports, LTD");

            worksheet.GetRow(3).CreateCell(1).SetCellValue("LICENSE/PERMIT NUMBER");
            worksheet.GetRow(3).CreateCell(2).SetCellValue("3404-23-372-001");

            startRow = 5; // Adjust as needed
            startColumn = 1; // Adjust as needed
        }

        public NewJerseyReport()
        {
            reportName = $"NewJerseyReport.xlsx";
            calculatedColumns = new List<CalculatedColumn>();

            CalculatedColumn upcCodeColumn = new CalculatedColumn
            {
                Name = "UPC CODE",
                Calculation = (row, parameters) =>
                {


                    if (row["UPCCode"] != DBNull.Value)
                    {
                        return Convert.ToInt32(row["UPCCode"]);
                    }
                    else
                    {
                        return "";
                    }

                },
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn fromDateColumn = new CalculatedColumn
            {
                Name = "FROM DATE",
                Calculation = (row, parameters) => DateTime.Now.AddMonths(1).Date.AddDays(1 - DateTime.Now.AddMonths(1).Day).ToString("MM/dd/yyyy"),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn toDateColumn = new CalculatedColumn
            {
                Name = "TO DATE",
                Calculation = (row, parameters) => DateTime.Now.AddMonths(2).Date.AddDays(-DateTime.Now.AddMonths(2).Day).ToString("MM/dd/yyyy"),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn BRNumberColumn = new CalculatedColumn
            {
                Name = "BRAND REGISTRATION NUMBER",
                Calculation = (row, parameters) =>
                {
                    if (row["BRNumber"] != DBNull.Value)
                    {
                        return Convert.ToInt32(row["BRNumber"]);
                    }
                    else
                    {
                        return "Pending";
                    }
                },
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn productNameColumn = new CalculatedColumn
            {
                Name = "PRODUCT NAME",
                Calculation = (row, parameters) => Convert.ToString(row["ProductName"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn abvColumn = new CalculatedColumn
            {
                Name = "PROOF OR ABV (%)",
                Calculation = (row, parameters) =>
                {

                    return Convert.ToDouble(row["ABV"]) / 100;
                },
                Format = "0.00%",
                RequiresExternalValues = false
            };

            CalculatedColumn typeColumn = new CalculatedColumn
            {
                Name = "UNIT",
                Calculation = (row, parameters) => Convert.ToString(row["Type"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn unitColumn = new CalculatedColumn
            {
                Name = "UNIT QUANTITY",
                Calculation = (row, parameters) => Convert.ToInt32(row["Units"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn unitVolumneColumn = new CalculatedColumn
            {
                Name = "UNIT VOLUME/SIZE",
                Calculation = (row, parameters) =>
                {
                    double volume = Convert.ToDouble(row["Volume"]);
                    string unit = Convert.ToString(row["VolumeUnit"]);

                    // Convert volume to liters if the unit is in ounces
                    if (unit.Equals("Oz", StringComparison.OrdinalIgnoreCase))
                    {
                        // Assuming 1 Oz = 0.0295735 Liters
                        volume *= 0.0295735;
                    }

                    return volume + "L";
                },
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn rebateColumn = new CalculatedColumn
            {
                Name = "RIP/REBATE CODE",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn comboCodeColumn = new CalculatedColumn
            {
                Name = "COMBO CODE",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn beerMixColumn = new CalculatedColumn
            {
                Name = "BEER-MIX & MATCH CODE",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn closeoutColumn = new CalculatedColumn
            {
                Name = "CLOSEOUT PERMIT NUMBER",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn frontlineCasePriceColumn = new CalculatedColumn
            {
                Name = "FRONTLINE CASE LIST PRICE",
                Calculation = (row, parameters) => Convert.ToDouble(row["CaseRetailPrice"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn frontlineUnitPriceColumn = new CalculatedColumn
            {
                Name = "FRONTLINE UNIT LIST PRICE",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(row["CaseRetailPrice"]) / Convert.ToDouble(row["Units"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn bestCasePriceColumn = new CalculatedColumn
            {
                Name = "BEST CASE PRICE",
                Calculation = (row, parameters) =>
                {
                    List<string> discountColumns = new List<string>
                    {
                        "PaletDiscountPercentage1",
                        "PaletDiscountPercentage2",
                        "PaletDiscountPercentage3"
                    };

                    double basePrice = Convert.ToDouble(row["CaseRetailPrice"]);

                    // Get the highest discount percentage
                    double highestDiscount = discountColumns
                    .Select(column => row[column])
                    .OfType<IConvertible>() // Filter out non-convertible values
                    .Select(convertible => convertible is DBNull ? 0 : convertible.ToDouble(null))
                    .Where(discount => discount > 0)
                    .DefaultIfEmpty(0)
                    .Max();

                    // Calculate the best case price
                    double bestCasePrice = basePrice * (1 - highestDiscount / 100);
                    return bestCasePrice;
                },
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn bestUnitPriceColumn = new CalculatedColumn
            {
                Name = "BEST UNIT PRICE",
                Calculation = (row, parameters) =>
                {

                    double bestCasePrice = Convert.ToDouble(parameters["BEST CASE PRICE"]);
                    int units = Convert.ToInt32(row["Units"]);
                    return bestCasePrice / units;

                },
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn splitCaseUnitPriceColumn = new CalculatedColumn
            {
                Name = "SPLIT CASE UNIT PRICE",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn paletDiscountThreshold1Column = new CalculatedColumn
            {
                Name = "DISCOUNT QTY1",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountThreshold1"] != DBNull.Value)
                    {
                        return Convert.ToInt16(row["PaletDiscountThreshold1"]);
                    }
                    return "";

                },
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn paletDiscountPercentage1Column = new CalculatedColumn
            {
                Name = "DISCOUNT AMT1",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountPercentage1"] != DBNull.Value)
                    {
                        return Convert.ToDouble(row["PaletDiscountPercentage1"]) / 100;
                    }
                    return 0;

                },
                Format = "0.00%",
                RequiresExternalValues = false
            };

            CalculatedColumn paletDiscountThreshold2Column = new CalculatedColumn
            {
                Name = "DISCOUNT QTY2",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountThreshold2"] != DBNull.Value)
                    {
                        return Convert.ToInt16(row["PaletDiscountThreshold2"]);
                    }
                    return "";

                },
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn paletDiscountPercentage2Column = new CalculatedColumn
            {
                Name = "DISCOUNT AMT2",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountPercentage2"] != DBNull.Value)
                    {
                        return Convert.ToDouble(row["PaletDiscountPercentage2"]) / 100;
                    }
                    return "";

                },
                Format = "0.00%",
                RequiresExternalValues = false
            };

            CalculatedColumn paletDiscountThreshold3Column = new CalculatedColumn
            {
                Name = "DISCOUNT QTY3",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountThreshold3"] != DBNull.Value)
                    {
                        return Convert.ToInt16(row["PaletDiscountThreshold3"]);
                    }
                    return "";

                },
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn paletDiscountPercentage3Column = new CalculatedColumn
            {
                Name = "DISCOUNT AMT3",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountPercentage3"] != DBNull.Value)
                    {
                        return Convert.ToDouble(row["PaletDiscountPercentage3"]) / 100;
                    }
                    return "";

                },
                Format = "0.00%",
                RequiresExternalValues = false
            };



            calculatedColumns.AddRange(new CalculatedColumn[]
            {
                upcCodeColumn,
                fromDateColumn,
                toDateColumn,
                BRNumberColumn,
                productNameColumn,
                abvColumn,
                typeColumn,
                unitColumn,
                unitVolumneColumn,
                rebateColumn,
                comboCodeColumn,
                beerMixColumn,
                closeoutColumn,
                frontlineCasePriceColumn,
                frontlineUnitPriceColumn,
                bestCasePriceColumn,
                bestUnitPriceColumn,
                splitCaseUnitPriceColumn,
                paletDiscountThreshold1Column,
                paletDiscountPercentage1Column,
                paletDiscountThreshold2Column,
                paletDiscountPercentage2Column,
                paletDiscountThreshold3Column,
                paletDiscountPercentage3Column
            });
        }

    }
}
