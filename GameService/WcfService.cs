using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WpfGameApp;

namespace GameService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class WcfService : IWcfService
    {
        /// <summary>
        /// Протоколирование
        /// </summary>
        private Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns>Признак успешности регистрации</returns>
        public bool Register(string name)
        {
            try
            {
                log.Debug($"Register ({name})");
                using (var db = new DB())
                {
                    var user = db.Players.Any(x => x.Name == name);
                    if (!user)
                    {
                        db.Players.Add(new WpfGameApp.Entities.Player() { Name = name });
                        db.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                log.WithProperty("EventID",(int)EventID.Exception).Warn(ex);
                return false;
            }
        }

        /// <summary>
        /// Состояние сервиса
        /// </summary>
        /// <returns></returns>
        public bool Heartbeat()
        {
           log.Debug("Heartbeat");
            return true;
        }

        /// <summary>
        /// Отмена регистрации пользователя
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Unregister(string name)
        {
            try
            {
                log.Debug($"Unregister ({name})");
                using (var db = new DB())
                {
                    var user = db.Players.Where(x => x.Name == name).FirstOrDefault();
                    if (user == null)
                    {
                        return false;
                    }
                    db.Players.Remove(user);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return false;
            }
        }
    }
}
