using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinaio
{
    internal class ColumnMappingData
    {
       static public Dictionary<string, ColumnMapping> columnMappings = new Dictionary<string, ColumnMapping>
{
    { "ID", new ColumnMapping { DatabaseColumnName = "ID", AppColumnName = "ID"} },
    { "ProductStatus", new ColumnMapping { DatabaseColumnName = "ProductStatus", AppColumnName = "Estado del Producto", ShowColumn = false } },
    { "CellarName", new ColumnMapping { DatabaseColumnName = "CellarName", AppColumnName = "Nombre de Bodega" } },
    { "ProductName", new ColumnMapping { DatabaseColumnName = "ProductName", AppColumnName = "Nombre del Producto" } },
    { "ProductYear", new ColumnMapping { DatabaseColumnName = "ProductYear", AppColumnName = "Año del Producto" } },
    { "Volume", new ColumnMapping { DatabaseColumnName = "Volume", AppColumnName = "Volumen" } },
    { "VolumeUnit", new ColumnMapping { DatabaseColumnName = "VolumeUnit", AppColumnName = "Unidad de Volumen" } },
    { "ABV", new ColumnMapping { DatabaseColumnName = "ABV", AppColumnName = "Grado Alcoholico" } },
    { "Units", new ColumnMapping { DatabaseColumnName = "Units", AppColumnName = "Unidades" } },
    { "BottleBaseCost", new ColumnMapping { DatabaseColumnName = "BottleBaseCost", AppColumnName = "Costo por Botella Base" } },
    { "FOB", new ColumnMapping { DatabaseColumnName = "FOB", AppColumnName = "FOB" } },
    { "Freight", new ColumnMapping { DatabaseColumnName = "Freight", AppColumnName = "Flete" } },
    { "RY", new ColumnMapping { DatabaseColumnName = "RY", AppColumnName = "RY" } },
    { "BR", new ColumnMapping { DatabaseColumnName = "BR", AppColumnName = "BR" } },
    { "WAR", new ColumnMapping { DatabaseColumnName = "WAR", AppColumnName = "WAR" } },
    { "CaseWholesalePrice", new ColumnMapping { DatabaseColumnName = "CaseWholesalePrice", AppColumnName = "Precio para Mayorista por Caja" } },
    { "PaletDiscountPercentage1", new ColumnMapping { DatabaseColumnName = "PaletDiscountPercentage1", AppColumnName = "Porcentaje de Descuento por Palet 1" } },
    { "PaletDiscountThreshold1", new ColumnMapping { DatabaseColumnName = "PaletDiscountThreshold1", AppColumnName = "Umbral de descuento por palet 1" } },
    { "PaletDiscountPercentage2", new ColumnMapping { DatabaseColumnName = "PaletDiscountPercentage2", AppColumnName = "Porcentaje de Descuento por Palet 2" } },
    { "PaletDiscountThreshold2", new ColumnMapping { DatabaseColumnName = "PaletDiscountThreshold2", AppColumnName = "Umbral de descuento por palet 2" } },
    { "PaletDiscountPercentage3", new ColumnMapping { DatabaseColumnName = "PaletDiscountPercentage3", AppColumnName = "Porcentaje de Descuento por Palet 3" } },
    { "PaletDiscountThreshold3", new ColumnMapping { DatabaseColumnName = "PaletDiscountThreshold3", AppColumnName = "Umbral de descuento por palet 3" } },
    { "WholesaleCommisPercentage", new ColumnMapping { DatabaseColumnName = "WholesaleCommisPercentage", AppColumnName = "Porcentaje de Comisión por mayorista" } },
    { "Estruct", new ColumnMapping { DatabaseColumnName = "Estruct", AppColumnName = "Estruct" } },
    { "STTax", new ColumnMapping { DatabaseColumnName = "STTax", AppColumnName = "STTax" } },
    { "DeliveryCost", new ColumnMapping { DatabaseColumnName = "DeliveryCost", AppColumnName = "Costo por Entrega" } },
    { "CaseRetailPrice", new ColumnMapping { DatabaseColumnName = "CaseRetailPrice", AppColumnName = "Precio Menudeo por Caja" } },
    { "AveragePrice", new ColumnMapping { DatabaseColumnName = "AveragePrice", AppColumnName = "Precio Promedio" } },
    { "RetailCommisPercentage", new ColumnMapping { DatabaseColumnName = "RetailCommisPercentage", AppColumnName = "Porcentaje de Comision Menudeo" } },
    { "PPAYPercentage", new ColumnMapping { DatabaseColumnName = "PPAYPercentage", AppColumnName = "PorcentajePPAY" } },
    { "UPCCode", new ColumnMapping { DatabaseColumnName = "UPCCode", AppColumnName = "Codigo UPC" } },
    { "BRNumber", new ColumnMapping { DatabaseColumnName = "BRNumber", AppColumnName = "NumeroBR" } },
    { "Type", new ColumnMapping { DatabaseColumnName = "Type", AppColumnName = "Tipo" } },
    { "DistributorId", new ColumnMapping { DatabaseColumnName = "DistributorId", AppColumnName = "Id Distribuidor" } },
    { "WholesalerCode", new ColumnMapping { DatabaseColumnName = "WholesalerCode", AppColumnName = "Codigo Mayorista" } },
    { "TTBID", new ColumnMapping { DatabaseColumnName = "TTBID", AppColumnName = "TTBID" } },
    { "ProductClass", new ColumnMapping { DatabaseColumnName = "ProductClass", AppColumnName = "Clase Producto" } },
    { "SKU", new ColumnMapping { DatabaseColumnName = "SKU", AppColumnName = "SKU" } },
    { "Apellation", new ColumnMapping { DatabaseColumnName = "Apellation", AppColumnName = "Denominacion" } },
    { "Supplier", new ColumnMapping { DatabaseColumnName = "Supplier", AppColumnName = "Proveedor" } },
    { "SleeveSize", new ColumnMapping { DatabaseColumnName = "SleeveSize", AppColumnName = "Tamaño Sleeve" } },
    { "ContainerType", new ColumnMapping { DatabaseColumnName = "ContainerType", AppColumnName = "Tipo de Contenedor" } },
    { "YearAdded", new ColumnMapping { DatabaseColumnName = "YearAdded", AppColumnName = "Año Añadido" } },
    { "BodegaQuickbooks", new ColumnMapping { DatabaseColumnName = "BodegaQuickbooks", AppColumnName = "Bodega Quickbooks" } },
    { "NombreQuickbooks", new ColumnMapping { DatabaseColumnName = "NombreQuickbooks", AppColumnName = "Nombre Quickbooks" } },
    { "CodigoQuickbooks", new ColumnMapping { DatabaseColumnName = "CodigoQuickbooks", AppColumnName = "Codigo Quickbooks" } },
    { "NombreScribebase", new ColumnMapping { DatabaseColumnName = "NombreScribebase", AppColumnName = "Nombre Scribebase" } },
    { "CodigoScribebase", new ColumnMapping { DatabaseColumnName = "CodigoScribebase", AppColumnName = "Codigo Scribebase" } },
    { "BodegaProviSevenfifty", new ColumnMapping { DatabaseColumnName = "BodegaProviSevenfifty", AppColumnName = "Bodega ProviSevenfifty" } },
    { "CodigoProviSevenfifty", new ColumnMapping { DatabaseColumnName = "CodigoProviSevenfifty", AppColumnName = "Codigo ProviSevenfifty" } },
    { "NombrePricePostNJ", new ColumnMapping { DatabaseColumnName = "NombrePricePostNJ", AppColumnName = "Nombre PricePostNJ" } },
    { "CodigoPricePostNJ", new ColumnMapping { DatabaseColumnName = "CodigoPricePostNJ", AppColumnName = "Codigo PricePostNJ" } },
    { "NombrePricePostNY", new ColumnMapping { DatabaseColumnName = "NombrePricePostNY", AppColumnName = "Nombre PricePostNY" } },
    { "CodigoPricePostNY", new ColumnMapping { DatabaseColumnName = "CodigoPricePostNY", AppColumnName = "Codigo PricePostNY" } },
    { "NombreCatalogo", new ColumnMapping { DatabaseColumnName = "NombreCatalogo", AppColumnName = "Nombre Catalogo" } },
    { "PaginaCatalogo", new ColumnMapping { DatabaseColumnName = "PaginaCatalogo", AppColumnName = "Pagina Catalogo" } },
    { "CodigoProvi", new ColumnMapping { DatabaseColumnName = "CodigoProvi", AppColumnName = "CodigoProvi" } }
};

    }
}
