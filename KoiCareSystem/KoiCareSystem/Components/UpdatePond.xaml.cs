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

namespace KoiCareSystem.Components
{
    /// <summary>
    /// Interaction logic for UpdatePond.xaml
    /// </summary>
    public partial class UpdatePond : Window
    {
        private Pond _pond;
        private PondService _pondService = new();

        public UpdatePond(Pond pond)
        {
            InitializeComponent();
            _pond = pond;
            InitializeFields();
        }

        private void InitializeFields()
        {
            NameTextBox.Text = _pond.Name;
            VolumeTextBox.Text = _pond.Volume.ToString();
            DepthTextBox.Text = _pond.Depth.ToString();
            PumpingCapacityTextBox.Text = _pond.PumpingCapacity.ToString();
            DrainTextBox.Text = _pond.Drain.ToString();
            SkimmerTextBox.Text = _pond.Skimmer.ToString();
            NoteTextBox.Text = _pond.Note;
            ThumbnailTextBox.Text = _pond.Thumbnail;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                _pond.Name = NameTextBox.Text;
                _pond.Volume = int.Parse(VolumeTextBox.Text);
                _pond.Depth = float.Parse(DepthTextBox.Text);
                _pond.PumpingCapacity = int.Parse(PumpingCapacityTextBox.Text);
                _pond.Drain = int.Parse(DrainTextBox.Text);
                _pond.Skimmer = int.Parse(SkimmerTextBox.Text);
                _pond.Note = NoteTextBox.Text;
                _pond.Thumbnail = ThumbnailTextBox.Text;

                _pondService.UpdatePond(_pond);
                MessageBox.Show("Pond updated successfully.");
                this.Close();
            }
        }

        private bool ValidateUri(string uri)
        {
            return Uri.TryCreate(uri, UriKind.Absolute, out Uri? validatedUri)
                   && (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Name is required.");
                return false;
            }

            if (!int.TryParse(VolumeTextBox.Text, out int volume) || volume <= 0)
            {
                MessageBox.Show("Volume must be a positive integer.");
                return false;
            }

            if (!float.TryParse(DepthTextBox.Text, out float depth) || depth <= 0)
            {
                MessageBox.Show("Depth must be a positive number.");
                return false;
            }

            if (!int.TryParse(PumpingCapacityTextBox.Text, out int pumpingCapacity) || pumpingCapacity <= 0)
            {
                MessageBox.Show("Pumping Capacity must be a positive integer.");
                return false;
            }

            if (!int.TryParse(DrainTextBox.Text, out int drain) || drain <= 0)
            {
                MessageBox.Show("Drain must be a positive integer.");
                return false;
            }

            if (!int.TryParse(SkimmerTextBox.Text, out int skimmer) || skimmer <= 0)
            {
                MessageBox.Show("Skimmer must be a positive integer.");
                return false;
            }

            string uri = ThumbnailTextBox.Text;
            if (!ValidateUri(uri))
            {
                MessageBox.Show("Invalid URI");
                return false;
            }

            return true;
        }
    }
}
