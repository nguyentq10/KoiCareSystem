using System.Windows;
using Repositories.Entities;
using Services.Services;

namespace KoiCareSystem.Components
{
    public partial class UpdateCategory : Window
    {
        private readonly CategoryService _categoryService;
        private readonly Category _category;

        public UpdateCategory(Category category)
        {
            InitializeComponent();
            _categoryService = new CategoryService();
            _category = category;
            LoadCategoryDetails();
        }

        private void LoadCategoryDetails()
        {
            NameTextBox.Text = _category.Name;
            DescriptionTextBox.Text = _category.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _category.Name = NameTextBox.Text;
            _category.Description = DescriptionTextBox.Text;

            try
            {
                _categoryService.UpdateCategory(_category);
                MessageBox.Show("Category updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the category: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
