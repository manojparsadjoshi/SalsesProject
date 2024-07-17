using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Entity
{
    public class VenderModel
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public  string Contract {  get; set; }
        public string Address { get; set; }

    }
}
