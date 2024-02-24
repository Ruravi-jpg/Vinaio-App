using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Vinaio.Reports
{
    public class BaseReport
    {
        protected List<CalculatedColumn> calculatedColumns;
        protected string reportName;
        protected DataTable dataTable;
        List<Dictionary<string, object>> calculatedValuesList;

        protected virtual void AddCustomHeaders(ISheet worksheet, out int startRow, out int startColumn)
        {
            // By default, do nothing. This method can be overridden in derived classes.
            startRow = 0;
            startColumn = 0;
        }

        protected void RetrieveData()
        {
            // Create an instance of DatabaseService
            DatabaseService databaseService = new DatabaseService();

            // Query to get data from the database
            string selectQuery = "SELECT * FROM MainData";
            dataTable = databaseService.ExecuteQuery(selectQuery);
        }

        protected void CalculateColumns()
        {

            // Create a list to store the calculated values for each row
            calculatedValuesList = new List<Dictionary<string, object>>();

            // Populate calculated values for each row
            foreach (DataRow row in dataTable.Rows)
            {
                // Create a dictionary to store calculated values for this row
                Dictionary<string, object> calculatedValues = new Dictionary<string, object>();

                // Populate calculated values for each column
                foreach (CalculatedColumn column in calculatedColumns.Where(x => !x.RequiresExternalValues))
                {
                    // Calculate the value for this column
                    object cellValue = column.Calculate(row, calculatedValues);

                    // Store the calculated value in the dictionary
                    calculatedValues[column.Name] = cellValue;
                }

                foreach (CalculatedColumn column in calculatedColumns.Where(x => x.RequiresExternalValues))
                {
                    // Calculate the value for this column
                    object cellValue = column.Calculate(row, calculatedValues);

                    // Store the calculated value in the dictionary
                    calculatedValues[column.Name] = cellValue;
                }

                // Add the calculated values for this row to the list
                calculatedValuesList.Add(calculatedValues);
            }
        }


        public void ShowDataGridWindow()
        {
            
            // Retrieve data from the database
            RetrieveData();

            // Calculate columns based on the retrieved data
            CalculateColumns();

            // Create a new instance of DataGridWindow
            DataDispolayWindow dataGridWindow = new DataDispolayWindow();

            // Pass the data and calculated columns to the window
            dataGridWindow.LoadData(calculatedValuesList, calculatedColumns);

            bool? result = dataGridWindow.ShowDialog();

            if (result == true)
            {
                GenerateReport();
            }
            
        }

        public void GenerateReport()
        {
            try
            {

                string defaultFileName = reportName;

                if (dataTable == null || calculatedColumns == null)
                {
                    // Retrieve data from the database
                    RetrieveData();

                    // Calculate columns based on the retrieved data
                    CalculateColumns();
                }


                // Create a SaveFileDialog
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx",
                    DefaultExt = "xlsx",
                    Title = "Save Excel Report",
                    FileName = defaultFileName
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Create a new workbook
                    IWorkbook workbook = new XSSFWorkbook();

                    // Create a worksheet
                    ISheet worksheet = workbook.CreateSheet("Report");

                    int customHeaderStartRow, customHeaderStartColumn;
                    AddCustomHeaders(worksheet, out customHeaderStartRow, out customHeaderStartColumn);

                    // Add headers
                    List<string> headers = calculatedColumns.Select(column => column.Name).ToList();

                    // Add headers to the worksheet
                    for (int i = 0; i < headers.Count; i++)
                    {
                        string headerValue = calculatedColumns[i].OptionalColumnName ?? calculatedColumns[i].Name;

                        // Create the row if it doesn't exist
                        var headerRow = worksheet.GetRow(customHeaderStartRow) ?? worksheet.CreateRow(customHeaderStartRow);

                        headerRow.CreateCell(customHeaderStartColumn + i).SetCellValue(headerValue);
                    }


                    // Iterate over rows in DataTable
                    int rowIndex = customHeaderStartRow + 1;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Add calculated values to the worksheet
                        int columnIndex = 0;

                        foreach (string header in headers)
                        {
                            // Create the row if it doesn't exist
                            var dataRow = worksheet.GetRow(rowIndex) ?? worksheet.CreateRow(rowIndex);

                            // Adjust the column index for data cells to align with headers
                            ICell cell = dataRow.CreateCell(customHeaderStartColumn + columnIndex);

                            //get the cell value
                            object cellValue = calculatedValuesList[rowIndex - customHeaderStartRow - 1][header];


                            // Apply format if specified
                            var column = calculatedColumns.First(c => c.Name == header);
                            if (!string.IsNullOrEmpty(column.Format))
                            {
                                var cellAux = dataRow.GetCell(customHeaderStartColumn + columnIndex);
                                ICellStyle cellStyle = workbook.CreateCellStyle();
                                IDataFormat format = workbook.CreateDataFormat();

                                double value = 0.0;
                                if (column.Format == "0.00%")
                                {
                                    cellStyle.DataFormat = format.GetFormat("0.00%");

                                    if (cellValue.ToString().Contains("%"))
                                    {
                                        //convert the value onto its decimal form, for example 12.00% to 0.12
                                        value = double.Parse(cellValue.ToString().Replace("%", "")) / 100;
                                    }
                                    else
                                    {
                                        value = double.Parse(cellValue.ToString());
                                    }
                                    double truncatedValue = Math.Truncate(value * 100) / 100; // Truncate to 2 decimals
                                    cell.SetCellValue(truncatedValue);

                                    cell.CellStyle = cellStyle;

                                }
                                else if (column.Format == "$#,##0.00")
                                {
                                    cellStyle.DataFormat = format.GetFormat("$#,##0.00");
                                    // Assuming this is a currency format, set the value as a number and apply currency format
                                    value = double.Parse(cellValue.ToString().Replace("$", ""));
                                    double truncatedValue = Math.Truncate(value * 100) / 100; // Truncate to 2 decimals
                                    cell.SetCellValue(truncatedValue);
                                    cell.CellStyle = cellStyle;

                                }
                                else if (column.Format == "€#,##0.00")
                                {
                                    cellStyle.DataFormat = format.GetFormat("€#,##0.00");
                                    // Assuming this is a currency format, set the value as a number and apply currency format
                                    value = double.Parse(cellValue.ToString().Replace("€", ""));
                                    double truncatedValue = Math.Truncate(value * 100) / 100; // Truncate to 2 decimals
                                    cell.SetCellValue(truncatedValue);
                                    cell.CellStyle = cellStyle;
                                }
                                // Add more conditions for other custom formats if needed
                                else
                                {
                                    cell.SetCellValue(cellValue.ToString());
                                }
                            }
                            else
                            {
                                try
                                {
                                    string value = cellValue.ToString();
                                    // No specific format, set the value as is
                                    if (int.TryParse(value, out int result))
                                    {
                                        cell.SetCellValue(result);
                                    }
                                    else if (double.TryParse(value, out double result2))
                                    {
                                        double truncatedValue = Math.Truncate(result2 * 100) / 100; // Truncate to 2 decimals
                                        cell.SetCellValue(truncatedValue);
                                    }
                                    else if (cellValue is DateTime)
                                    {
                                        cell.SetCellValue((DateTime)cellValue);
                                    }
                                    else
                                    {
                                        // If the value is not of a recognized numeric type, set it as a string
                                        cell.SetCellValue(cellValue.ToString());
                                    }
                                }
                                catch (Exception e)
                                {

                                    throw;
                                }
                               
                            }

                            columnIndex++;
                        }

                        rowIndex++;
                    }

                    //auto size columns
                    for (int i = 0; i < headers.Count; i++)
                    {
                        worksheet.AutoSizeColumn(i);
                    }

                    foreach (var modifiedCell in ModifiedCellTracker.ModifiedCells)
                    {
                        int modifiedRowIndex = modifiedCell.RowIndex;
                        int modifiedColumnIndex = modifiedCell.ColumnIndex;

                        // Apply formatting to the cell in the Excel worksheet based on its coordinates (rowIndex, columnIndex)
                        IRow row = worksheet.GetRow(modifiedRowIndex + customHeaderStartRow + 1);
                        ICell cell = row.GetCell(modifiedColumnIndex + customHeaderStartColumn);
                        if (cell != null)
                        {
                            ICellStyle existingStyle = cell.CellStyle;
                            ICellStyle newStyle = workbook.CreateCellStyle();

                            // Preserve any existing styles
                            if (existingStyle != null)
                            {
                                newStyle.CloneStyleFrom(existingStyle);
                            }

                            // Apply the new background color
                            newStyle.FillForegroundColor = IndexedColors.Yellow.Index;
                            newStyle.FillPattern = FillPattern.SolidForeground;

                            cell.CellStyle = newStyle;
                        }
                    }

                    ModifiedCellTracker.Reset();

                    // Save the Excel workbook to the chosen file path
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        workbook.Write(fs);
                        fs.Close();
                    }

                    MessageBox.Show("Excel report created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating Excel report: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}