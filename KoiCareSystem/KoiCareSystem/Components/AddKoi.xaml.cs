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
    /// Interaction logic for AddKoi.xaml
    /// </summary>
    public partial class AddKoi : Window
    {
        private readonly KoiService _koiService = new();

        public Repositories.Entities.User? LoggedInUser { get; set; }

        private Pond _pond;

        public AddKoi(Pond pond)
        {
            _pond = pond;
            InitializeComponent();
        }

        private bool ValidateUri(string uri)
        {
            return Uri.TryCreate(uri, UriKind.Absolute, out Uri? validatedUri)
                   && (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
        }

        private void AddKoiButton_Click(object sender, RoutedEventArgs e)
        {
            ValidationMessageTextBlock.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(KoiNameTextBox.Text))
            {
                ValidationMessageTextBlock.Text = "Koi Name is required.";
                return;
            }

            if (!int.TryParse(AgeTextBox.Text, out int age))
            {
                ValidationMessageTextBlock.Text = "Age must be a valid integer.";
                return;
            }

            if (!int.TryParse(LengthTextBox.Text, out int length))
            {
                ValidationMessageTextBlock.Text = "Length must be a valid integer.";
                return;
            }

            if (!float.TryParse(WeightTextBox.Text, out float weight))
            {
                ValidationMessageTextBlock.Text = "Weight must be a valid number.";
                return;
            }

            if (string.IsNullOrWhiteSpace(ColorTextBox.Text))
            {
                ValidationMessageTextBlock.Text = "Color is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(VarietyTextBox.Text))
            {
                ValidationMessageTextBlock.Text = "Variety is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(SexTextBox.Text))
            {
                ValidationMessageTextBlock.Text = "Sex is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(OriginTextBox.Text))
            {
                ValidationMessageTextBlock.Text = "Origin is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(PhysiqueTextBox.Text))
            {
                ValidationMessageTextBlock.Text = "Physique is required.";
                return;
            }

            if (_pond == null)
            {
                ValidationMessageTextBlock.Text = "Pond information is missing.";
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
                var newKoi = new Koi
                {
                    UserId = LoggedInUser.UserId,
                    PondId = _pond.PondId,
                    Name = KoiNameTextBox.Text,
                    Age = age,
                    Length = length,
                    Weight = weight,
                    Color = ColorTextBox.Text,
                    Variety = VarietyTextBox.Text,
                    Sex = SexTextBox.Text,
                    Origin = OriginTextBox.Text,
                    Physique = PhysiqueTextBox.Text,
                    Thumbnail = ThumbnailTextBox.Text,
                    Note = NoteTextBox.Text
                };

                _koiService.AddKoi(newKoi);
                MessageBox.Show("Koi added successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding koi: {ex.Message}");
            }
        }
    }
}
