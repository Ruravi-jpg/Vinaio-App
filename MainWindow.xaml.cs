using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;

namespace Vinaio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OleDbConnection connection = new OleDbConnection();

        private List<CalculatedColumn> calculatedColumnsMasterReport;
        public MainWindow()
        {

            InitializeComponent();

            connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Rura\\Documents\\BaseMaestra.accdb";

            RefreshDataGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string userControlName = button.Tag.ToString();
                LoadUserControl(GetUserControlByName(userControlName));
            }
        }

        private UserControl GetUserControlByName(string userControlName)
        {
            switch (userControlName)
            {
                case "Inicio":
                    return null;
                case "Reportes":
                    return new CreateReportsUserControl();
                case "InsertarVino":
                    return new AddProductsUserControl();
                case "BuscarVino":
                    return new SearchProductsUserControl();
                case "Configuracion":
                    return new ConfigurationUserControl();
                default:
                    return null;
            }
        }

        private void LoadUserControl(UserControl userControl)
        {
            MainContentControl.Content = userControl;
        }

        private void ImportCsv_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                string csvFilePath = openFileDialog.FileName;
                ImportCsvData(csvFilePath);
            }
        }

        private void ImportCsvData(string filePath)
        {
            try
            {
                string[] csvLines = File.ReadAllLines(filePath);

                foreach (string csvLine in csvLines.Skip(1).Where(line => !string.IsNullOrEmpty(line)))
                {
                    string[] values = csvLine.Split(',');

                    // Check if the array has enough elements before accessing them
                    if (values.Length >= 15)
                    {
                        string productName = values[0].Trim();
                        int productYear = int.Parse(values[1].Trim());
                        int units = int.Parse(values[2].Trim());
                        double bottleBaseCost = double.Parse(values[3].Trim());
                        double fob = double.Parse(values[4].Trim());
                        double ry = double.Parse(values[5].Trim());
                        double br = double.Parse(values[6].Trim());
                        double war = double.Parse(values[7].Trim());
                        double caseWholesalePrice = double.Parse(values[8].Trim());
                        double paletDiscountPercentage = double.Parse(values[9].Trim());
                        double commisPercentage = double.Parse(values[10].Trim());
                        double stTax = double.Parse(values[11].Trim());
                        double caseRetailPrice = double.Parse(values[12].Trim());
                        double averagePrice = double.Parse(values[13].Trim());
                        double discountPercentage = double.Parse(values[14].Trim());

                        InsertDataIntoDatabase(productName, productYear, units, bottleBaseCost, fob, ry, br, war, caseWholesalePrice, paletDiscountPercentage, commisPercentage, stTax, caseRetailPrice, averagePrice, discountPercentage);
                    }
                    else
                    {
                        // Log or handle the case where the array doesn't have enough elements
                        MessageBox.Show("Invalid CSV line: " + csvLine);
                    }
                }

                // Refresh the data grid after import
                RefreshDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing CSV: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InsertDataIntoDatabase(string productName, int productYear, int units, double bottleBaseCost, double fob, double ry, double br, double war, double caseWholesalePrice, double paletDiscountPercentage, double commisPercentage, double stTax, double caseRetailPrice, double averagePrice, double discountPercentage)
        {
            try
            {
                string insertQuery = "INSERT INTO MainData (ProductName, ProductYear, Units, BottleBaseCost, FOB, RY, BR, WAR, CaseWholesalePrice, PaletDiscountPercentage, CommisPercentage, STTax, CaseRetailPrice, AveragePrice, DiscountPercentage) " +
                                     "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                {
                    // Add parameters and their values
                    command.Parameters.AddWithValue("ProductName", productName);
                    command.Parameters.AddWithValue("ProductYear", productYear);
                    command.Parameters.AddWithValue("Units", units);
                    command.Parameters.AddWithValue("BottleBaseCost", bottleBaseCost);
                    command.Parameters.AddWithValue("FOB", fob);
                    command.Parameters.AddWithValue("RY", ry);
                    command.Parameters.AddWithValue("BR", br);
                    command.Parameters.AddWithValue("WAR", war);
                    command.Parameters.AddWithValue("CaseWholesalePrice", caseWholesalePrice);
                    command.Parameters.AddWithValue("PaletDiscountPercentage", paletDiscountPercentage);
                    command.Parameters.AddWithValue("CommisPercentage", commisPercentage);
                    command.Parameters.AddWithValue("STTax", stTax);
                    command.Parameters.AddWithValue("CaseRetailPrice", caseRetailPrice);
                    command.Parameters.AddWithValue("AveragePrice", averagePrice);
                    command.Parameters.AddWithValue("DiscountPercentage", discountPercentage);

                    // Open the connection, execute the query, and close the connection
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting data into the database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Ensure the connection is closed even if an exception occurs
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }



        private void RefreshDataGrid()
        {
            try
            {
                // Refresh the data grid after importing data
                using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM MainData", connection))
                {
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);

                    DataView view = dataset.Tables[0].DefaultView;
                    //BaseData.ItemsSource = view;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data grid: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Ensure the connection is closed
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
