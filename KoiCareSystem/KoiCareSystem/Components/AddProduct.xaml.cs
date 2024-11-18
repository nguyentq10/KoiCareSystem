using Repositories.Entities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Windows;

namespace KoiCareSystem.Components
{
    public partial class AddProduct : Window
    {
        private readonly ProductService _productService = new();
        private readonly CategoryService _categoryService = new();

        public AddProduct()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = _categoryService.GetAllCategories();
            CategoryComboBox.ItemsSource = categories;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
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

            var newProduct = new ExternalProduct
            {
                Name = NameTextBox.Text,
                Description = DescriptionTextBox.Text,
                Price = price,
                Thumbnail = ThumbnailTextBox.Text,
                ExternalUrl = ExternalUrlTextBox.Text,
                CategoryId = selectedCategory.CategoryId,
                Status = StatusCheckBox.IsChecked ?? false,
                CreatedAt = DateTime.Now
            };

            try
            {
                _productService.AddProduct(newProduct);
                MessageBox.Show("Product added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
