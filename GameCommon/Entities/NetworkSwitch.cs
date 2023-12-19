using System;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Карточка "Сетевой коммутатор"
    /// </summary>
    public class NetworkSwitch : Equipment
    {
        /// <summary>
        /// Количество портов
        /// </summary>
        [XmlAttribute()]
        public int Count { get; set; }
               
        public NetworkSwitch()
        {
            ImageUri = new Uri("Resources/networkSwitch.png", UriKind.Relative);
        }
    }
}
