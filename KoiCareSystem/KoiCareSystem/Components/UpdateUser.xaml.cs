using System.Windows;
using Services.Services;
using Repositories.Entities;
using System.Windows.Controls;

namespace KoiCareSystem.Components
{
    public partial class UpdateUser : Window
    {
        private readonly UserService _userService = new();
        private readonly User _user;

        public UpdateUser(User user)
        {
            InitializeComponent();
            _user = user;
            LoadUserData();
        }

        private void LoadUserData()
        {
            EmailTextBox.Text = _user.Email;
            PasswordTextBox.Text = _user.Password;
            NameTextBox.Text = _user.Name;
            RoleComboBox.SelectedItem = RoleComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == _user.Role);
        }

        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _user.Email = EmailTextBox.Text;
            _user.Password = PasswordTextBox.Text;
            _user.Name = NameTextBox.Text;
            _user.Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            _userService.UpdateUser(_user);
            MessageBox.Show("User updated successfully.");
            Close();
        }
    }
}
