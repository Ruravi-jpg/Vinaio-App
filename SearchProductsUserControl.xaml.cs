using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vinaio
{
    /// <summary>
    /// Interaction logic for SearchProductsUserControl.xaml
    /// </summary>
    public partial class SearchProductsUserControl : UserControl
    {
        DatabaseService DatabaseService;
        List<string> columnNames;
        private string currentColumnName;
        Dictionary<string, ColumnMapping> columnMappings = ColumnMappingData.columnMappings;
        public SearchProductsUserControl()
        {
            InitializeComponent();
            DatabaseService = new DatabaseService();

            columnNames = DatabaseService.GetColumnNamesAndTypes("MainData").Keys.ToList();
            AddColumnNamesToCombobox();
        }

        private void AddColumnNamesToCombobox()
        {
            foreach (string columnName in columnNames)
            {
                // Check if the columnName is in the columnMappings dictionary
                if (columnMappings.ContainsKey(columnName))
                {
                    // Use the translated name from the columnMappings dictionary
                    ColumnComboBox.Items.Add(columnMappings[columnName].AppColumnName);
                }
                else
                {
                    // Use the original columnName if no translation is found
                    ColumnComboBox.Items.Add(columnName);
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userInput = UserQuery.Text.Trim();
                string secondUserInput = SecondUserQuery.Text.Trim();

                if (!string.IsNullOrEmpty(currentColumnName) && ComparisonOperatorComboBox.SelectedItem is ComboBoxItem selectedComboBoxItem)
                {
                    string query = $"SELECT * FROM MainData WHERE ";

                    string selectedOperator = selectedComboBoxItem.Content.ToString();

                    // Use the original column name if translation is available, otherwise use the provided name
                    string columnNameForQuery = columnMappings.Values.FirstOrDefault(mapping => mapping.AppColumnName == currentColumnName)?.DatabaseColumnName ?? currentColumnName;
                    // Handle different comparison operators
                    switch (selectedOperator)
                    {
                        case "Contains":
                            query += $"{columnNameForQuery} LIKE '%{userInput}%'";
                            break;

                        case "Equals":
                            query += $"{columnNameForQuery} = '{userInput}'";
                            break;

                        case "Greater Than":
                            double numericValue;
                            if (double.TryParse(userInput, out numericValue))
                            {
                                query += $"{columnNameForQuery} > {numericValue}";
                            }
                            else
                            {
                                MessageBox.Show("Invalid numeric input.");
                                return;
                            }
                            break;

                        case "Less Than":
                            if (double.TryParse(userInput, out numericValue))
                            {
                                query += $"{columnNameForQuery} < {numericValue}";
                            }
                            else
                            {
                                MessageBox.Show("Invalid numeric input.");
                                return;
                            }
                            break;

                        case "Between":
                            double secondNumericValue;
                            if (double.TryParse(userInput, out numericValue) && double.TryParse(secondUserInput, out secondNumericValue))
                            {
                                query += $"{columnNameForQuery} BETWEEN {numericValue} AND {secondNumericValue}";
                            }
                            else
                            {
                                MessageBox.Show("Invalid numeric input.");
                                return;
                            }
                            break;

                        default:
                            MessageBox.Show("Please select a comparison operator.");
                            return;
                    }

                    // Execute the query
                    DataTable resultTable = DatabaseService.ExecuteQuery(query);

                    // Create a new DataTable with alternative column names
                    DataTable mappedResultTable = new DataTable();

                    // Add columns to the new DataTable based on the mappings
                    foreach (var mapping in columnMappings.Values)
                    {
                        mappedResultTable.Columns.Add(mapping.AppColumnName);
                    }

                    // Copy data from the original DataTable to the new DataTable
                    foreach (DataRow originalRow in resultTable.Rows)
                    {
                        DataRow newRow = mappedResultTable.NewRow();

                        foreach (var mapping in columnMappings.Values)
                        {
                            newRow[mapping.AppColumnName] = originalRow[mapping.DatabaseColumnName];
                        }

                        mappedResultTable.Rows.Add(newRow);
                    }

                    // Update the DataGrid with the new DataTable
                    ResultsDataGrid.ItemsSource = mappedResultTable.DefaultView;

                }
                else
                {
                    MessageBox.Show("Please select a column.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                MessageBox.Show($"Error executing the search: {ex.Message}");
            }
        }

        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            ColumnComboBox.SelectedIndex = -1;
            currentColumnName = "";
            UserQuery.Text = "Query";
        }

        private void ColumnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedItem != null)
            {
                currentColumnName = comboBox.SelectedItem.ToString();
            }
        }

        private void ResultsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                // Access the edited row
                var editedRow = (DataRowView)e.Row.Item;

                // Access the edited column and value
                var editedColumn = e.Column.Header.ToString();
                var editedValue = ((TextBox)e.EditingElement).Text;

                // Find the ID value from the edited row
                if (editedRow != null && editedRow.Row.Table.Columns.Contains("ID"))
                {
                    int id;
                    if (int.TryParse(editedRow["ID"].ToString(), out id))
                    {
                        // Find the corresponding database column name using the mapping
                        string databaseColumnName = GetDatabaseColumnName(editedColumn);

                        // Update the database based on the changes
                        UpdateDatabase(id, databaseColumnName, editedValue);
                    }
                    else
                    {
                        MessageBox.Show("Unable to parse ID value.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                MessageBox.Show($"Error updating the database: {ex.Message}");
            }
        }

        private string GetDatabaseColumnName(string appColumnName)
        {
            // Find the corresponding database column name using the mapping
            foreach (var mapping in columnMappings.Values)
            {
                if (mapping.AppColumnName == appColumnName)
                {
                    return mapping.DatabaseColumnName;
                }
            }

            // If no mapping is found, return the original column name
            return appColumnName;
        }

        private void UpdateDatabase(int id, string editedColumn, string editedValue)
        {
            try
            {
                // Use the ID value directly in the query
                string updateQuery = $"UPDATE MainData SET {editedColumn} = '{editedValue}' WHERE ID = {id}";

                // Execute the update query using your database service
                DatabaseService.ExecuteQuery(updateQuery);
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                MessageBox.Show($"Error updating the database: {ex.Message}");
            }
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Retrieve all items from the database
                string query = "SELECT * FROM MainData";
                DataTable resultTable = DatabaseService.ExecuteQuery(query);

                // Create a new DataTable with alternative column names
                DataTable mappedResultTable = new DataTable();

                // Add columns to the new DataTable based on the mappings
                foreach (var mapping in columnMappings.Values)
                {
                    mappedResultTable.Columns.Add(mapping.AppColumnName);
                }

                // Copy data from the original DataTable to the new DataTable
                foreach (DataRow originalRow in resultTable.Rows)
                {
                    DataRow newRow = mappedResultTable.NewRow();

                    foreach (var mapping in columnMappings.Values)
                    {
                        newRow[mapping.AppColumnName] = originalRow[mapping.DatabaseColumnName];
                    }

                    mappedResultTable.Rows.Add(newRow);
                }

                // Update the DataGrid with the new DataTable
                ResultsDataGrid.ItemsSource = mappedResultTable.DefaultView;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                MessageBox.Show($"Error retrieving data from the database: {ex.Message}");
            }
        }

        private void ComparisonOperatorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComparisonOperatorComboBox.SelectedItem is ComboBoxItem selectedComboBoxItem)
            {
                string selectedOperator = selectedComboBoxItem.Content.ToString();

                // Adjust visibility based on the selected operator
                SecondUserQuery.Visibility = selectedOperator == "Between" ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
