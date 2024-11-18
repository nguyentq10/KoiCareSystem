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
using System.Windows.Shapes;

namespace KoiCareSystem
{
    /// <summary>
    /// Interaction logic for SaltCalculatorWindow.xaml
    /// </summary>
    public partial class SaltCalculatorWindow : Window
    {
        public Repositories.Entities.User? LoggedInUser { get; set; }

        private readonly PondService _pondService = new();
        private double _volume;
        private double _currentConcentration;
        private double _desiredConcentration;
        private double _waterChange;

        public SaltCalculatorWindow()
        {
            InitializeComponent();
        }

        private void LoadPonds()
        {
            var ponds = _pondService.GetPondsByUserId(LoggedInUser.UserId);
            PondComboBox.ItemsSource = ponds;
            if (ponds.Any())
            {
                PondComboBox.SelectedIndex = 0;
            }
        }

        private void PondComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PondComboBox.SelectedItem is Pond selectedPond)
            {
                _volume = selectedPond.Volume;
                PondVolumeTextBlock.Text = $"{_volume} liters";
                ShowResult();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _currentConcentration = Math.Round(CurrentConcentrationSlider.Value, 2);
            _desiredConcentration = Math.Round(DesiredConcentrationSlider.Value, 2);
            _waterChange = Math.Round(WaterChangeSlider.Value, 2);

            CurrentConcentrationTextBlock.Text = $"{_currentConcentration:F2} %";
            DesiredConcentrationTextBlock.Text = $"{_desiredConcentration:F2} %";
            WaterChangeTextBlock.Text = $"{_waterChange:F2} % ({(_volume * _waterChange / 100):F2} liters)";

            ShowResult();
        }

        private void ShowResult()
        {
            if (_desiredConcentration < _currentConcentration)
            {
                double tempConcentration = _currentConcentration;
                int changes = 0;
                if (_waterChange == 0 || _desiredConcentration == 0)
                {
                    ResultTextBlock.Text = "Changes: N/A";
                    return;
                }
                while (tempConcentration > _desiredConcentration)
                {
                    tempConcentration -= (tempConcentration * _waterChange) / 100;
                    changes++;
                }
                ResultTextBlock.Text = $"Number of water changes to reach desired concentration: {changes}";
            }
            else
            {
                double concentrationDiff = _desiredConcentration - _currentConcentration;
                double saltAmount = (_volume * concentrationDiff) / 100;
                double saltPerChange = (_volume * _waterChange * _desiredConcentration) / 10000;
                ResultTextBlock.Text = $"Required Salt Amount: {saltAmount:F1} kg\nPer water change (refill): {saltPerChange:F2} kg";
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
            LoadPonds();
        }
    }
}
