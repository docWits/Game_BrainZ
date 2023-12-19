using System;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Карточка "Система хранения"
    /// </summary>
    public class Storage : Equipment
    {
        /// <summary>
        /// Ёмкость в ТБайт
        /// </summary>
        [XmlAttribute()]
        public int Size { get; set; }     

        public Storage()
        {
            ImageUri = new Uri("Resources/storage.png", UriKind.Relative);
        }
    }
}
