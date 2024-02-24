
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme;
using TextBox = System.Windows.Controls.TextBox;

namespace Vinaio
{

    /// <summary>
    /// Interaction logic for DataDispolayWindow.xaml
    /// </summary>
    public partial class DataDispolayWindow : Window
    {
        public DataDispolayWindow()
        {
            InitializeComponent();
        }

        private void TrueButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void FalseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        public void LoadData(List<Dictionary<string, object>> calculatedValuesList, List<CalculatedColumn> calculatedColumns)
        {

            //cast calculated values into a list of keyValueHolder
            List<KeyValueHolder> keyValueHolders = new List<KeyValueHolder>();
            foreach (var calculatedValue in calculatedValuesList)
            {
                KeyValueHolder keyValueHolder = new KeyValueHolder();
                foreach (var key in calculatedValue.Keys)
                {
                    keyValueHolder.Add(key, calculatedValue[key]);
                }
                keyValueHolders.Add(keyValueHolder);
            }
            // Clear any existing columns and rows
            DataGrid.Columns.Clear();
            DataGrid.Items.Clear();

            // Create columns dynamically based on the keys in the dictionaries
            if (calculatedValuesList.Count > 0)
            {
                // Get the keys from the first dictionary
                var keys = calculatedValuesList[0].Keys.ToList();

                // Create columns for each key
                foreach (var key in keys)
                {
                    var dataGridColumn = new DataGridTextColumn
                    {
                        Header = key,
                        Binding = new System.Windows.Data.Binding($"[{key}]")
                    };

                    // Apply format if specified for the current column
                    var column = calculatedColumns.FirstOrDefault(c => c.Name == key);
                    if (column != null && !string.IsNullOrEmpty(column.Format))
                    {
                        dataGridColumn.Binding.StringFormat = column.Format;
                    }

                    DataGrid.Columns.Add(dataGridColumn);
                }
            }

            // Set the items source to calculatedValuesList
            DataGrid.ItemsSource = calculatedValuesList;
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var editedValue = (e.EditingElement as TextBox)?.Text;

            ModifiedCellTracker.ModifiedCells.Add(new ModifiedCell
            {
                RowIndex = e.Row.GetIndex(),
                ColumnIndex = e.Column.DisplayIndex,
                NewValue = editedValue
            });
        }

    }
}
