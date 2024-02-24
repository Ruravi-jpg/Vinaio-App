using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace Vinaio.Reports
{
    public class MainReport : BaseReport
    {

        public MainReport()
        {
            DateTime currentDate = DateTime.Now;
            string currentMonth = currentDate.ToString("MM"); // Month in two digits
            string currentYear = currentDate.ToString("yyyy"); // Year in four digits
            reportName = $"CostosMaster_{currentMonth}-{currentYear}.xlsx";
            calculatedColumns = new List<CalculatedColumn>();


            CalculatedColumn productStatusColumn = new CalculatedColumn
            {
                Name = "Estado del producto",
                Calculation = (row, parameters) => Convert.ToString(row["ProductStatus"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn productNameColumn = new CalculatedColumn
            {
                Name = "Nombre del producto",
                Calculation = (row, parameters) => Convert.ToString(row["ProductName"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn productYearColumn = new CalculatedColumn
            {
                Name = "Año",
                Calculation = (row, parameters) => Convert.ToString(row["ProductYear"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn cellarNameColumn = new CalculatedColumn
            {
                Name = "Bodega",
                Calculation = (row, parameters) => Convert.ToString(row["CellarName"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn productTypeColumn = new CalculatedColumn
            {
                Name = "Tipo de producto",
                Calculation = (row, parameters) => Convert.ToString(row["Type"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn volumeColumn = new CalculatedColumn
            {
                Name = "Volume",
                OptionalColumnName = "Volumen",
                Calculation = (row, parameters) => Convert.ToDouble(row["Volume"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn volumeUnitColumn = new CalculatedColumn
            {
                Name = "VolumeUnit",
                OptionalColumnName = "Unidad de volumen",
                Calculation = (row, parameters) => Convert.ToString(row["VolumeUnit"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn abvColumn = new CalculatedColumn
            {
                Name = "ABV",
                OptionalColumnName = "Porcentaje de alcohol",
                Calculation = (row, parameters) =>
                {

                    return Convert.ToDouble(row["ABV"]) / 100;
                },
                Format = "0.00%",
                RequiresExternalValues = false
            };

            CalculatedColumn unitsColumn = new CalculatedColumn
            {
                Name = "Units",
                OptionalColumnName = "Unidades por caja",
                Calculation = (row, parameters) => Convert.ToInt32(row["Units"]),
                Format = null,
                RequiresExternalValues = false
            };

            CalculatedColumn bottleBaseCostColumn = new CalculatedColumn
            {
                Name = "BottleBaseCost",
                OptionalColumnName = "Costo base de la botella",
                Calculation = (row, parameters) => Convert.ToDouble(row["BottleBaseCost"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn ryColumn = new CalculatedColumn
            {
                Name = "RY",
                OptionalColumnName = "Ry",
                Calculation = (row, parameters) => Convert.ToDouble(row["RY"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn FreightColumn = new CalculatedColumn
            {
                Name = "Freight",
                OptionalColumnName = "Costo del Flete",
                Calculation = (row, parameters) => Convert.ToDouble(row["Freight"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn brColumn = new CalculatedColumn
            {
                Name = "BR",
                OptionalColumnName = "Broker",
                Calculation = (row, parameters) => Convert.ToDouble(row["BR"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn warColumn = new CalculatedColumn
            {
                Name = "WAR",
                OptionalColumnName = "Costo almacén",
                Calculation = (row, parameters) => Convert.ToDouble(row["WAR"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn caseWholesalePriceColumn = new CalculatedColumn
            {
                Name = "CaseWholesalePrice",
                OptionalColumnName = "Precio de venta mayoreo por caja",
                Calculation = (row, parameters) => Convert.ToDouble(row["CaseWholesalePrice"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn stTaxColumn = new CalculatedColumn
            {
                Name = "STTax",
                Calculation = (row, parameters) => Convert.ToDouble(row["STTax"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn caseRetailPriceColumn = new CalculatedColumn
            {
                Name = "CaseRetailPrice",
                OptionalColumnName = "Precio de venta por caja al menudeo",
                Calculation = (row, parameters) => Convert.ToDouble(row["CaseRetailPrice"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };


            CalculatedColumn paletDiscountPercentage1Column = new CalculatedColumn
            {
                Name = "PaletDiscountPercentage1",
                OptionalColumnName = "Descuento por palet 1",
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

            CalculatedColumn paletDiscountThreshold1Column = new CalculatedColumn
            {
                Name = "PaletDiscountThreshold1",
                OptionalColumnName = "Umbral de descuento por palet 1",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountThreshold1"] != DBNull.Value)
                    {
                        return Convert.ToInt16(row["PaletDiscountThreshold1"]);
                    }
                    return 0;

                },
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn paletDiscountPercentage2Column = new CalculatedColumn
            {
                Name = "PaletDiscountPercentage2",
                OptionalColumnName = "Descuento por palet 2",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountPercentage2"] != DBNull.Value)
                    {
                        return Convert.ToDouble(row["PaletDiscountPercentage2"]) / 100;
                    }
                    return 0;

                },
                Format = "0.00%",
                RequiresExternalValues = false
            };

            CalculatedColumn paletDiscountThreshold2Column = new CalculatedColumn
            {
                Name = "PaletDiscountThreshold2",
                OptionalColumnName = "Umbral de descuento por palet 2",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountThreshold2"] != DBNull.Value)
                    {
                        return Convert.ToInt16(row["PaletDiscountThreshold2"]);
                    }
                    return 0;

                },
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn paletDiscountPercentage3Column = new CalculatedColumn
            {
                Name = "PaletDiscountPercentage3",
                OptionalColumnName = "Descuento por palet 3",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountPercentage3"] != DBNull.Value)
                    {
                        return Convert.ToDouble(row["PaletDiscountPercentage3"]) / 100;
                    }
                    return 0;

                },
                Format = "0.00%",
                RequiresExternalValues = false
            };

            CalculatedColumn paletDiscountThreshold3Column = new CalculatedColumn
            {
                Name = "PaletDiscountThreshold3",
                OptionalColumnName = "Umbral de descuento por palet 3",
                Calculation = (row, parameters) =>
                {

                    if (row["PaletDiscountThreshold3"] != DBNull.Value)
                    {
                        return Convert.ToInt16(row["PaletDiscountThreshold3"]);
                    }
                    return 0;

                },
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn wholesaleCommisPercentageColumn = new CalculatedColumn
            {
                Name = "WholesaleCommisPercentage",
                OptionalColumnName = "Comisión vendeor por mayoreo",
                Calculation = (row, parameters) => Convert.ToDouble(row["WholesaleCommisPercentage"]) / 100,
                Format = "0.00%",
                RequiresExternalValues = false
            };

            CalculatedColumn retialCommisPercentageColumn = new CalculatedColumn
            {
                Name = "RetailCommisPercentage",
                OptionalColumnName = "Comisión vendeor por menudeo",
                Calculation = (row, parameters) => Convert.ToDouble(row["RetailCommisPercentage"]) / 100,
                Format = "0.00%",
                RequiresExternalValues = false
            };

            CalculatedColumn averagePriceColumn = new CalculatedColumn
            {
                Name = "AveragePrice",
                OptionalColumnName = "Precio promedio",
                Calculation = (row, parameters) => Convert.ToDouble(row["AveragePrice"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn fobColumn = new CalculatedColumn
            {
                Name = "FOB",
                OptionalColumnName = "FOB",
                Calculation = (row, parameters) => Convert.ToDouble(row["FOB"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn estructColumn = new CalculatedColumn
            {
                Name = "Estruct",
                OptionalColumnName = "Estruct",
                Calculation = (row, parameters) => Convert.ToDouble(row["Estruct"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };

            CalculatedColumn deliveryCostColumn = new CalculatedColumn
            {
                Name = "Delivery",
                OptionalColumnName = "Costo de entrega",
                Calculation = (row, parameters) => Convert.ToDouble(row["DeliveryCost"]),
                Format = "$#,##0.00",
                RequiresExternalValues = false
            };










            //require external values
            CalculatedColumn caseCostColumn = new CalculatedColumn
            {
                Name = "CaseCost",
                OptionalColumnName = "Costo por caja",
                Calculation = (row, parameters) => Convert.ToInt32(row["Units"]) * Convert.ToDouble(parameters["BottleBaseCost"]),
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn priceInEurosColumn = new CalculatedColumn
            {
                Name = "PriceInEuros",
                OptionalColumnName = "Precio en euros",
                Calculation = (row, parameters) =>
                {
                    double caseCost = Convert.ToInt32(row["Units"]) * Convert.ToDouble(parameters["BottleBaseCost"]);
                    return caseCost + Convert.ToDouble(row["FOB"]);
                },
                Format = "€#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn priceInDollarsColumn = new CalculatedColumn
            {
                Name = "PriceInDollars",
                OptionalColumnName = "Precio en dólares",
                Calculation = (row, parameters) => (Convert.ToInt32(row["Units"]) * Convert.ToDouble(parameters["BottleBaseCost"]) + Convert.ToDouble(row["FOB"])) * 1.15,
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn fedTaxColumn = new CalculatedColumn
            {
                Name = "FedTax",
                OptionalColumnName = "Impuesto federal",
                Calculation = (row, parameters) => Convert.ToInt32(row["Units"]) * 0.21165,
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn WholsalerCaseCostColumn = new CalculatedColumn
            {
                Name = "WholesalerCaseCost",
                OptionalColumnName = "Costo de caja al mayoreo",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["PriceInDollars"]) +
                    Convert.ToDouble(row["RY"]) +
                    Convert.ToDouble(row["Freight"]) +
                    Convert.ToDouble(row["BR"]) +
                    Convert.ToDouble(parameters["FedTax"]) +
                    Convert.ToDouble(row["WAR"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleBottleCostColumn = new CalculatedColumn
            {
                Name = "WholesaleBottleCost",
                OptionalColumnName = "Costo de botella al mayoreo",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["WholesalerCaseCost"]) / Convert.ToInt32(row["Units"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleBottlePriceColumn = new CalculatedColumn
            {
                Name = "WholesaleBottlePrice",
                OptionalColumnName = "Precio por botella al mayoreo",
                Calculation = (row, parameters) => Convert.ToDouble(row["CaseWholesalePrice"]) / Convert.ToInt32(row["Units"]),
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesalePaletDiscount1TotalColumn = new CalculatedColumn
            {
                Name = "WholesalePaletDiscountTotal1",
                OptionalColumnName = "Precio Total palet al mayoreo (descuento 1)",
                Calculation = (row, parameters) =>
                {
                    double caseRetailPrice = Convert.ToDouble(row["CaseWholesalePrice"]);

                    if (row["PaletDiscountPercentage1"] != DBNull.Value)
                    {
                        double paletDiscountPercentage = Convert.ToDouble(row["PaletDiscountPercentage1"]);
                        return caseRetailPrice - caseRetailPrice * paletDiscountPercentage / 100;
                    }
                    else
                    {
                        return 0;
                    }

                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesalePaletDiscount2TotalColumn = new CalculatedColumn
            {
                Name = "WholesalePaletDiscountTotal2",
                OptionalColumnName = "Precio Total palet al mayoreo (descuento 2)",
                Calculation = (row, parameters) =>
                {
                    double caseRetailPrice = Convert.ToDouble(row["CaseWholesalePrice"]);

                    if (row["PaletDiscountPercentage2"] != DBNull.Value)
                    {
                        double paletDiscountPercentage = Convert.ToDouble(row["PaletDiscountPercentage2"]);
                        return caseRetailPrice - caseRetailPrice * paletDiscountPercentage / 100;
                    }
                    else
                    {
                        return 0;
                    }
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesalePaletDiscount3TotalColumn = new CalculatedColumn
            {
                Name = "WholesalePaletDiscountTotal3",
                OptionalColumnName = "Precio Total palet al mayoreo (descuento 3)",
                Calculation = (row, parameters) =>
                {
                    double caseRetailPrice = Convert.ToDouble(row["CaseWholesalePrice"]);
                    if (row["PaletDiscountPercentage3"] != DBNull.Value)
                    {
                        double paletDiscountPercentage = Convert.ToDouble(row["PaletDiscountPercentage3"]);
                        return caseRetailPrice - caseRetailPrice * paletDiscountPercentage / 100;
                    }
                    else
                    {
                        return 0;
                    }
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleBot1Column = new CalculatedColumn
            {
                Name = "WholesaleBOT1",
                OptionalColumnName = "Precio por botella al mayoreo (descuento 1)",
                Calculation = (row, parameters) =>
                {
                    double paletDiscountTotal = Convert.ToDouble(parameters["WholesalePaletDiscountTotal1"]);

                    if (paletDiscountTotal != 0)
                    {
                        return paletDiscountTotal / Convert.ToInt32(row["Units"]);
                    }
                    else
                    {
                        return 0;
                    }
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleBot2Column = new CalculatedColumn
            {
                Name = "WholesaleBOT2",
                OptionalColumnName = "Precio por botella al mayoreo (descuento 2)",
                Calculation = (row, parameters) =>
                {
                    double paletDiscountTotal = Convert.ToDouble(parameters["WholesalePaletDiscountTotal2"]);

                    if (paletDiscountTotal != 0)
                    {
                        return paletDiscountTotal / Convert.ToInt32(row["Units"]);
                    }
                    else
                    {
                        return 0;
                    }
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleBot3Column = new CalculatedColumn
            {
                Name = "WholesaleBOT3",
                OptionalColumnName = "Precio por botella al mayoreo (descuento 3)",
                Calculation = (row, parameters) =>
                {
                    double paletDiscountTotal = Convert.ToDouble(parameters["WholesalePaletDiscountTotal3"]);

                    if (paletDiscountTotal != 0)
                    {
                        return paletDiscountTotal / Convert.ToInt32(row["Units"]);
                    }
                    else
                    {
                        return 0;
                    }
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleGrossProfit1TotalColumn = new CalculatedColumn
            {
                Name = "WholesaleGrossProfitTotal1",
                OptionalColumnName = "Ganancia bruta total al mayoreo (descuento 1)",
                Calculation = (row, parameters) =>
                {
                    double PaletDiscountTotal1 = Convert.ToDouble(parameters["WholesalePaletDiscountTotal1"]);

                    if (row["PaletDiscountPercentage1"] != DBNull.Value)
                    {
                        return Convert.ToDouble(parameters["WholesaleCommisTotal1"]) - Convert.ToDouble(parameters["WholesalerCaseCost"]);
                    }
                    else
                    {
                        return 0;
                    }

                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleGrossProfit2TotalColumn = new CalculatedColumn
            {
                Name = "WholesaleGrossProfitTotal2",
                OptionalColumnName = "Ganancia bruta total al mayoreo (descuento 2)",
                Calculation = (row, parameters) =>
                {
                    double PaletDiscountTotal1 = Convert.ToDouble(parameters["WholesalePaletDiscountTotal2"]);

                    if (row["PaletDiscountPercentage2"] != DBNull.Value)
                    {
                        return Convert.ToDouble(parameters["WholesaleCommisTotal2"]) - Convert.ToDouble(parameters["WholesalerCaseCost"]);
                    }
                    else
                    {
                        return 0;
                    }

                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleGrossProfit3TotalColumn = new CalculatedColumn
            {
                Name = "WholesaleGrossProfitTotal3",
                OptionalColumnName = "Ganancia bruta total al mayoreo (descuento 3)",
                Calculation = (row, parameters) =>
                {
                    double PaletDiscountTotal1 = Convert.ToDouble(parameters["WholesalePaletDiscountTotal3"]);

                    if (row["PaletDiscountPercentage3"] != DBNull.Value)
                    {
                        return Convert.ToDouble(parameters["WholesaleCommisTotal3"]) - Convert.ToDouble(parameters["WholesalerCaseCost"]);
                    }
                    else
                    {
                        return 0;
                    }

                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleNetProfit1Column = new CalculatedColumn
            {
                Name = "WholesaleNetProfit1",
                OptionalColumnName = "Ganancia neta al mayoreo (descuento 1)",
                Calculation = (row, parameters) =>
                {
                    if (parameters["WholesaleGrossProfitTotal1"] != DBNull.Value)
                    {
                        return Convert.ToDouble(parameters["WholesaleGrossProfitTotal1"]) - Convert.ToDouble(row["Estruct"]);
                    }
                    else
                    {
                        return 0;
                    }

                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleNetProfit2Column = new CalculatedColumn
            {
                Name = "WholesaleNetProfit2",
                OptionalColumnName = "Ganancia neta al mayoreo (descuento 2)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["WholesaleGrossProfitTotal2"]) - Convert.ToDouble(row["Estruct"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleNetProfit3Column = new CalculatedColumn
            {
                Name = "WholesaleNetProfit3",
                OptionalColumnName = "Ganancia neta al mayoreo (descuento 3)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["WholesaleGrossProfitTotal3"]) - Convert.ToDouble(row["Estruct"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            ///******************
            CalculatedColumn wholesaleCommisTotalBaseColumn = new CalculatedColumn
            {
                Name = "WholesaleCommisTotalBase",
                OptionalColumnName = "Precio total menos Comisión vendedor al mayoreo (base)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["WholesalerCaseCost"]) * (1 - Convert.ToDouble(row["WholesaleCommisPercentage"]) / 100);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleCommisTotal1Column = new CalculatedColumn
            {
                Name = "WholesaleCommisTotal1",
                OptionalColumnName = "Precio total menos Comisión vendedor al mayoreo (descuento 1)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["WholesalePaletDiscountTotal1"]) * (1 - Convert.ToDouble(row["WholesaleCommisPercentage"]) / 100);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleCommisTotal2Column = new CalculatedColumn
            {
                Name = "WholesaleCommisTotal2",
                OptionalColumnName = "Precio total menos Comisión vendedor al mayoreo (descuento 2)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["WholesalePaletDiscountTotal2"]) * (1 - Convert.ToDouble(row["WholesaleCommisPercentage"]) / 100);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleCommisTotal3Column = new CalculatedColumn
            {
                Name = "WholesaleCommisTotal3",
                OptionalColumnName = "Precio total menos Comisión vendedor al mayoreo (descuento 3)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["WholesalePaletDiscountTotal3"]) * (1 - Convert.ToDouble(row["WholesaleCommisPercentage"]) / 100);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleGrossProfitBaseColumn = new CalculatedColumn
            {
                Name = "WholesaleGrossProfitBase",
                OptionalColumnName = "Ganancia bruta total al mayoreo (base)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["WholesaleCommisTotalBase"]) - Convert.ToDouble(parameters["WholesalerCaseCost"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn wholesaleNetProfitBaseColumn = new CalculatedColumn
            {
                Name = "WholesaleNetProfitBase",
                OptionalColumnName = "Ganancia neta total al mayoreo (base)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["WholesaleGrossProfitBase"]) - Convert.ToDouble(row["Estruct"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            //Retail
            CalculatedColumn retailCaseCostColumn = new CalculatedColumn
            {
                Name = "RetailCaseCost",
                OptionalColumnName = "Costo por caja al menudeo",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["WholesalerCaseCost"]) + Convert.ToDouble(row["STTax"]) + Convert.ToDouble(row["DeliveryCost"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailBottleCostColumn = new CalculatedColumn
            {
                Name = "RetailBottleCost",
                OptionalColumnName = "Costo por botella al menudeo",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["RetailCaseCost"]) / Convert.ToDouble(row["Units"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailBottlePriceColumn = new CalculatedColumn
            {
                Name = "RetailBottlePrice",
                OptionalColumnName = "Precio por botella al menudeo",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["CaseRetailPrice"]) / Convert.ToDouble(row["Units"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailPaletDiscount1TotalColumn = new CalculatedColumn
            {
                Name = "RetailPaletDiscountTotal1",
                OptionalColumnName = "Precio total palet al menudeo (descuento 1)",
                Calculation = (row, parameters) =>
                {
                    double caseRetailPrice = Convert.ToDouble(row["CaseRetailPrice"]);

                    if (row["PaletDiscountPercentage1"] != DBNull.Value)
                    {
                        double paletDiscountPercentage = Convert.ToDouble(row["PaletDiscountPercentage1"]);
                        return caseRetailPrice - caseRetailPrice * paletDiscountPercentage / 100;
                    }
                    else
                    {
                        return 0;
                    }

                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailPaletDiscount2TotalColumn = new CalculatedColumn
            {
                Name = "RetailPaletDiscountTotal2",
                OptionalColumnName = "Precio total palet al menudeo (descuento 2)",
                Calculation = (row, parameters) =>
                {
                    double caseRetailPrice = Convert.ToDouble(row["CaseRetailPrice"]);

                    if (row["PaletDiscountPercentage2"] != DBNull.Value)
                    {
                        double paletDiscountPercentage = Convert.ToDouble(row["PaletDiscountPercentage2"]);
                        return caseRetailPrice - caseRetailPrice * paletDiscountPercentage / 100;
                    }
                    else
                    {
                        return 0;
                    }
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailPaletDiscount3TotalColumn = new CalculatedColumn
            {
                Name = "RetailPaletDiscountTotal3",
                OptionalColumnName = "Precio total palet al menudeo (descuento 3)",
                Calculation = (row, parameters) =>
                {
                    double caseRetailPrice = Convert.ToDouble(row["CaseRetailPrice"]);
                    if (row["PaletDiscountPercentage3"] != DBNull.Value)
                    {
                        double paletDiscountPercentage = Convert.ToDouble(row["PaletDiscountPercentage3"]);
                        return caseRetailPrice - caseRetailPrice * paletDiscountPercentage / 100;
                    }
                    else
                    {
                        return 0;
                    }
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };


            //**************************************************************


            CalculatedColumn retialCommisTotal1Column = new CalculatedColumn
            {
                Name = "RetailCommisTotal1",
                OptionalColumnName = "Precio total menos Comisión vendedor al menudeo (descuento 1)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["RetailPaletDiscountTotal1"]) * (1 - Convert.ToDouble(row["RetailCommisPercentage"]) / 100);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retialCommisTotal2Column = new CalculatedColumn
            {
                Name = "RetailCommisTotal2",
                OptionalColumnName = "Precio total menos Comisión vendedor al menudeo (descuento 2)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["RetailPaletDiscountTotal2"]) * (1 - Convert.ToDouble(row["RetailCommisPercentage"]) / 100);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retialCommisTotal3Column = new CalculatedColumn
            {
                Name = "RetailCommisTotal3",
                OptionalColumnName = "Precio total menos Comisión vendedor al menudeo (descuento 3)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["RetailPaletDiscountTotal3"]) * (1 - Convert.ToDouble(row["RetailCommisPercentage"]) / 100);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };



            CalculatedColumn retailGrossProfit1TotalColumn = new CalculatedColumn
            {
                Name = "RetailGrossProfitTotal1",
                OptionalColumnName = "Ganancia bruta total al menudeo (descuento 1)",
                Calculation = (row, parameters) =>
                {
                    double PaletDiscountTotal1 = Convert.ToDouble(parameters["RetailPaletDiscountTotal1"]);

                    if (row["PaletDiscountPercentage1"] != DBNull.Value)
                    {
                        return Convert.ToDouble(parameters["RetailCommisTotal1"]) - Convert.ToDouble(parameters["RetailCaseCost"]);
                    }
                    else
                    {
                        return 0;
                    }

                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailGrossProfit2TotalColumn = new CalculatedColumn
            {
                Name = "RetailGrossProfitTotal2",
                OptionalColumnName = "Ganancia bruta total al menudeo (descuento 2)",
                Calculation = (row, parameters) =>
                {
                    double PaletDiscountTotal1 = Convert.ToDouble(parameters["RetailPaletDiscountTotal2"]);

                    if (row["PaletDiscountPercentage2"] != DBNull.Value)
                    {
                        return Convert.ToDouble(parameters["RetailCommisTotal2"]) - Convert.ToDouble(parameters["RetailCaseCost"]);
                    }
                    else
                    {
                        return 0;
                    }

                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailGrossProfit3TotalColumn = new CalculatedColumn
            {
                Name = "RetailGrossProfitTotal3",
                OptionalColumnName = "Ganancia bruta total al menudeo (desceunto 3)",
                Calculation = (row, parameters) =>
                {
                    double PaletDiscountTotal1 = Convert.ToDouble(parameters["RetailPaletDiscountTotal3"]);

                    if (row["PaletDiscountPercentage3"] != DBNull.Value)
                    {
                        return Convert.ToDouble(parameters["RetailCommisTotal3"]) - Convert.ToDouble(parameters["RetailCaseCost"]);
                    }
                    else
                    {
                        return 0;
                    }

                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailNetProfit1Column = new CalculatedColumn
            {
                Name = "RetailNetProfit1",
                OptionalColumnName = "Ganancia neta al menudeo (descuento 1)",
                Calculation = (row, parameters) =>
                {
                    if (parameters["RetailGrossProfitTotal1"] != DBNull.Value)
                    {
                        return Convert.ToDouble(parameters["RetailGrossProfitTotal1"]) - Convert.ToDouble(row["Estruct"]);
                    }
                    else
                    {
                        return 0;
                    }

                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailNetProfit2Column = new CalculatedColumn
            {
                Name = "RetailNetProfit2",
                OptionalColumnName = "Ganancia neta al menudeo (desceunto 2)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["RetailGrossProfitTotal2"]) - Convert.ToDouble(row["Estruct"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailNetProfit3Column = new CalculatedColumn
            {
                Name = "RetailNetProfit3",
                OptionalColumnName = "Ganancia neta al menudeo (descuento 3)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["RetailGrossProfitTotal3"]) - Convert.ToDouble(row["Estruct"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };


            //**************************************************************

            CalculatedColumn retailBot1Column = new CalculatedColumn
            {
                Name = "RetailBOT1",
                OptionalColumnName = "Precio por botella al menudeo (descuento 1)",
                Calculation = (row, parameters) =>
                {
                    double paletDiscountTotal = Convert.ToDouble(parameters["RetailPaletDiscountTotal1"]);

                    if (paletDiscountTotal != 0)
                    {
                        return paletDiscountTotal / Convert.ToInt32(row["Units"]);
                    }
                    else
                    {
                        return 0;
                    }
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailBot2Column = new CalculatedColumn
            {
                Name = "RetailBOT2",
                OptionalColumnName = "Precio por botella al menudeo (descuento 2)",
                Calculation = (row, parameters) =>
                {
                    double paletDiscountTotal = Convert.ToDouble(parameters["RetailPaletDiscountTotal2"]);

                    if (paletDiscountTotal != 0)
                    {
                        return paletDiscountTotal / Convert.ToInt32(row["Units"]);
                    }
                    else
                    {
                        return 0;
                    }
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailBot3Column = new CalculatedColumn
            {
                Name = "RetailBOT3",
                OptionalColumnName = "Precio por botella al menudeo (descuento 3)",
                Calculation = (row, parameters) =>
                {
                    double paletDiscountTotal = Convert.ToDouble(parameters["RetailPaletDiscountTotal3"]);

                    if (paletDiscountTotal != 0)
                    {
                        return paletDiscountTotal / Convert.ToInt32(row["Units"]);
                    }
                    else
                    {
                        return 0;
                    }
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailCommisTotalColumn = new CalculatedColumn
            {
                Name = "RetailCommisTotal",
                OptionalColumnName = "Precio total menos Comisión vendedor al menudeo (base)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(row["AveragePrice"]) * (1 - Convert.ToDouble(row["WholesaleCommisPercentage"]) / 100);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn ppayColumn = new CalculatedColumn
            {
                Name = "PPAYTotal",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(row["CaseRetailPrice"]) * (Convert.ToDouble(row["PPAYPercentage"]) / 100);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retailGrossProfitColumn = new CalculatedColumn
            {
                Name = "RetailGrossProfit",
                OptionalColumnName = "Ganancia bruta total al menudeo (base)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["RetailCaseCost"]) - Convert.ToDouble(parameters["RetailCommisTotal"]) - Convert.ToDouble(row["PPAYPercentage"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };

            CalculatedColumn retialNetProfitColumn = new CalculatedColumn
            {
                Name = "RetailNetProfit",
                OptionalColumnName = "Ganancia neta total al menudeo (base)",
                Calculation = (row, parameters) =>
                {
                    return Convert.ToDouble(parameters["RetailGrossProfit"]) - Convert.ToDouble(row["Estruct"]);
                },
                Format = "$#,##0.00",
                RequiresExternalValues = true
            };


            //extra values
            CalculatedColumn bodegaQuickbooks = new CalculatedColumn
            {
                Name = "Bodega en quickbooks",
                Calculation = (row, parameters) => Convert.ToString(row["BodegaQuickbooks"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn nombreQuickbooks = new CalculatedColumn
            {
                Name = "Nombre en quickbooks",
                Calculation = (row, parameters) => Convert.ToString(row["NombreQuickbooks"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn codigoQuickbooks = new CalculatedColumn
            {
                Name = "Código en quickbooks",
                Calculation = (row, parameters) => Convert.ToString(row["CodigoQuickbooks"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn nombreScribebase = new CalculatedColumn
            {
                Name = "Nombre en Scribebase",
                Calculation = (row, parameters) => Convert.ToString(row["NombreScribebase"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn codigoScribebase = new CalculatedColumn
            {
                Name = "Código en Scribebase",
                Calculation = (row, parameters) => Convert.ToString(row["CodigoScribebase"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn bodegaProviSevenfifty = new CalculatedColumn
            {
                Name = "Bodega en Provi-Sevenfifty",
                Calculation = (row, parameters) => Convert.ToString(row["BodegaProviSevenfifty"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn codigoProviSevenfifty = new CalculatedColumn
            {
                Name = "Código en Provi-Sevenfifty",
                Calculation = (row, parameters) => Convert.ToString(row["CodigoProviSevenfifty"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn nombrePricepostNJ = new CalculatedColumn
            {
                Name = "Nombre en Price Post NJ",
                Calculation = (row, parameters) => Convert.ToString(row["NombrePricePostNJ"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn codigoPricepostNJ = new CalculatedColumn
            {
                Name = "Código en Price Post NJ",
                Calculation = (row, parameters) => Convert.ToString(row["CodigoPricePostNJ"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn nombrePricepostNY = new CalculatedColumn
            {
                Name = "Nombre en Price Post NY",
                Calculation = (row, parameters) => Convert.ToString(row["NombrePricePostNY"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn codigoPricepostNY = new CalculatedColumn
            {
                Name = "Código en Price Post NY",
                Calculation = (row, parameters) => Convert.ToString(row["CodigoPricePostNY"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn codigoProvi = new CalculatedColumn
            {
                Name = "Código en Provi",
                Calculation = (row, parameters) => Convert.ToString(row["CodigoProvi"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn nombreCatalogo = new CalculatedColumn
            {
                Name = "Nombre en Catálogo",
                Calculation = (row, parameters) => Convert.ToString(row["NombreCatalogo"]),
                Format = "",
                RequiresExternalValues = false
            };

            CalculatedColumn paginaCatalogo = new CalculatedColumn
            {
                Name = "Página en Catálogo",
                Calculation = (row, parameters) =>
                {

                    return row["PaginaCatalogo"] != DBNull.Value ? Convert.ToInt16(row["PaginaCatalogo"]) : "";
                },
                Format = "",
                RequiresExternalValues = false
            };


            //empty column for separation
            CalculatedColumn emptyColumn = new CalculatedColumn
            {
                Name = "",
                Calculation = (row, parameters) => "", // No actual calculation needed
                Format = "", // You can leave it empty or set it to a default format
            };



            // Add the columns to the list
            calculatedColumns.AddRange(new CalculatedColumn[]
            {
                productStatusColumn,
                productNameColumn,
                productYearColumn,
                cellarNameColumn,
                productTypeColumn,
                volumeColumn,
                volumeUnitColumn,
                abvColumn,
                unitsColumn,
                bottleBaseCostColumn,
                caseCostColumn,
                fobColumn,
                priceInEurosColumn,
                priceInDollarsColumn,
                ryColumn,
                FreightColumn,
                brColumn,
                fedTaxColumn,
                warColumn,
                WholsalerCaseCostColumn,
                wholesaleBottleCostColumn,
                wholesaleBottlePriceColumn,
                caseWholesalePriceColumn,

                paletDiscountPercentage1Column,
                paletDiscountThreshold1Column,
                wholesalePaletDiscount1TotalColumn,
                wholesaleCommisTotal1Column,
                wholesaleBot1Column,
                wholesaleGrossProfit1TotalColumn,
                wholesaleNetProfit1Column,

                paletDiscountPercentage2Column,
                paletDiscountThreshold2Column,
                wholesalePaletDiscount2TotalColumn,
                wholesaleCommisTotal2Column,
                wholesaleBot2Column,
                wholesaleGrossProfit2TotalColumn,
                wholesaleNetProfit2Column,

                paletDiscountPercentage3Column,
                paletDiscountThreshold3Column,
                wholesalePaletDiscount3TotalColumn,
                wholesaleCommisTotal3Column,
                wholesaleBot3Column,
                wholesaleGrossProfit3TotalColumn,
                wholesaleNetProfit3Column,

                wholesaleCommisPercentageColumn,
                wholesaleCommisTotalBaseColumn,
                wholesaleGrossProfitBaseColumn,
                wholesaleNetProfitBaseColumn,


                emptyColumn,


                stTaxColumn,
                deliveryCostColumn,
                retailCaseCostColumn,
                retailBottleCostColumn,
                retailBottlePriceColumn,
                caseRetailPriceColumn,
                averagePriceColumn,

                paletDiscountPercentage1Column,
                retailPaletDiscount1TotalColumn,
                paletDiscountThreshold1Column,
                retialCommisTotal1Column,
                retailBot1Column,
                retailGrossProfit1TotalColumn,
                retailNetProfit1Column,

                paletDiscountPercentage2Column,
                paletDiscountThreshold2Column,
                retailPaletDiscount2TotalColumn,
                retialCommisTotal2Column,
                retailBot2Column,
                retailGrossProfit2TotalColumn,
                retailNetProfit2Column,

                paletDiscountPercentage3Column,
                paletDiscountThreshold3Column,
                retailPaletDiscount3TotalColumn,
                retialCommisTotal3Column,
                retailBot3Column,
                retailGrossProfit3TotalColumn,
                retailNetProfit3Column,

                retailCommisTotalColumn,
                ppayColumn,
                retailGrossProfitColumn,
                retialNetProfitColumn,

                emptyColumn,

                bodegaQuickbooks,
                nombreQuickbooks,
                codigoQuickbooks,
                nombreScribebase,
                codigoScribebase,
                bodegaProviSevenfifty,
                codigoProviSevenfifty,
                nombrePricepostNJ,
                codigoPricepostNJ,
                nombrePricepostNY,
                codigoPricepostNY,
                codigoProvi,
                nombreCatalogo,
                paginaCatalogo

            });
        }


    }
}
