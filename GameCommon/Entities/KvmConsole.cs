using System;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Карточка "Консоль KVM (keyboard-mouse-video)"
    /// </summary>
    [Table("KvmModules")]
    public class KvmConsole : Equipment
    {
        /// <summary>
        /// Количество серверов
        /// </summary>
        [XmlAttribute()]
        public int Count { get; set; }

        public KvmConsole()
        {
            ImageUri = new Uri("Resources/kvm.png", UriKind.Relative);
        }
    }
}
