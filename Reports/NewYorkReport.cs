using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinaio.Reports
{
    internal class NewYorkReport : BaseReport
    {
        public NewYorkReport()
        {
            reportName = $"NewYorkReport.xlsx";
            calculatedColumns = new List<CalculatedColumn>();


            CalculatedColumn postTypeColumn = new CalculatedColumn
            {
                Name = "post_type",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(postTypeColumn);

            CalculatedColumn postMonthColumn = new CalculatedColumn
            {
                Name = "post_month",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(postMonthColumn);

            CalculatedColumn postYearColumn = new CalculatedColumn
            {
                Name = "post_year",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(postYearColumn);

            CalculatedColumn wholesalerColumn = new CalculatedColumn
            {
                Name = "wholesaler",
                Calculation = (row, parameters) => { return Convert.ToString(row["WholesalerCode"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(wholesalerColumn);

            CalculatedColumn bevTypeColumn = new CalculatedColumn
            {
                Name = "bev_type",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(bevTypeColumn);

            CalculatedColumn prodItemColumn = new CalculatedColumn
            {
                Name = "prod_item",
                Calculation = (row, parameters) => { return Convert.ToString(row["ProductName"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(prodItemColumn);

            CalculatedColumn comboLimColumn = new CalculatedColumn
            {
                Name = "combo_lim",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(comboLimColumn);

            CalculatedColumn brandRegColumn = new CalculatedColumn
            {
                Name = "brand_reg",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(brandRegColumn);

            CalculatedColumn ttbIdColumn = new CalculatedColumn
            {
                Name = "ttb_id",
                Calculation = (row, parameters) => { return Convert.ToString(row["TTBID"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(ttbIdColumn);

            CalculatedColumn brandNameColumn = new CalculatedColumn
            {
                Name = "brand_name",
                Calculation = (row, parameters) => { return Convert.ToString(row["CellarName"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(brandNameColumn);

            CalculatedColumn prodNameColumn = new CalculatedColumn
            {
                Name = "prod_name",
                Calculation = (row, parameters) => { return Convert.ToString(row["ProductName"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(prodNameColumn);

            CalculatedColumn labelTypeColumn = new CalculatedColumn
            {
                Name = "label_type",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(labelTypeColumn);

            CalculatedColumn pimInfoColumn = new CalculatedColumn
            {
                Name = "prim_info",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(pimInfoColumn);

            CalculatedColumn distribIdColumn = new CalculatedColumn
            {
                Name = "distrib_id",
                Calculation = (row, parameters) =>
                {
                    if (row["DistributorId"] != DBNull.Value)
                    {

                        return Convert.ToDouble(row["DistributorId"]);
                    }
                    return "";
                },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(distribIdColumn);

            CalculatedColumn itemSizeColumn = new CalculatedColumn
            {
                Name = "item_size",
                Calculation = (row, parameters) => { return Convert.ToDouble(row["Volume"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(itemSizeColumn);

            CalculatedColumn itemSizeumColumn = new CalculatedColumn
            {
                Name = "",
                Calculation = (row, parameters) => { return Convert.ToDouble(row["Volume"]) + Convert.ToString(row["VolumeUnit"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(itemSizeumColumn);

            CalculatedColumn umColumn = new CalculatedColumn
            {
                Name = "um",
                Calculation = (row, parameters) => { return Convert.ToString(row["VolumeUnit"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(umColumn);

            CalculatedColumn botPerCaseColumn = new CalculatedColumn
            {
                Name = "botpercase",
                Calculation = (row, parameters) => { return Convert.ToInt16(row["Units"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(botPerCaseColumn);

            CalculatedColumn subPackColumn = new CalculatedColumn
            {
                Name = "subpack",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(subPackColumn);

            CalculatedColumn vintageColumn = new CalculatedColumn
            {
                Name = "vintage",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(vintageColumn);

            CalculatedColumn alcoholColumn = new CalculatedColumn
            {
                Name = "alcohol",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(alcoholColumn);

            CalculatedColumn proofColumn = new CalculatedColumn
            {
                Name = "proof",
                Calculation = (row, parameters) => { return Convert.ToDouble(row["ABV"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(proofColumn);

            CalculatedColumn botPriceColumn = new CalculatedColumn
            {
                Name = "bot_price",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(row["CaseRetailPrice"]) / Convert.ToDouble(row["Units"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };
            calculatedColumns.Add(botPriceColumn);

            CalculatedColumn casePriceColumn = new CalculatedColumn
            {
                Name = "case_price",
                Calculation = (row, parameters) => Convert.ToDouble(row["CaseRetailPrice"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };
            calculatedColumns.Add(casePriceColumn);

            CalculatedColumn botNYCColumn = new CalculatedColumn
            {
                Name = "bot_nyc",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(botNYCColumn);

            CalculatedColumn caseNYCColumn = new CalculatedColumn
            {
                Name = "case_nyc",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(caseNYCColumn);

            CalculatedColumn fullCaseColumn = new CalculatedColumn
            {
                Name = "fullcase",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(fullCaseColumn);

            CalculatedColumn splitCharColumn = new CalculatedColumn
            {
                Name = "split_char",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(splitCharColumn);

            CalculatedColumn fobColumn = new CalculatedColumn
            {
                Name = "fob",
                Calculation = (row, parameters) => { return Convert.ToDouble(row["FOB"]); },
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };
            calculatedColumns.Add(fobColumn);

            CalculatedColumn nysWholeColumn = new CalculatedColumn
            {
                Name = "nys_whole",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(nysWholeColumn);

            CalculatedColumn nysItemColumn = new CalculatedColumn
            {
                Name = "nys_item",
                Calculation = (row, parameters) => { return Convert.ToString(row["ProductName"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(nysItemColumn);

            CalculatedColumn comboDisaColumn = new CalculatedColumn
            {
                Name = "combo_disa",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(comboDisaColumn);

            CalculatedColumn comboAsseColumn = new CalculatedColumn
            {
                Name = "combo_asse",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(comboAsseColumn);

            CalculatedColumn limAvailColumn = new CalculatedColumn
            {
                Name = "lim_avail",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(limAvailColumn);

            CalculatedColumn allocDetColumn = new CalculatedColumn
            {
                Name = "alloc_det",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(allocDetColumn);

            CalculatedColumn allocReasColumn = new CalculatedColumn
            {
                Name = "alloc_reas",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(allocReasColumn);

            CalculatedColumn nysProdColumn = new CalculatedColumn
            {
                Name = "nys_prod",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(nysProdColumn);

            CalculatedColumn discCodeColumn = new CalculatedColumn
            {
                Name = "dis_code",
                Calculation = (row, parameters) =>
                {

                    List<string> discounts = new List<string>();

                    for (int i = 1; i <= 3; i++) // Adjust the loop range based on your maximum number of discounts
                    {
                        string discountColumnName = $"PaletDiscountPercentage{i}";
                        string thresholdColumnName = $"PaletDiscountThreshold{i}";

                        if (row[discountColumnName] != DBNull.Value && row[thresholdColumnName] != DBNull.Value)
                        {
                            double discount = Convert.ToDouble(row[discountColumnName]);
                            int threshold = Convert.ToInt32(row[thresholdColumnName]);

                            string discountString = $"{discount:F2}% ON {threshold} C";
                            discounts.Add(discountString);
                        }
                    }

                    return string.Join(", ", discounts);

                },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(discCodeColumn);

            // Define the number of discount columns
            int numberOfDiscounts = 10;

            // Loop to generate the columns
            for (int i = 1; i <= numberOfDiscounts; i++)
            {
                string thresholdColumnName = $"PaletDiscountThreshold{i}";
                string percentageColumnName = $"PaletDiscountPercentage{i}";

                CalculatedColumn qtyColumn = new CalculatedColumn
                {
                    Name = $"qty_{i}",
                    Calculation = (currentRow, parameters) =>
                    {
                        if (currentRow.Table.Columns.Contains(thresholdColumnName) && currentRow[thresholdColumnName] != DBNull.Value)
                        {
                            return Convert.ToInt16(currentRow[thresholdColumnName]);
                        }
                        return "";
                    },
                    Format = "",
                    RequiresExternalValues = false
                };

                CalculatedColumn unitColumn = new CalculatedColumn
                {
                    Name = $"unit_{i}",
                    Calculation = (currentRow, parameters) =>
                    {
                        if (currentRow.Table.Columns.Contains(thresholdColumnName) && currentRow[thresholdColumnName] != DBNull.Value)
                        {
                            return "C";
                        }
                        return "";
                    },
                    Format = null,
                    RequiresExternalValues = false
                };

                CalculatedColumn amountColumn = new CalculatedColumn
                {
                    Name = $"amount_{i}",
                    Calculation = (currentRow, parameters) =>
                    {
                        if (currentRow.Table.Columns.Contains(percentageColumnName) && currentRow[percentageColumnName] != DBNull.Value)
                        {
                            return Convert.ToInt16(currentRow[percentageColumnName]);
                        }
                        return "";
                    },
                    Format = "",
                    RequiresExternalValues = false
                };

                CalculatedColumn typeColumn = new CalculatedColumn
                {
                    Name = $"type_{i}",
                    Calculation = (currentRow, parameters) =>
                    {
                        if (currentRow.Table.Columns.Contains(percentageColumnName) && currentRow[percentageColumnName] != DBNull.Value)
                        {
                            return "%";
                        }
                        return "";
                    },
                    Format = "",
                    RequiresExternalValues = false
                };

                // Add the columns to the list
                calculatedColumns.AddRange(new CalculatedColumn[] { qtyColumn, unitColumn, amountColumn, typeColumn });
            }

            // Loop to generate the columns
            for (int i = 1; i <= numberOfDiscounts; i++)
            {
                CalculatedColumn cmbItemColumn = new CalculatedColumn
                {
                    Name = $"cmbitem_{i}",
                    Calculation = (row, parameters) =>
                    {
                        return "";
                    },
                    Format = "",
                    RequiresExternalValues = false
                };

                CalculatedColumn cmbQtyColumn = new CalculatedColumn
                {
                    Name = $"cmbqty_{i}",
                    Calculation = (row, parameters) =>
                    {
                        return "";
                    },
                    Format = "",
                    RequiresExternalValues = false
                };

                // Add the columns to the list
                calculatedColumns.AddRange(new CalculatedColumn[] { cmbItemColumn, cmbQtyColumn });
            }

        }
    }
}
