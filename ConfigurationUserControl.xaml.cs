using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
namespace Vinaio
{
    /// <summary>
    /// Interaction logic for ConfigurationUserControl.xaml
    /// </summary>
    public partial class ConfigurationUserControl : UserControl
    {
        public ConfigurationUserControl()
        {
            InitializeComponent();
            LoadDatabaseConfig();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Save configuration settings to your application
            SaveDatabaseConfiguration();
            SaveEmailConfiguration();
        }

        private void btnSelectDatabaseFile_Click(object sender, RoutedEventArgs e)
        {
            // Use OpenFileDialog to allow the user to select the Access database file
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Access Database Files|*.accdb;*.mdb",
                Title = "Select Access Database File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Update the TextBox with the selected file path
                txtDatabaseFilePath.Text = openFileDialog.FileName;
            }
        }

        private void LoadDatabaseConfig()
        {
            // Load existing database configuration
            txtDatabaseFilePath.Text = ConfigurationManager.AppSettings["DatabaseFilePath"];
        }

        private void SaveDatabaseConfiguration()
        {
            // Save the updated database file path to your application's settings or configuration
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DatabaseFilePath"].Value = txtDatabaseFilePath.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void SaveEmailConfiguration()
        {
            // Implement code to save email configuration
            string smtpServer = txtSmtpServer.Text;
            string smtpPort = txtSmtpPort.Text;
            string smtpUsername = txtSmtpUsername.Text;
            string smtpPassword = txtSmtpPassword.Password;
            bool enableSsl = chkEnableSsl.IsChecked ?? false;

            // Save these values to your application's settings or configuration
            // For example, you can use the ConfigurationManager or custom settings class
        }
    }

}

