using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TelegramClient;
using TelegramClient.Core;

namespace TelegramSubcriptionNotificationService
{
    public partial class TelegramNotificationService : ServiceBase
    {
        public TelegramNotificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var client = ClientFactory.BuildClient(194798, "fd58bfc486f125f1677c40af959472c8", "149.154.167.40", 443);
        }

        protected override void OnStop()
        {
        }
    }
}
