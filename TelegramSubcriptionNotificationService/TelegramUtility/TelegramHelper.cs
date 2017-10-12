using OpenTl.Schema;
using System;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Net.Core;
using Telegram.Net.Core.MTProto;
using Telegram.Net.Core.Requests;
using Windows.UI.Notifications;

namespace TelegramUtility
{
    public delegate void Callback();
    public class TelegramHelper
    {
        private SessionStore Session;
        private TelegramClient Client;
        private AuthSentCode Hash;
        private Callback CallbackOnConnected;
        private Callback CallbackOnAuthenticated;

        public TelegramHelper(Callback callbackOnConnected, Callback callbackOnAuthenticated)
        {
            Session = new SessionStore();

            Client = new TelegramClient(Session, 194798, "fd58bfc486f125f1677c40af959472c8", new DeviceInfo("D11", "S11", "V11", "en-US"), "149.154.167.40:443");

            CallbackOnConnected = callbackOnConnected;
            CallbackOnAuthenticated = callbackOnAuthenticated;

            Client.UpdateMessage += Client_UpdateMessage;
            Client.ConnectionStateChanged += Client_ConnectionStateChanged;

            Client.Start();
        }

        public async Task RequestForOTPAsync(string registeredMobileNumber)
        {
            try
            {
                if (!Client.IsUserAuthorized())
                {
                    Hash = await Client.SendCode(registeredMobileNumber, VerificationCodeDeliveryType.NumericCodeViaTelegram);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public async Task ValidateOTP(string registeredMobileNumber, string code)
        {
            try
            {
                if (!string.IsNullOrEmpty(Hash.phoneCodeHash))
                {
                    var auth = await Client.SignIn(registeredMobileNumber, Hash.phoneCodeHash, code);
                    if (Client.IsUserAuthorized())
                    {
                        CallbackOnAuthenticated();
                    }
                }
                else
                {
                    throw new Exception("Invalid hash value.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void Client_ConnectionStateChanged(object sender, ConnectionStateEventArgs e)
        {
            if (e.isConnected)
            {
                if (Client.IsUserAuthorized())
                {
                    CallbackOnAuthenticated();
                    //InitUpdateListener();
                }
                else
                {
                    CallbackOnConnected();
                }
            };
        }

        private void Client_UpdateMessage(object sender, Updates e)
        {
            ShowNotification(e.ToString());
        }

        private void ShowNotification(string message)
        {
            string logo = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "\\Cyberoam Auth Manager\\info.ico");
            string toastXml = $@"<toast>
                    <visual>
                        <binding template='ToastGeneric'>
                            <text>Telegram Notification</text>
                            <text>{message}</text>
                            <image src='{logo}' placement='appLogoOverride' hint-crop='circle'/>
                        </binding>
                    </visual>
                </toast>";
            Windows.Data.Xml.Dom.XmlDocument doc = new Windows.Data.Xml.Dom.XmlDocument();
            doc.LoadXml(toastXml);

            ToastNotification toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier("Cyberoam Auth Manager").Show(toast);
        }

        private async void InitUpdateListener()
        {
            var updatesStateRequest = new GetUpdatesStateRequest();
            await Client.SendRpcRequest(updatesStateRequest);
            var initialState = (UpdatesStateConstructor)updatesStateRequest.updatesState;

            var request = new GetUpdatesDifferenceRequest(initialState.pts, initialState.date, initialState.qts);
            await Client.SendRpcRequest(request);
            
        }
    }
}
