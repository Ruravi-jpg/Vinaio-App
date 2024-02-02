using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Vinaio.Reports
{
    public class BaseReport
    {
        protected List<CalculatedColumn> calculatedColumns;
        protected string reportName;

        protected virtual void AddCustomHeaders(ISheet worksheet, out int startRow, out int startColumn)
        {
            // By default, do nothing. This method can be overridden in derived classes.
            startRow = 1;
            startColumn = 1;
        }

        public void GenerateReport()
        {
            try
            {
                string defaultFileName = reportName;

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

                    // Create an instance of DatabaseService
                    DatabaseService databaseService = new DatabaseService();

                    // Query to get data from the database
                    string selectQuery = "SELECT * FROM MainData";
                    DataTable dataTable = databaseService.ExecuteQuery(selectQuery);

                    // Iterate over rows in DataTable
                    int rowIndex = customHeaderStartRow + 1;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Create a dictionary to store calculated column values
                        Dictionary<string, object> calculatedValues = new Dictionary<string, object>();

                        foreach (CalculatedColumn column in calculatedColumns.Where(c => !c.RequiresExternalValues))
                        {
                            calculatedValues[column.Name] = column.Calculate(row, calculatedValues);
                        }

                        foreach (CalculatedColumn column in calculatedColumns.Where(c => c.RequiresExternalValues))
                        {
                            calculatedValues[column.Name] = column.Calculate(row, calculatedValues);
                        }

                        // Add calculated values to the worksheet
                        int columnIndex = 0;

                        foreach (string header in headers)
                        {
                            // Create the row if it doesn't exist
                            var dataRow = worksheet.GetRow(rowIndex) ?? worksheet.CreateRow(rowIndex);

                            // Adjust the column index for data cells to align with headers
                            ICell cell = dataRow.CreateCell(customHeaderStartColumn + columnIndex);

                            object cellValue = calculatedValues[header];

                            // Apply format if specified
                            var column = calculatedColumns.First(c => c.Name == header);
                            if (!string.IsNullOrEmpty(column.Format))
                            {
                                if (column.Format == "0.00%")
                                {
                                    // Convert to percentage, multiply by 100, add "%" sign, and set as text
                                    double value = double.Parse(cellValue.ToString());
                                    double truncatedValue = Math.Truncate(value * 100) / 100; // Truncate to 2 decimals
                                    cell.SetCellValue($"{Convert.ToDouble(truncatedValue) * 100}%");

                                }
                                else if (column.Format == "\"$\"#,##0.00")
                                {
                                    // Assuming this is a currency format, set the value as a number and apply currency format
                                    double value = double.Parse(cellValue.ToString());
                                    double truncatedValue = Math.Truncate(value * 100) / 100; // Truncate to 2 decimals
                                    cell.SetCellValue($"${truncatedValue}");

                                }
                                else if (column.Format == "€#,##0.00")
                                {
                                    // Assuming this is a currency format, set the value as a number and apply currency format
                                    double value = double.Parse(cellValue.ToString());
                                    double truncatedValue = Math.Truncate(value * 100) / 100; // Truncate to 2 decimals
                                    cell.SetCellValue($"€{truncatedValue}");

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
                                    // No specific format, set the value as is
                                    if (int.TryParse((string)cellValue, out int result))
                                    {
                                        cell.SetCellValue(result);
                                    }
                                    else if (double.TryParse((string)cellValue, out double result2))
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