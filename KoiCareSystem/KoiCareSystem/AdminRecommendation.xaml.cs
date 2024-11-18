using KoiCareSystem.Components;
using Repositories.Entities;
using Services.Services;
using System.Windows;

namespace KoiCareSystem
{
    public partial class AdminRecommendation : Window
    {
        private readonly ProductService _productService = new();

        public Repositories.Entities.User? LoggedInUser { get; set; }
        public AdminRecommendation()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            RecommendationDataGrid.ItemsSource = _productService.GetAllProducts();
        }

        private void AddRecommendation_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProduct();
            addProductWindow.ShowDialog();
            LoadProducts();
        }

        private void UpdateRecommendation_Click(object sender, RoutedEventArgs e)
        {
            if (RecommendationDataGrid.SelectedItem is ExternalProduct selectedProduct)
            {
                var updateProductWindow = new UpdateProduct(selectedProduct);
                updateProductWindow.ShowDialog();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Please select a product to update.");
            }
        }

        private void DeleteRecommendation_Click(object sender, RoutedEventArgs e)
        {
            if (RecommendationDataGrid.SelectedItem is ExternalProduct selectedProduct)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _productService.DeleteProduct(selectedProduct);
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoggedInUser != null)
            {
                AdminSidebar.LoggedInUser = LoggedInUser;
            }
            else
            {
                MessageBox.Show("Logged in user information is missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            if (LoggedInUser.Role == "manager")
            {
                AddProductButton.Visibility = Visibility.Collapsed;
                UpdateProductButton.Visibility = Visibility.Collapsed;
                DeleteProductButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
