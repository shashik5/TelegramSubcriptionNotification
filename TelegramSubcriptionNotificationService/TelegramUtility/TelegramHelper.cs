using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClient.Core;

namespace TelegramUtility
{
    public static class TelegramHelper
    {
        private static ITelegramClient client = ClientFactory.BuildClient(194798, "fd58bfc486f125f1677c40af959472c8", "149.154.167.40", 443);
        private static OpenTl.Schema.Auth.ISentCode Hash;

        public static async Task RequestForOTPAsync(string registeredMobileNumber)
        {
            try
            {
                await client.ConnectService.ConnectAsync();
                Hash = await client.AuthService.SendCodeRequestAsync(registeredMobileNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task ValidateOTP(string registeredMobileNumber, string code)
        {
            try
            {
                if (Hash.PhoneRegistered)
                {
                    var user = await client.AuthService.MakeAuthAsync(registeredMobileNumber, Hash.PhoneCodeHash, code); 
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
