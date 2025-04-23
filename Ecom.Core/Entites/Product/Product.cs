using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entites.Product
{
    public class Product : BaseEntity<int>
    {
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public virtual List<Photo> photos { get; set; }

        [ForeignKey(nameof(categoryId))]
        public int categoryId { get; set; }
        public Category category { get; set; }
    }
}
