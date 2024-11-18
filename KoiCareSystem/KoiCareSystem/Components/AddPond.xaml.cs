using Repositories.Entities;
using Services.Services;
using System;
using System.Windows;

namespace KoiCareSystem.Components
{
    /// <summary>
    /// Interaction logic for AddPond.xaml
    /// </summary>
    public partial class AddPond : Window
    {
        private readonly PondService _pondService = new();

        public Repositories.Entities.User? LoggedInUser { get; set; }

        public AddPond()
        {
            InitializeComponent();
        }

        private bool ValidateUri(string uri)
        {
            return Uri.TryCreate(uri, UriKind.Absolute, out Uri? validatedUri)
                   && (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
        }

        private void AddPondButton_Click(object sender, RoutedEventArgs e)
        {

            ValidationMessageTextBlock.Text = string.Empty;


            if (string.IsNullOrWhiteSpace(PondNameTextBox.Text))
            {
                ValidationMessageTextBlock.Text = "Pond Name is required.";
                return;
            }

            if (!int.TryParse(VolumeTextBox.Text, out int volume))
            {
                ValidationMessageTextBlock.Text = "Volume must be a valid integer.";
                return;
            }

            if (!float.TryParse(DepthTextBox.Text, out float depth))
            {
                ValidationMessageTextBlock.Text = "Depth must be a valid number.";
                return;
            }

            if (!int.TryParse(PumpingCapacityTextBox.Text, out int pumpingCapacity))
            {
                ValidationMessageTextBlock.Text = "Pumping Capacity must be a valid integer.";
                return;
            }

            if (!int.TryParse(DrainTextBox.Text, out int drain))
            {
                ValidationMessageTextBlock.Text = "Drain must be a valid integer.";
                return;
            }

            if (!int.TryParse(SkimmerTextBox.Text, out int skimmer))
            {
                ValidationMessageTextBlock.Text = "Skimmer must be a valid integer.";
                return;
            }

            string uri = ThumbnailTextBox.Text;
            if (!ValidateUri(uri))
            {
                MessageBox.Show("Invalid URI");
                return;
            }

            try
            {
                var newPond = new Pond
                {
                    UserId = LoggedInUser.UserId, 
                    Name = PondNameTextBox.Text,
                    Volume = volume,
                    Depth = depth,
                    PumpingCapacity = pumpingCapacity,
                    Drain = drain,
                    Skimmer = skimmer,
                    Thumbnail = ThumbnailTextBox.Text,
                    Note = NoteTextBox.Text
                };

                _pondService.AddPond(newPond);
                MessageBox.Show("Pond added successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding pond: {ex.Message}");
            }
        }
    }
}
