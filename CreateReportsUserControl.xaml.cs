using System;
using System.Collections.Generic;
using System.Linq;
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
using Vinaio.Reports;

namespace Vinaio
{
    /// <summary>
    /// Interaction logic for InicioUserControl.xaml
    /// </summary>
    public partial class CreateReportsUserControl : UserControl
    {
        public CreateReportsUserControl()
        {
            InitializeComponent();
        }
        private void CreateBaseReport(object sender, RoutedEventArgs e)
        {
            MainReport mainReport = new MainReport();

            mainReport.ShowDataGridWindow();
        }

        private void CreateNewJerseyReport(object sender, RoutedEventArgs e)
        {
            NewJerseyReport newJerseyReport = new();

            newJerseyReport.ShowDataGridWindow();
        }

        private void CreateNewYorkReport(object sender, RoutedEventArgs e)
        {
            NewYorkReport newYorkReport = new();

            newYorkReport.ShowDataGridWindow();
        }

        private void CreateVinaioReport(object sender, RoutedEventArgs e)
        {
            VinaioReport vinaioReport = new();
            vinaioReport.ShowDataGridWindow();
        }
    }
}
