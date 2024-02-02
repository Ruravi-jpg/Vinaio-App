using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Vinaio
{
    internal class DatabaseService
    {
        string connectionString;
        public DatabaseService()
        {
            connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={DatabaseConfig.DatabaseFilePath};Persist Security Info=False;";
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using OleDbConnection connection = new OleDbConnection(connectionString);

                connection.Open();

                using OleDbCommand command = new OleDbCommand(query, connection);

                using OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                MessageBox.Show($"Database error: {ex.Message}");
            }

            return dataTable;
        }

        public Dictionary<string, Type> GetColumnNamesAndTypes(string tableName)
        {
            Dictionary<string, Type> columnNamesAndTypes = new Dictionary<string, Type>();

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Get the schema table
                    var schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName });

                    // Check if the schema table contains the necessary columns
                    if (schemaTable.Columns.Contains("COLUMN_NAME") && schemaTable.Columns.Contains("ORDINAL_POSITION"))
                    {
                        // Get the ordinal positions for the columns
                        var columnOrdinalForName = schemaTable.Columns["COLUMN_NAME"].Ordinal;
                        var columnOrdinalForOrdinal = schemaTable.Columns["ORDINAL_POSITION"].Ordinal;

                        // Order the columns by ordinal position
                        var columns = from DataRow r in schemaTable.Rows
                                      orderby r.ItemArray[columnOrdinalForOrdinal]
                                      select new
                                      {
                                          Ordinal = r.ItemArray[columnOrdinalForOrdinal].ToString(),
                                          ColumnName = r.ItemArray[columnOrdinalForName].ToString()
                                      };

                        // Populate the dictionary with column names and types
                        foreach (var column in columns)
                        {
                            if (column.ColumnName == "ID")
                                continue;

                            string columnName = column.ColumnName;

                            // You can retrieve the type using your existing function
                            int dataType = GetDataTypeForColumn(connection, tableName, columnName);
                            Type columnType = GetClrTypeFromOleDbType(dataType);

                            columnNamesAndTypes[columnName] = columnType;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                MessageBox.Show($"Database error: {ex.Message}");
            }

            return columnNamesAndTypes;
        }

        private int GetDataTypeForColumn(OleDbConnection connection, string tableName, string columnName)
        {
            // Retrieve the data type for a specific column
            var schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, columnName });

            if (schemaTable != null && schemaTable.Rows.Count > 0)
            {
                return (int)schemaTable.Rows[0]["DATA_TYPE"];
            }

            return -1; // Indicates an error or unknown data type
        }

        private Type GetClrTypeFromOleDbType(int oleDbType)
        {
            switch (oleDbType)
            {
                case (int)OleDbType.BigInt:
                    return typeof(long);
                case (int)OleDbType.Binary:
                    return typeof(byte[]);
                case (int)OleDbType.Boolean:
                    return typeof(bool);
                case (int)OleDbType.BSTR:
                case (int)OleDbType.Char:
                case (int)OleDbType.VarWChar:
                case (int)OleDbType.VarChar:
                case (int)OleDbType.WChar:
                    return typeof(string);
                case (int)OleDbType.Currency:
                    return typeof(decimal);
                case (int)OleDbType.Date:
                    return typeof(DateTime);
                case (int)OleDbType.Decimal:
                    return typeof(decimal);
                case (int)OleDbType.Double:
                    return typeof(double);
                case (int)OleDbType.Integer:
                    return typeof(int);
                case (int)OleDbType.Single:
                    return typeof(float);
                case (int)OleDbType.SmallInt:
                    return typeof(short);
                case (int)OleDbType.TinyInt:
                    return typeof(byte);
                case (int)OleDbType.Guid:
                    return typeof(Guid);
                case (int)OleDbType.DBDate:
                case (int)OleDbType.DBTime:
                case (int)OleDbType.DBTimeStamp:
                    return typeof(DateTime);
                case (int)OleDbType.Empty:
                    return typeof(object);
                case (int)OleDbType.IUnknown:
                case (int)OleDbType.IDispatch:
                case (int)OleDbType.Variant:
                    return typeof(object);
                // Add more cases for other OleDbType values as needed
                default:
                    return typeof(object);
            }
        }
        public Dictionary<string, string> GetLastItem(string tableName)
        {
            Dictionary<string, string> lastItem = new Dictionary<string, string>();

            try
            {

                string query = $"SELECT TOP 1 * FROM {tableName} ORDER BY ID DESC";

                DataTable dataTable = ExecuteQuery(query);

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        lastItem[column.ColumnName] = dataTable.Rows[0][column.ColumnName].ToString();
                    }


                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                MessageBox.Show($"Database error: {ex.Message}");
            }

            return lastItem;
        }

        public void AddNewItem(string tableName, Dictionary<string, string> values)
        {
            try
            {
                StringBuilder columns = new StringBuilder();
                StringBuilder parameterNames = new StringBuilder();

                // Build the column names and parameter placeholders
                foreach (var entry in values)
                {
                    columns.Append(entry.Key + ",");
                    parameterNames.Append("@" + entry.Key + ",");
                }

                // Remove the trailing commas
                columns.Remove(columns.Length - 1, 1);
                parameterNames.Remove(parameterNames.Length - 1, 1);

                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameterNames})";

                using OleDbConnection connection = new OleDbConnection(connectionString);

                using OleDbCommand command = new OleDbCommand(query, connection);
                // Add parameters to the command
                foreach (var entry in values)
                {
                    object parameterValue = string.IsNullOrEmpty(entry.Value) ? DBNull.Value : (object)entry.Value;
                    command.Parameters.AddWithValue("@" + entry.Key, parameterValue);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                MessageBox.Show($"Database error: {ex.Message}");
            }
        }
        public void InsertItemsFromCsv(string tableName, string csvFilePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(csvFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        Dictionary<string, string> itemValues = new Dictionary<string, string>();

                        // Map each value to its corresponding column name
                        for (int i = 0; i < values.Length; i++)
                        {
                            string columnName = GetColumnNameForIndex(i);
                            itemValues[columnName] = values[i];
                        }

                        AddNewItem(tableName, itemValues);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                MessageBox.Show($"Error inserting items from CSV: {ex.Message}");
            }
        }

        private string GetColumnNameForIndex(int index)
        {
            // Implement your logic to map the index to the corresponding column name
            // For example, if the CSV file has headers, you can use the header row to get the column names
            // If the CSV file does not have headers, you can use a predefined mapping or generate column names based on the index
            // Return the column name for the given index

            // Check if the CSV file has headers
            //bool hasHeaders = /* logic to determine if the CSV file has headers */;

            //if (hasHeaders)
            //{
            //    // Use the header row to get the column names
            //    string[] headers = /* logic to get the header row from the CSV file */;
            //    return headers[index];
            //}
            //else
            //{
            //    // Use a predefined mapping or generate column names based on the index
            //    string[] columnNames = /* logic to generate column names based on the index */;
            //    return columnNames[index];
            //}

            return "";
        }
    }


}
