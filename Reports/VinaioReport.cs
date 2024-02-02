using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinaio.Reports
{
    internal class VinaioReport : BaseReport
    {
        public VinaioReport()
        {
            reportName = $"VinaioReport.xlsx";
            calculatedColumns = new List<CalculatedColumn>();


            CalculatedColumn skuColumn = new CalculatedColumn
            {
                Name = "SKU",
                Calculation = (row, parameters) => { return Convert.ToString(row["SKU"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(skuColumn);

            CalculatedColumn statusColumn = new CalculatedColumn
            {
                Name = "Status",
                Calculation = (row, parameters) => { return Convert.ToString(row["ProductStatus"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(statusColumn);

            CalculatedColumn producerNameColumn = new CalculatedColumn
            {
                Name = "Producer Name",
                Calculation = (row, parameters) => { return Convert.ToString(row["CellarName"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(producerNameColumn);

            CalculatedColumn productNameColumn = new CalculatedColumn
            {
                Name = "Product Name",
                Calculation = (row, parameters) => { return Convert.ToString(row["ProductName"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(producerNameColumn);

            CalculatedColumn apellationColumn = new CalculatedColumn
            {
                Name = "Apellation",
                Calculation = (row, parameters) => { return Convert.ToString(row["Apellation"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(apellationColumn);

            CalculatedColumn supplierColumn = new CalculatedColumn
            {
                Name = "Supplier",
                Calculation = (row, parameters) => { return Convert.ToString(row["Supplier"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(supplierColumn);

            CalculatedColumn vintageColumn = new CalculatedColumn
            {
                Name = "Vintage",
                Calculation = (row, parameters) =>
                {

                    if (row["YearAdded"] == DBNull.Value)
                    {
                        return "NV";
                    }
                    else
                    {
                        return Convert.ToInt16(row["YearAdded"]);
                    }
                },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(vintageColumn);

            CalculatedColumn sizeColumn = new CalculatedColumn
            {
                Name = "Size",
                Calculation = (row, parameters) => { return Convert.ToInt16(row["Volume"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(sizeColumn);

            CalculatedColumn sizeUnitColumn = new CalculatedColumn
            {
                Name = "Size Unit",
                Calculation = (row, parameters) => { return Convert.ToString(row["VolumeUnit"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(sizeUnitColumn);

            CalculatedColumn caseSizeColumn = new CalculatedColumn
            {
                Name = "Case Size",
                Calculation = (row, parameters) => { return Convert.ToInt16(row["Units"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(caseSizeColumn);

            CalculatedColumn sleeveSizeColumn = new CalculatedColumn
            {
                Name = "Sleeve Size",
                Calculation = (row, parameters) => { return ""; },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(sleeveSizeColumn);

            CalculatedColumn containerTypeColumn = new CalculatedColumn
            {
                Name = "Container Type",
                Calculation = (row, parameters) => { return Convert.ToString(row["ContainerType"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(containerTypeColumn);

            CalculatedColumn upcColumn = new CalculatedColumn
            {
                Name = "UPC",
                Calculation = (row, parameters) => { return Convert.ToString(row["UPCCode"]); },
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(upcColumn);

            CalculatedColumn priceEffectivedateColumn = new CalculatedColumn
            {
                Name = "Price Effective Date",
                Calculation = (row, parameters) => DateTime.Now.AddMonths(2).Date.AddDays(-DateTime.Now.AddMonths(2).Day).ToString("yyyy/mm/dd"),
                Format = null,
                RequiresExternalValues = false
            };
            calculatedColumns.Add(priceEffectivedateColumn);

            CalculatedColumn frontlineUnitPriceColumn = new CalculatedColumn
            {
                Name = "Frontline bottle price",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(row["CaseRetailPrice"]) / Convert.ToDouble(row["Units"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };
            calculatedColumns.Add(frontlineUnitPriceColumn);

            CalculatedColumn frontlineCasePriceColumn = new CalculatedColumn
            {
                Name = "Frontline case price",
                Calculation = (row, parameters) => Convert.ToDouble(row["CaseRetailPrice"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };
            calculatedColumns.Add(frontlineCasePriceColumn);


            int numberOfDiscounts = 3;
            for (int i = 1; i <= numberOfDiscounts; i++)
            {
                string thresholdColumnName = $"PaletDiscountThreshold{i}";
                string percentageColumnName = $"PaletDiscountPercentage{i}";

                CalculatedColumn levelPrice = new CalculatedColumn
                {
                    Name = $"Level {i} Price",
                    Calculation = (currentRow, parameters) =>
                    {
                        if (currentRow.Table.Columns.Contains(thresholdColumnName) && currentRow[thresholdColumnName] != DBNull.Value)
                        {
                            double price = Convert.ToDouble(currentRow["CaseRetailPrice"]);
                            double discount = Convert.ToDouble(currentRow[percentageColumnName]);
                            double discountPrice = price - price * discount / 100;
                            return discountPrice;
                        }
                        return "";
                    },
                    Format = "$#,##0.00",
                    RequiresExternalValues = false
                };

                CalculatedColumn levelQuantity = new CalculatedColumn
                {
                    Name = $"Level {i} Quantity",
                    Calculation = (currentRow, parameters) =>
                    {
                        if (currentRow.Table.Columns.Contains(thresholdColumnName) && currentRow[thresholdColumnName] != DBNull.Value)
                        {
                            return Convert.ToInt16(currentRow[thresholdColumnName]);
                        }
                        return "";
                    },
                    Format = null,
                    RequiresExternalValues = false
                };

                // Add the columns to the list
                calculatedColumns.AddRange(new CalculatedColumn[] { levelPrice, levelQuantity });
            }
        }
    }
}
