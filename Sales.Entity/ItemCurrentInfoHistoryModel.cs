﻿using SalsesProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sales.Entity
{
    public class ItemCurrentInfoHistoryModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        [JsonIgnore]
        public ItemsModel Item { get; set; }
        public int Quentity {  get; set; }
        public DateTime TransDate { get; set; }

         public StockInOut StockInOut { get; set;}
        public TransactionType TransactionType { get; set; }
    }
    public enum StockInOut
    {
        In,
        Out
    }
    public enum TransactionType
    {
        purchase,
        sales
    }
}