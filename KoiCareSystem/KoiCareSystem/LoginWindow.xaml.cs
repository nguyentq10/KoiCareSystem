using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private UserService? _service = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text.IsNullOrEmpty() || PasswordTextBox.Password.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter email and password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_service == null)
            {
                MessageBox.Show("Authentication service is not available.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User? account = _service.Authenticate(UsernameTextBox.Text, PasswordTextBox.Password);

            if (account == null)
            {
                MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }            

            if (account.Role == "member")
            {
                HomeWindow main = new();
                main.LoggedInUser = account;

                main.Show();
                this.Hide();
            }
            else if (account.Role == "manager" || account.Role == "admin")
            {
                AdminUser main = new();
                main.LoggedInUser = account;
                main.Show();
                this.Hide();
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
