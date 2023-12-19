using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Оборудование с определённой массой
    /// </summary>
    public abstract class Equipment : Costable
    {
        /// <summary>
        /// Масса в кг
        /// </summary>
        [XmlAttribute()]
        [Range(1, int.MaxValue)]
        public int Weight { get; set; }
    }
}
