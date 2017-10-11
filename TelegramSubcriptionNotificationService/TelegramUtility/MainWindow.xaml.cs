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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TelegramUtilityWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OnConnected onClientConnected = OnClientConnected;
            TelegramHelper THelper = new TelegramHelper(onClientConnected);
            Application.Current.Properties.Add("THelper", THelper);
        }

        private void OnClientConnected()
        {
            MainFrame.NavigationService.Navigate(new AuthenticationPage());
        }
    }
}
