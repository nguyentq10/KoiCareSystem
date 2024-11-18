using System;
using System.Windows;
using System.Windows.Controls;
using Services.Services;
using Repositories.Entities;

namespace KoiCareSystem
{
    /// <summary>
    /// Interaction logic for AdminCategories.xaml
    /// </summary>
    public partial class AdminCategories : Window
    {
        private readonly CategoryService _categoryService;

        public Repositories.Entities.User? LoggedInUser { get; set; }

        public AdminCategories()
        {
            InitializeComponent();
            _categoryService = new CategoryService();
            LoadCategories();
        }

        private void LoadCategories()
        {
            CategoryDataGrid.ItemsSource = _categoryService.GetAllCategories();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var addCategoryWindow = new Components.AddCategory();
            addCategoryWindow.ShowDialog();
            LoadCategories();
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryDataGrid.SelectedItem is Category selectedCategory)
            {
                var updateCategoryWindow = new Components.UpdateCategory(selectedCategory);
                updateCategoryWindow.ShowDialog();
                LoadCategories();
            }
            else
            {
                MessageBox.Show("Please select a category to update.");
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryDataGrid.SelectedItem is Category selectedCategory)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this category?", "Delete Category", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _categoryService.DeleteCategory(selectedCategory);
                    LoadCategories();
                }
            }
            else
            {
                MessageBox.Show("Please select a category to delete.");
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
                AddCateButton.Visibility = Visibility.Collapsed;
                UpdateCateButton.Visibility = Visibility.Collapsed;
                DeleteCateButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
