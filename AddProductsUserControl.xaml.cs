using Microsoft.Win32;
using NPOI.POIFS.FileSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Vinaio
{
    /// <summary>
    /// Interaction logic for AddProductsUserControl.xaml
    /// </summary>
    public partial class AddProductsUserControl : UserControl
    {
        DatabaseService DatabaseService;
        Dictionary<string, Type> columnNamesAndTypes;
        Dictionary<string, ColumnMapping> columnMappings = ColumnMappingData.columnMappings;
        public AddProductsUserControl()
        {
            InitializeComponent();
            DatabaseService = new DatabaseService();

            columnNamesAndTypes = DatabaseService.GetColumnNamesAndTypes("MainData");
            List<string> columnNames = columnNamesAndTypes.Keys.ToList();

            CreateInputControls(columnNames);
            SetCustomModificationToLabels();
        }

        void SetCustomModificationToLabels()
        {
            foreach (var wrapPanelChild in YourGrid.Children)
            {
                if (wrapPanelChild is WrapPanel wrapPanel)
                {
                    foreach (var labelInputPair in wrapPanel.Children)
                    {
                        if (labelInputPair is StackPanel stackPanel)
                        {
                            foreach (var control in stackPanel.Children)
                            {
                                if (control is TextBox textBox && textBox.Name == "txtProductStatus")
                                {
                                    ComboBox comboBox = new ComboBox();
                                    comboBox.Name = "cmbProductStatus";
                                    comboBox.Items.Add("Activo");
                                    comboBox.Items.Add("Inactivo");
                                    comboBox.SelectionChanged += ComboBox_SelectionChanged;
                                    textBox.Visibility = Visibility.Collapsed;
                                    stackPanel.Children.Add(comboBox);
                                    //stackPanel.Children.Remove(textBox); // Remove the TextBox from the StackPanel
                                    break; // Exit the loop once the TextBox is found
                                }
                                else if (control is TextBox textBox2 && textBox2.Name == "txtVolumeUnit")
                                {
                                    ComboBox comboBox = new ComboBox();
                                    comboBox.Name = "cmbVolumeUnit";
                                    comboBox.Items.Add("ml");
                                    comboBox.Items.Add("L");
                                    comboBox.Items.Add("Oz");
                                    comboBox.Items.Add("Gal");
                                    comboBox.SelectionChanged += ComboBox_SelectionChanged;
                                    textBox2.Visibility = Visibility.Collapsed;
                                    stackPanel.Children.Add(comboBox);
                                    //stackPanel.Children.Remove(textBox2); // Remove the TextBox from the StackPanel
                                    break; // Exit the loop once the TextBox is found
                                }
                                //add now a option for when the textbox is a combobox with the options being starting from the current year 30 years into the past
                                else if (control is TextBox textBox3 && textBox3.Name == "txtProductYear")
                                {
                                    ComboBox comboBox = new ComboBox();
                                    comboBox.Name = "cmbProductYear";
                                    for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 100; i--)
                                    {
                                        comboBox.Items.Add(i);
                                    }
                                    comboBox.SelectionChanged += ComboBox_SelectionChanged;
                                    textBox3.Visibility = Visibility.Collapsed;
                                    stackPanel.Children.Add(comboBox);
                                    //stackPanel.Children.Remove(textBox3); // Remove the TextBox from the StackPanel
                                    break; // Exit the loop once the TextBox is found
                                }
                                //now with the txtType and add a combobox with the options: "Wine", "Beer", "Spirits", "Other"
                                else if (control is TextBox textBox4 && textBox4.Name == "txtType")
                                {
                                    ComboBox comboBox = new ComboBox();
                                    comboBox.Name = "cmbType";
                                    comboBox.Items.Add("Wine");
                                    comboBox.Items.Add("Beer");
                                    comboBox.Items.Add("Spirits");
                                    comboBox.Items.Add("Other");
                                    comboBox.SelectionChanged += ComboBox_SelectionChanged;
                                    textBox4.Visibility = Visibility.Collapsed;
                                    stackPanel.Children.Add(comboBox);
                                    //stackPanel.Children.Remove(textBox4); // Remove the TextBox from the StackPanel
                                    break; // Exit the loop once the TextBox is found
                                }
                                else if (control is TextBox textBox5 && textBox5.Name == "txtYearAdded")
                                {
                                    ComboBox comboBox = new ComboBox();
                                    comboBox.Name = "cmbYearAdded";
                                    for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 30; i--)
                                    {
                                        comboBox.Items.Add(i);
                                    }
                                    comboBox.SelectionChanged += ComboBox_SelectionChanged;
                                    textBox5.Visibility = Visibility.Collapsed;
                                    stackPanel.Children.Add(comboBox);
                                    //stackPanel.Children.Remove(textBox3); // Remove the TextBox from the StackPanel
                                    break; // Exit the loop once the TextBox is found
                                }

                            }
                        }
                    }
                }
            }
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (sender is ComboBox comboBox)
            {
                string value = comboBox.SelectedItem.ToString();
                string comboBoxName = comboBox.Name;
                string textBoxName = comboBoxName.Replace("cmb", "txt");

                foreach (var wrapPanelChild in YourGrid.Children)
                {
                    if (wrapPanelChild is WrapPanel wrapPanel)
                    {
                        foreach (var labelInputPair in wrapPanel.Children)
                        {
                            if (labelInputPair is StackPanel stackPanel)
                            {
                                foreach (var control in stackPanel.Children)
                                {
                                    if (control is TextBox textBox && textBox.Name == textBoxName)
                                    {
                                        textBox.Text = value;
                                        break; // Exit the loop once the TextBox is found
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool IsValueValid(string value, Type expectedType)
        {


            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            switch (Type.GetTypeCode(expectedType))
            {
                case TypeCode.Int32:
                    if (!int.TryParse(value, out _))
                    {
                        return false;
                    }
                    break;
                case TypeCode.Double:
                    if (!double.TryParse(value, out _))
                    {
                        return false;
                    }
                    break;
                case TypeCode.Decimal:
                    if (!decimal.TryParse(value, out _))
                    {
                        return false;
                    }
                    break;
                case TypeCode.DateTime:
                    if (!DateTime.TryParse(value, out _))
                    {
                        return false;
                    }
                    break;
                case TypeCode.Boolean:
                    if (!bool.TryParse(value, out _))
                    {
                        return false;
                    }
                    break;
                // Add more type validations as needed
                default:
                    // Handle other types or use a more specific validation approach
                    break;
            }

            return true; // All values are valid
        }

        private void CreateInputControls(List<string> columnNames)
        {
            YourGrid.RowDefinitions.Clear();
            YourGrid.ColumnDefinitions.Clear();

            // Create a WrapPanel to host the label and input pairs
            WrapPanel wrapPanel = new WrapPanel();
            wrapPanel.Orientation = Orientation.Horizontal;
            wrapPanel.Margin = new Thickness(10);

            // Add the WrapPanel to YourGrid
            YourGrid.Children.Add(wrapPanel);

            Style labelStyle = new Style(typeof(Label));
            Style textBoxStyle = new Style(typeof(TextBox));

            // Set the Foreground property using the AccentColor resource
            labelStyle.Setters.Add(new Setter(Label.ForegroundProperty, FindResource("SecondaryAccentColor")));
            labelStyle.Setters.Add(new Setter(Label.FontSizeProperty, 16.0));

            textBoxStyle.Setters.Add(new Setter(TextBox.ForegroundProperty, FindResource("TextColor")));
            textBoxStyle.Setters.Add(new Setter(TextBox.FontSizeProperty, 16.0));
            foreach (string columnName in columnNames)
            {

                string labelContent = columnMappings[columnName].AppColumnName;
                // Create a label
                Label label = new Label
                {
                    Content = labelContent,
                    Margin = new Thickness(5),
                    VerticalAlignment = VerticalAlignment.Center,
                    // Apply the custom style
                    Style = labelStyle
                };

                // Create a TextBox for input
                TextBox textBox = new TextBox
                {
                    Name = $"txt{columnName}",
                    Margin = new Thickness(5),
                    VerticalAlignment = VerticalAlignment.Center,
                    Style = textBoxStyle,
                    //set the background to transparent
                    Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0)),
                    //remove the border
                    BorderThickness = new Thickness(0.5),
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

                textBox.TextChanged += TextBox_TextChanged;

                // Create a StackPanel for each pair of label and input
                StackPanel labelInputPair = new StackPanel();
                labelInputPair.Orientation = Orientation.Vertical;
                labelInputPair.HorizontalAlignment = HorizontalAlignment.Stretch;

                // Add label and TextBox to the StackPanel
                labelInputPair.Children.Add(label);
                labelInputPair.Children.Add(textBox);

                labelInputPair.Margin = new Thickness(2);

                // Add the StackPanel to the WrapPanel
                wrapPanel.Children.Add(labelInputPair);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Perform validation for the modified TextBox
            if (sender is TextBox textBox)
            {
                string columnName = textBox.Name.Substring(3); // Remove "txt" prefix to get the column name
                string value = textBox.Text;

                // Validate the value based on its type
                if (columnNamesAndTypes.TryGetValue(columnName, out Type expectedType))
                {
                    if (!IsValueValid(value, expectedType))
                    {
                        // Set background color to indicate validation failure
                        textBox.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 0, 0));
                    }
                    else
                    {
                        // Reset background color if the value is valid
                        textBox.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0));
                    }
                }
            }
        }

        private void PopulateWithLastItemInDb(object sender, RoutedEventArgs e)
        {
            string tableName = "MainData"; // Replace with your actual table name
            Dictionary<string, string> lastItem = DatabaseService.GetLastItem(tableName);

            if (lastItem.Count > 0)
            {
                foreach (var entry in lastItem)
                {
                    string textBoxName = $"txt{entry.Key}";
                    string comboBoxName = $"cmb{entry.Key}";
                    // Iterate through the WrapPanel to find the TextBox by name
                    foreach (var wrapPanelChild in YourGrid.Children)
                    {
                        if (wrapPanelChild is WrapPanel wrapPanel)
                        {
                            foreach (var labelInputPair in wrapPanel.Children)
                            {
                                if (labelInputPair is StackPanel stackPanel)
                                {
                                    foreach (var control in stackPanel.Children)
                                    {
                                        if (control is TextBox textBox && textBox.Name == textBoxName)
                                        {
                                            textBox.Text = entry.Value;
                                            //break; // Exit the loop once the TextBox is found
                                        }
                                        //also search for a combobox with the name of comboboxName, and if it exists it searches for the value of entry.value on the combobox and sets that as the current selectedComboboxItem
                                        else if (control is ComboBox comboBox && comboBox.Name == comboBoxName)
                                        {
                                            if(entry.Value == "Active")
                                            {
                                                comboBox.SelectedItem = "Activo";
                                            }
                                            else if(entry.Value == "Inactive")
                                            {
                                                comboBox.SelectedItem = "Inactivo";
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    comboBox.SelectedItem = entry.Value;
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }
                                            }
                                            //break; // Exit the loop once the ComboBox is found
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No data found in the database.");
            }
        }

        private void Add_New_Item_Click(object sender, RoutedEventArgs e)
        {
            // Step 1: Retrieve values from input controls
            Dictionary<string, string> values = new Dictionary<string, string>();

            foreach (var control in YourGrid.Children)
            {
                if (control is TextBox textBox && textBox.Name.StartsWith("txt"))
                {
                    string columnName = textBox.Name.Substring(3); // Remove "txt" prefix to get the column name
                    string value = textBox.Text;
                    values[columnName] = value;
                }
            }

            foreach (var value in values)
            {
                if (!IsValueValid(value.Value, columnNamesAndTypes[value.Key]))
                {
                    MessageBox.Show($"Invalid value for {value.Key}.");
                    return;
                }
            }

            DatabaseService.AddNewItem("MainData", values);

            ClearInputControls();

        }

        // Clear input controls to reset the form after successfully adding a new item
        private void ClearInputControls()
        {
            foreach (var control in YourGrid.Children)
            {
                if (control is TextBox textBox && textBox.Name.StartsWith("txt"))
                {
                    textBox.Text = string.Empty;
                }
            }
        }

        private void Generate_csv(object sender, RoutedEventArgs e)
        {
            string tableName = "MainData"; // Replace with your actual table name
            GenerateDefaultCsvFile(tableName);
        }

        private void GenerateDefaultCsvFile(string tableName)
        {
            try
            {
                string csvHeader = string.Join(",", columnNamesAndTypes.Keys.ToList());

                // Use SaveFileDialog to allow the user to choose the file location
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    FileName = "DefaultData.csv",
                    Title = "Save CSV File",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    DefaultExt = ".csv"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string csvFilePath = saveFileDialog.FileName;
                    File.WriteAllText(csvFilePath, csvHeader);
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating default CSV file: {ex.Message}");
            }
        }
    }


}
