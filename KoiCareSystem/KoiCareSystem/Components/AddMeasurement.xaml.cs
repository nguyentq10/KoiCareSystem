using Repositories.Entities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Windows;

namespace KoiCareSystem.Components
{
    public partial class AddMeasurement : Window
    {
        private readonly MeasurementService _service = new();
        private readonly PondService _pondService = new();
        public Repositories.Entities.User? LoggedInUser { get; set; }

        public AddMeasurement()
        {
            InitializeComponent();
            LoadPondIds();
        }

        private void LoadPondIds()
        {
            try
            {
                List<Pond> ponds = _pondService.GetPondsByUserId(LoggedInUser.UserId);
                PondIdComboBox.ItemsSource = ponds;
                PondIdComboBox.DisplayMemberPath = "PondId";
                PondIdComboBox.SelectedValuePath = "PondId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pond IDs: {ex.Message}");
            }
        }

        private void AddMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoggedInUser == null)
            {
                MessageBox.Show("Please log in to add a measurement.");
                return;
            }

            try
            {
                var measurement = new Measurement
                {
                    PondId = (int)PondIdComboBox.SelectedValue,
                    UserId = LoggedInUser.UserId,
                    Nitrite = double.Parse(NitriteTextBox.Text),
                    Oxygen = double.Parse(OxygenTextBox.Text),
                    Nitrate = double.Parse(NitrateTextBox.Text),
                    Temperature = double.Parse(TemperatureTextBox.Text),
                    Phosphate = double.Parse(PhosphateTextBox.Text),
                    PH = double.Parse(PHTextBox.Text),
                    Ammonium = double.Parse(AmmoniumTextBox.Text),
                    Hardness = double.Parse(HardnessTextBox.Text),
                    CarbonDioxide = double.Parse(CarbonDioxideTextBox.Text),
                    CarbonHardness = double.Parse(CarbonHardnessTextBox.Text),
                    Salt = double.Parse(SaltTextBox.Text),
                    TotalChlorines = double.Parse(TotalChlorinesTextBox.Text),
                    OutdoorTemp = double.Parse(OutdoorTempTextBox.Text),
                    AmountFed = double.Parse(AmountFedTextBox.Text),
                    CreatedAt = DateTime.Now
                };

                _service.AddMeasurement(measurement);
                MessageBox.Show("Measurement added successfully.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding measurement: {ex.Message}");
            }
        }
    }
}
