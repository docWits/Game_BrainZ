using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Карточка "Сервер"
    /// </summary>
    public class Server : Equipment
    {
        /// <summary>
        /// Количество процессоров
        /// </summary>
        [XmlAttribute(AttributeName = "CPU")]
        public int CPUs { get; set; }

        /// <summary>
        /// Требуемая емкость системы хранения в Гбайт
        /// </summary>
        [XmlAttribute()]
        public int Storage { get; set; }

        /// <summary>
        /// Размер в units
        /// </summary>
        [XmlAttribute()]
        public int Size { get; set; }

        /// <summary>
        /// Количество серверо в отсеке размером 6U
        /// </summary>
        public int Count
        {
            get
            {
                return (Size == 0) ? 0 : 6 / Size;
            }
        }

        public Server()
        {
            ImageUri = new Uri("Resources/server.png", UriKind.Relative);
        }
    }
}
