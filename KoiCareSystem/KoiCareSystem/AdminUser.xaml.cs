using System.Windows;
using System.Collections.Generic;
using Services.Services;
using Repositories.Entities;
using KoiCareSystem.Components;

namespace KoiCareSystem
{
    public partial class AdminUser : Window
    {
        private readonly UserService _userService = new();

        public Repositories.Entities.User? LoggedInUser { get; set; }

        public AdminUser()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            List<User> users = _userService.GetAllUsers();
            UserDataGrid.ItemsSource = users;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUserWindow = new();
            addUserWindow.ShowDialog();
            LoadUsers();
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                UpdateUser updateUserWindow = new(selectedUser);
                updateUserWindow.ShowDialog();
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Please select a user to update.");
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _userService.DeleteUser(selectedUser);
                    LoadUsers();
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
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

            if (LoggedInUser.Role == "manager") {
                AddUserButton.Visibility = Visibility.Collapsed;
                UpdateUserButton.Visibility = Visibility.Collapsed;
                DeleteUserButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
