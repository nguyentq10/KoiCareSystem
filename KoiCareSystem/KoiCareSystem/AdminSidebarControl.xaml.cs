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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KoiCareSystem
{
    /// <summary>
    /// Interaction logic for AdminSidebarControl.xaml
    /// </summary>
    public partial class AdminSidebarControl : UserControl
    {
        public Repositories.Entities.User? LoggedInUser { get; set; }
        public AdminSidebarControl()
        {
            InitializeComponent();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            AdminUser adminUser = new();
            adminUser.LoggedInUser = LoggedInUser;
            adminUser.Show();
            Window.GetWindow(this)?.Close();
        }

        private void CategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            AdminCategories adminCategories = new();
            adminCategories.LoggedInUser = LoggedInUser;
            adminCategories.Show();
            Window.GetWindow(this)?.Close();
        }

        private void RecommendationButton_Click(object sender, RoutedEventArgs e)
        {
            AdminRecommendation adminRecommendation = new();
            adminRecommendation.LoggedInUser = LoggedInUser;
            adminRecommendation.Show();
            Window.GetWindow(this)?.Close();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new();
            loginWindow.Show();
            Window.GetWindow(this)?.Close();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
