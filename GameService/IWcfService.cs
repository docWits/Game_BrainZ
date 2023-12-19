using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GameService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IWcfService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IWcfService
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns>Признак успешности регистрации</returns>
        [OperationContract()]
        bool Register(string name);
        
        [OperationContract()]
        bool Heartbeat();

        [OperationContract()]
        bool Unregister(string name);
    }

}
