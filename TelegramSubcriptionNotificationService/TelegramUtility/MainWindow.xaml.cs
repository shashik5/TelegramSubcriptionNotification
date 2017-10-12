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

namespace TelegramUtility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsAuthenticationPageLoaded = false;
        private bool IsWindowMinimized = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TelegramUtilityWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Callback onClientConnected = OnClientConnected, onUserAuthenticated = OnUserAuthenticated;
            TelegramHelper THelper = new TelegramHelper(onClientConnected, onUserAuthenticated);
            Application.Current.Properties.Add("THelper", THelper);
        }

        private void OnClientConnected()
        {
            if (!IsAuthenticationPageLoaded)
            {
                MainFrame.NavigationService.Navigate(new AuthenticationPage());
                IsAuthenticationPageLoaded = true;
            }
        }

        private void OnUserAuthenticated()
        {
            if (!IsWindowMinimized)
            {
                Visibility = Visibility.Hidden;
                IsWindowMinimized = true;
            }
        }
    }
}
