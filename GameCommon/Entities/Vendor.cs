using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGameApp.Entities
{
    /// <summary>
    /// Производитель оборудования
    /// </summary>
    public class Vendor : Entity
    {
        /// <summary>
        /// Официальный интернет-сайт производителя
        /// </summary>
        [MaxLength(255)]
        public string Site { get; set; }
    }
}
