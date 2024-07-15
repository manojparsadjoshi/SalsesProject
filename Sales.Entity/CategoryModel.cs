using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Entity
{
    internal class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Vegetable { get; set; }
        public string Fruit { get; set; }
        public string Meat { get; set; }
    }
}
