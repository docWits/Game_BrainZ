using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Именованный элемент со стоимостью
    /// </summary>
    public class Costable : Entity
    {
        /// <summary>
        /// Стоимость
        /// </summary>
        private int price;

        /// <summary>
        /// Стоимость
        /// </summary>
        [XmlAttribute()]
        [Range(0, int.MaxValue)]
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Цена не может быть отрицательной");
                }
                price = value;
            }
        }

        /// <summary>
        /// Производитель оборудования
        /// </summary>       
        public Vendor Vendor { get; set; }

        /// <summary>
        /// Идентификатор производителя оборудования
        /// </summary>
        public Guid? VendorID { get; set; }
    }
}
