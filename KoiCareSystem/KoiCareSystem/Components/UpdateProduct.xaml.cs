using Repositories.Entities;
using Services.Services;
using System;
using System.Windows;

namespace KoiCareSystem.Components
{
    public partial class UpdateProduct : Window
    {
        private readonly ProductService _productService = new();
        private readonly CategoryService _categoryService = new();
        private readonly ExternalProduct _product;

        public UpdateProduct(ExternalProduct product)
        {
            InitializeComponent();
            _product = product;
            LoadCategories();
            LoadProductDetails();
        }

        private void LoadCategories()
        {
            var categories = _categoryService.GetAllCategories();
            CategoryComboBox.ItemsSource = categories;
        }

        private void LoadProductDetails()
        {
            NameTextBox.Text = _product.Name;
            DescriptionTextBox.Text = _product.Description;
            PriceTextBox.Text = _product.Price.ToString();
            ThumbnailTextBox.Text = _product.Thumbnail;
            ExternalUrlTextBox.Text = _product.ExternalUrl;
            CategoryComboBox.SelectedValue = _product.CategoryId;
            StatusCheckBox.IsChecked = _product.Status;
        }

        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                CategoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(PriceTextBox.Text, out var price))
            {
                MessageBox.Show("Invalid price format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedCategory = (Category)CategoryComboBox.SelectedItem;

            _product.Name = NameTextBox.Text;
            _product.Description = DescriptionTextBox.Text;
            _product.Price = price;
            _product.Thumbnail = ThumbnailTextBox.Text;
            _product.ExternalUrl = ExternalUrlTextBox.Text;
            _product.CategoryId = selectedCategory.CategoryId;
            _product.Status = StatusCheckBox.IsChecked ?? false;

            try
            {
                _productService.UpdateProduct(_product);
                MessageBox.Show("Product updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
