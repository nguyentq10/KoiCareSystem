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
    /// Interaction logic for UpdateKoi.xaml
    /// </summary>
    public partial class UpdateKoi : Window
    {
        private Koi _koi;
        private KoiService _koiService;

        public UpdateKoi(Koi koi)
        {
            InitializeComponent();
            _koi = koi;
            _koiService = new KoiService();
            InitializeFields();
        }

        private void InitializeFields()
        {
            NameTextBox.Text = _koi.Name;
            AgeTextBox.Text = _koi.Age.ToString();
            LengthTextBox.Text = _koi.Length.ToString();
            WeightTextBox.Text = _koi.Weight.ToString();
            ColorTextBox.Text = _koi.Color;
            VarietyTextBox.Text = _koi.Variety;
            SexTextBox.Text = _koi.Sex;
            ThumbnailTextBox.Text = _koi.Thumbnail;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                _koi.Name = NameTextBox.Text;
                _koi.Age = int.Parse(AgeTextBox.Text);
                _koi.Length = int.Parse(LengthTextBox.Text);
                _koi.Weight = float.Parse(WeightTextBox.Text);
                _koi.Color = ColorTextBox.Text;
                _koi.Variety = VarietyTextBox.Text;
                _koi.Sex = SexTextBox.Text;
                _koi.Thumbnail = ThumbnailTextBox.Text;

                _koiService.UpdateKoi(_koi);
                MessageBox.Show("Koi details updated successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill in all fields correctly.");
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

            if (!int.TryParse(AgeTextBox.Text, out int age) || age < 0)
            {
                MessageBox.Show("Please enter a valid age.");
                return false;
            }

            if (!int.TryParse(LengthTextBox.Text, out int length) || length < 0)
            {
                MessageBox.Show("Please enter a valid length.");
                return false;
            }

            if (!float.TryParse(WeightTextBox.Text, out float weight) || weight < 0)
            {
                MessageBox.Show("Please enter a valid weight.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ColorTextBox.Text))
            {
                MessageBox.Show("Color is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(VarietyTextBox.Text))
            {
                MessageBox.Show("Variety is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(SexTextBox.Text))
            {
                MessageBox.Show("Sex is required.");
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
