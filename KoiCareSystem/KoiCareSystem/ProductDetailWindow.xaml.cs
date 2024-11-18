using Repositories.Entities;
using Services.Services;
using System;
using System.Collections;
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
    /// Interaction logic for ProductDetailWindow.xaml
    /// </summary>
    public partial class ProductDetailWindow : Window
    {
        private readonly ProductService _service = new();

        public Repositories.Entities.User? LoggedInUser { get; set; }

        private ExternalProduct _pro;


        public ProductDetailWindow(ExternalProduct pro)
        {
            InitializeComponent();
            _pro = pro;
            DisplayKoiDetails();
        }


        private void DisplayKoiDetails()
        {
            if (_pro == null)
            {
                MessageBox.Show("Product details are not available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ProductThumbnail.Source = !string.IsNullOrEmpty(_pro.Thumbnail) ? new BitmapImage(new Uri(_pro.Thumbnail)) : null;
            ProductName.Text = _pro.Name ?? "N/A";
            Description.Text = _pro.Description.ToString();
            Price.Text = _pro.Price.ToString();
            ExternalUrl.Text = _pro.ExternalUrl.ToString();
            CategoryId.Text = _pro.CategoryId.ToString();
            Status.Text = _pro.Status.ToString();
            CreatedAt.Text = _pro.CreatedAt.ToString();
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
