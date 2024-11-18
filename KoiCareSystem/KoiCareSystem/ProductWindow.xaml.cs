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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    /// 

    public partial class ProductWindow : Window
    {
        private readonly ProductService _service = new();

        public Repositories.Entities.User? LoggedInUser { get; set; }

        public ProductWindow()
        {
            InitializeComponent();
        }

        private void LoadProduct()
        {
            if (LoggedInUser == null)
            {
                MessageBox.Show("Please log in to view your koi.");
                return;
            }
            var kois = _service.GetAllProducts();
            ProductListBox.ItemsSource = kois;
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
            LoadProduct();
        }

        private void ProductListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductListBox.SelectedItem is ExternalProduct selected)
            {
                ProductDetailWindow detailWindow = new ProductDetailWindow(selected);
                detailWindow.LoggedInUser = LoggedInUser;
                detailWindow.Show();
                this.Close();
            }
        }
    }
}
