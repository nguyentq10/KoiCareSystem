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
    /// Interaction logic for PondWindow.xaml
    /// </summary>
    public partial class PondWindow : Window
    {
        private readonly PondService _pondService = new();

        public Repositories.Entities.User? LoggedInUser { get; set; }

        
        public PondWindow()
        {
            InitializeComponent();
        }

        private void LoadPonds()
        {
            if (LoggedInUser == null) {
                MessageBox.Show("Please log in to view your ponds.");
                return;
            }
            var ponds = _pondService.GetPondsByUserId(LoggedInUser.UserId);
            PondListBox.ItemsSource = ponds;
        }

        private void PondListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PondListBox.SelectedItem is Pond selectedPond)
            {
                PondDetailWindow detailWindow = new PondDetailWindow(selectedPond);
                detailWindow.LoggedInUser = LoggedInUser;
                detailWindow.Show();
                this.Close();
            }
        }

        private void AddPondButton_Click(object sender, RoutedEventArgs e)
        {
            AddPond addPond = new();
            addPond.LoggedInUser = LoggedInUser;
            addPond.ShowDialog();
            LoadPonds();
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
            LoadPonds();
        }
    }
}