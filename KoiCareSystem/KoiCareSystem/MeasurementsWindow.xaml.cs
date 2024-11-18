using KoiCareSystem.Components;
using Repositories.Entities;
using Services.Services;
using System.Windows;
using System.Windows.Controls;

namespace KoiCareSystem
{
    public partial class MeasurementsWindow : Window
    {
        private readonly MeasurementService _service = new();

        public Repositories.Entities.User? LoggedInUser { get; set; }

        public MeasurementsWindow()
        {
            InitializeComponent();
        }

        private void LoadPonds()
        {
            if (LoggedInUser == null)
            {
                MessageBox.Show("Please log in to view your measurements.");
                return;
            }
            var ponds = _service.GetMeasurementsByUserId(LoggedInUser.UserId);
            MeansDataGrid.ItemsSource = ponds;
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
            LoadPonds();
        }

        private void AddMea_Click(object sender, RoutedEventArgs e)
        {
            AddMeasurement addMeasurement = new();
            addMeasurement.LoggedInUser = LoggedInUser;
            addMeasurement.ShowDialog();
            LoadPonds();
        }
    }
}
