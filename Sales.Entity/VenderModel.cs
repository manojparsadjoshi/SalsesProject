﻿using System;
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
        public int Id { get; set; }
        public string Name { get; set; }

        [StringLength(10)]
        public  string Contract {  get; set; }
        public string Address { get; set; }

    }
}
