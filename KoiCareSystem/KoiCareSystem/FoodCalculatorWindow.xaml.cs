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
    /// Interaction logic for FoodCalculatorWindow.xaml
    /// </summary>
    public partial class FoodCalculatorWindow : Window
    {
        public Repositories.Entities.User? LoggedInUser { get; set; }

        private readonly PondService _pondService = new();
        private readonly KoiService _koiService = new();
        private List<Pond>? _ponds;
        private float _temperature;
        private string _growthSpeed = "medium";
        private float _totalWeight;
        private float _foodRequirement;

        public FoodCalculatorWindow()
        {
            InitializeComponent();
        }

        private void LoadPonds()
        {
            _ponds = _pondService.GetPondsByUserId(LoggedInUser.UserId);
            foreach (var pond in _ponds)
            {
                PondComboBox.Items.Add(new ComboBoxItem { Content = pond.Name, Tag = pond.PondId });
            }
        }

        private void PondComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PondComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is int pondId)
            {
                var kois = _koiService.GetKoisByPondId(pondId);
                _totalWeight = kois.Sum(koi => koi.Weight);
                TotalWeightTextBlock.Text = $"Total Weight of Kois: {_totalWeight} g";
                CalculateFoodRequirement();
            }
        }

        private void TemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var tempRange = button.Content.ToString();
                _temperature = tempRange switch
                {
                    "6-8" => 7,
                    "9-12" => 10,
                    "13-16" => 14,
                    "17-20" => 18,
                    "21-28" => 24,
                    _ => 0
                };
                CalculateFoodRequirement();
            }
        }

        private void GrowthSpeedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GrowthSpeedComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                _growthSpeed = selectedItem.Content.ToString().ToLower();
                CalculateFoodRequirement();
            }
        }

        private void CalculateFoodRequirement()
        {
            if (_totalWeight > 0 && _temperature > 0)
            {
                float multiplier = _growthSpeed switch
                {
                    "low" => 1.7f,
                    "high" => 3.0f,
                    _ => 2.5f
                };

                _foodRequirement = _totalWeight * multiplier * (_temperature / 10000);
                FoodRequirementTextBlock.Text = $"Food Requirement: {_foodRequirement:F0} g/day";
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
