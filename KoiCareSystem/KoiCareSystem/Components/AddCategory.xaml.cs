using System.Windows;
using Repositories.Entities;
using Services.Services;

namespace KoiCareSystem.Components
{
    public partial class AddCategory : Window
    {
        private readonly CategoryService _categoryService;

        public AddCategory()
        {
            InitializeComponent();
            _categoryService = new CategoryService();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Category name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newCategory = new Category
            {
                Name = NameTextBox.Text,
                Description = DescriptionTextBox.Text,
                CreatedAt = DateTime.Now
            };

            try
            {
                _categoryService.AddCategory(newCategory);
                MessageBox.Show("Category added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the category: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
