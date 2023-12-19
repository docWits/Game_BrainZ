using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GameService
{
    partial class AppService : ServiceBase
    {
        /// <summary>
        /// Протоколирование
        /// </summary>
        private Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Сервис
        /// </summary>
        private WcfService service = new WcfService();

        /// <summary>
        /// Домик для сервиса
        /// </summary>
        private ServiceHost host;

        public AppService()
        {
            InitializeComponent();
            host = new ServiceHost(service);
        }

        /// <summary>
        /// Запуск сервиса вручную
        /// </summary>
        public void Start()
        {
            OnStart(null);
        }


        /// <summary>
        /// Событие запуска служба
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            host.Open();
            var db = new WpfGameApp.DB();
            log.WithProperty("EventID", (int)EventID.Start).Info($"Сервис запущен, в игре {db.Players.Count()} игроков");
        }

        /// <summary>
        /// Событие останова службы
        /// </summary>
        protected override void OnStop()
        {
            host.Close();
            log.WithProperty("EventID", (int)EventID.Stop).Info("Сервис остановлен");
        }

        /// <summary>
        /// Событие приостанова службы
        /// </summary>
        protected override void OnPause()
        {
            host.Close();
            log.WithProperty("EventID", (int)EventID.Pause).Info("Сервис приостановлен");
        }

        /// <summary>
        /// Событие возобновления службы
        /// </summary>
        protected override void OnContinue()
        {
            host.Open();
            log.WithProperty("EventID", (int)EventID.Continue).Info("Сервис возобновлён");
        }
    }
}
