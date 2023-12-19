using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Карточка "Шкаф"
    /// </summary>
    public class Rack : Costable
    {
         /// <summary>
        /// Количество серверов
        /// </summary>
        [XmlAttribute()]
        public int Count { get; set; }

        /// <summary>
        /// Нагрузка, кг
        /// </summary>
        [XmlAttribute()]
        public int Capacity { get; set; }              

        public Rack()
        {
            ImageUri = new Uri("Resources/rack.png", UriKind.Relative);
        }
    }
}
