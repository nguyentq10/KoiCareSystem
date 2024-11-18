using KoiCareSystem.Components;
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
    /// Interaction logic for KoiWindow.xaml
    /// </summary>
    public partial class KoiWindow : Window
    {
        private readonly KoiService _koiService = new();

        public Repositories.Entities.User? LoggedInUser { get; set; }

        public KoiWindow()
        {
            InitializeComponent();
        }

        private void LoadKois()
        {
            if(LoggedInUser == null)
            {
                MessageBox.Show("Please log in to view your koi.");
                return;
            }
            var kois = _koiService.GetKoisByUserId(LoggedInUser.UserId);
            KoiListBox.ItemsSource = kois;
        }

        private void KoiListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KoiListBox.SelectedItem is Koi selectedKoi)
            {
                KoiDetailWindow detailWindow = new KoiDetailWindow(selectedKoi);
                detailWindow.LoggedInUser = LoggedInUser;
                detailWindow.Show();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoggedInUser != null)
            {
                Sidebar.LoggedInUser = LoggedInUser;
            }
            else
            {
                MessageBox.Show("Logged in user information is missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            LoadKois();
        }
    }
}
