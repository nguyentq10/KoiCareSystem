using KoiCareSystem.Components;
using Repositories.Entities;
using Services.Services;
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

namespace KoiCareSystem
{
    /// <summary>
    /// Interaction logic for KoiDetailWindow.xaml
    /// </summary>
    public partial class KoiDetailWindow : Window
    {
        private readonly KoiService _koiService = new();

        public Repositories.Entities.User? LoggedInUser { get; set; }

        private Koi _koi;

        public KoiDetailWindow(Koi koi)
        {
            InitializeComponent();
            _koi = koi;
            try
            {
                DisplayKoiDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while displaying koi details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisplayKoiDetails()
        {
            try
            {
                if (_koi == null)
                {
                    MessageBox.Show("Koi details are not available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(!string.IsNullOrEmpty(_koi.Thumbnail))
                {
                    KoiThumbnail.Source = new BitmapImage(new Uri(_koi.Thumbnail));
                }

                KoiName.Text = _koi.Name ?? "N/A";
                KoiAge.Text = _koi.Age.ToString();
                KoiLength.Text = _koi.Length.ToString();
                KoiWeight.Text = _koi.Weight.ToString();
                KoiColor.Text = _koi.Color ?? "N/A";
                KoiVariety.Text = _koi.Variety ?? "N/A";
                KoiSex.Text = _koi.Sex ?? "N/A";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while displaying koi details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateKoiButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateKoi updateKoiWindow = new UpdateKoi(_koi);
            updateKoiWindow.ShowDialog();
            try
            {
                DisplayKoiDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating koi details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteKoiButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this koi?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _koiService.DeleteKoi(_koi);
                MessageBox.Show("Koi deleted successfully.");
                KoiWindow koiWindow = new();
                koiWindow.LoggedInUser = LoggedInUser;
                koiWindow.Show();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoggedInUser != null)
            {
                Sidebar.LoggedInUser = LoggedInUser;
            }
            else
            {
                MessageBox.Show("Logged in user information is missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
