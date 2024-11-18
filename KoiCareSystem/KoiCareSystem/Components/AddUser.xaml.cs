using System.Windows;
using Services.Services;
using Repositories.Entities;
using System.Windows.Controls;

namespace KoiCareSystem.Components
{
    public partial class AddUser : Window
    {
        private readonly UserService _userService = new();

        public AddUser()
        {
            InitializeComponent();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User newUser = new()
            {
                UserId = Guid.NewGuid().ToString(),
                Email = EmailTextBox.Text,
                Password = PasswordTextBox.Text,
                Name = NameTextBox.Text,
                Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            _userService.AddUser(newUser);
            MessageBox.Show("User added successfully.");
            Close();
        }
    }
}
