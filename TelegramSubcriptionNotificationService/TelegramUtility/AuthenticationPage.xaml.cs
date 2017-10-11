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
    /// Interaction logic for AuthenticationPage.xaml
    /// </summary>
    public partial class AuthenticationPage : Page
    {
        private TelegramHelper THelper = (TelegramHelper)Application.Current.Properties["THelper"];

        public AuthenticationPage()
        {
            InitializeComponent();
        }

        private async void ConnctButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await THelper.ValidateOTP(PhoneNumber.Text, OTP.Text);
            }
            catch (Exception)
            {
                PhoneNumber.IsEnabled = true;
                OTPRequestButton.Visibility = Visibility.Visible;
                OTPLabel.Visibility = Visibility.Hidden;
                OTP.Visibility = Visibility.Hidden;
                ConnctButton.Visibility = Visibility.Hidden;
            }
        }

        private async void OTPRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await THelper.RequestForOTPAsync(PhoneNumber.Text);

                PhoneNumber.IsEnabled = false;
                OTPRequestButton.Visibility = Visibility.Hidden;
                OTPLabel.Visibility = Visibility.Visible;
                OTP.Visibility = Visibility.Visible;
                ConnctButton.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                PhoneNumber.IsEnabled = true;
                OTPRequestButton.Visibility = Visibility.Visible;
                OTPLabel.Visibility = Visibility.Hidden;
                OTP.Visibility = Visibility.Hidden;
                ConnctButton.Visibility = Visibility.Hidden;
            }
        }
    }
}
