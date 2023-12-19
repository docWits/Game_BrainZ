using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Состояние игры
    /// </summary>
    [XmlRoot(ElementName = "State")]
    public class GameState
    {
        /// <summary>
        /// Список компонентов стойки
        /// </summary>
        [XmlElement(ElementName = "Entity")]
        public List<Entity> Entities { get; set; }

        /// <summary>
        /// Суммарная цена стойки
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Резерв нагрузки стойки, кг.
        /// <para>Если меньше нуля, стойка развалится</para>
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public GameState()
        {
            Entities = new List<Entity>();
        }

        /// <summary>
        /// Сохранение (сериализация) в виде XML-файла
        /// </summary>
        /// <param name="name">Имя файла</param>
        public void Save(string name)
        {
            // Объект для выполнения сериалиации
            var ser = new XmlSerializer(GetType());
            // Настройки записи XML-файла
            var settings = new XmlWriterSettings()
            {
                Indent = true
            };
            // Писатель в XML-файл
            using (XmlWriter wrt = XmlWriter.Create(name, settings))
            {
                // Выполнение сериализации
                ser.Serialize(wrt, this);
            }
        }

        /// <summary>
        /// Загрузка (десериализация) состояния игры из XML-файла
        /// </summary>
        /// <param name="name">Имя XML-файла</param>
        /// <returns>При ошибке возвращает состояние по умолчанию</returns>
        public static GameState Load(string name)
        {
            try
            {
                // Объект для выполнения сериалиации
                var ser = new XmlSerializer(typeof(GameState));
                using (XmlReader rdr = XmlReader.Create(name))
                {
                    return (GameState)ser.Deserialize(rdr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сохранение не удалось загрузить!\n{ex.Message}");
                return new GameState();
            }
        }
    }
}
