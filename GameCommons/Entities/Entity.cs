using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Корневой класс для иерархии оборудования
    /// </summary>
    [XmlRoot(Namespace = "http://www.croc.ru")]
    [XmlInclude(typeof(Server))]
    [XmlInclude(typeof(Rack))]
    [XmlInclude(typeof(KvmConsole))]
    [XmlInclude(typeof(NetworkSwitch))]
    [XmlInclude(typeof(Storage))]
    public abstract class Entity
    {
        /// <summary>
        /// Первичный ключ - уникальный идентификатор
        /// </summary>
        [Key()] // не обязательно, так как используется имя ID
        public Guid ID { get; set; }

        /// <summary>
        /// Картинка сущности
        /// </summary>
        [XmlIgnore()] // не сохраняется в XML-файл
        [NotMapped()] // не отображается на базу данных
        public Uri ImageUri { get; set; }

        /// <summary>
        /// Наименование оборудования
        /// </summary>
        [XmlAttribute()]
        [MaxLength(127)]
        public string Name { get; set; }       

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Entity()
        {
            ID = Guid.NewGuid(); // формирование нового уникального идентификатора
        }
    }
}
