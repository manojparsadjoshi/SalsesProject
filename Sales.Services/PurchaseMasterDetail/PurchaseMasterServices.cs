﻿using Microsoft.EntityFrameworkCore;
using Sales.Db;
using Sales.Db.Migrations;
using Sales.Entity;
using Sales.Services.PurchaseMasterDetail.ViewModel;
using Sales.Services.Vender.ViewModel;
using SalsesProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchaseMasterModel = Sales.Entity.PurchaseMasterModel;

namespace Sales.Services.PurchaseMasterDetail
{
    public class PurchaseMasterServices : IPurchaseMasterSercices
    {
        private ApplicationDbContext _context;
        public PurchaseMasterServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(PurchaseMasterVM model)
        {
            if (model != null)
            {
                if (model.PurchaseDetail == null)
                {
                    return false;
                }
                var masterpurchase = new PurchaseMasterModel()
                {
                    Id = 0,
                    VenderId = model.VenderId,
                    PurchaseDate = DateTime.Now,
                    InvoiceNumber = model.InvoiceNumber,
                    BillAmount = model.BillAmount,
                    Discount = model.Discount,
                    NetAmount = model.NetAmount
                };
                var masterdata = _context.purchaseMasterModels.Add(masterpurchase);
                _context.SaveChanges();
                if (masterdata != null)
                {
                    var details = from c in model.PurchaseDetail
                                  select new PurchaseDetailModel
                                  {
                                      Id = 0,
                                      ItemId = c.ItemId,
                                      Unit = c.Unit,
                                      Quentity = c.Quentity,
                                      Price = c.Price,
                                      Amount = c.Amount,

                                      PurchaseMasterId = masterdata.Entity.Id
                                  };
                    _context.purchaseMasterDetailModels.AddRange(details);
                    _context.SaveChanges();

                    foreach (var itemdetail in model.PurchaseDetail)
                    {
                        var currentItemInfo = _context.itemCurrentInfos.FirstOrDefault(i => i.ItemId == itemdetail.ItemId);
                        if (currentItemInfo != null)
                        {
                            currentItemInfo.Quentity += itemdetail.Quentity;
                            _context.itemCurrentInfos.Update(currentItemInfo);
                            _context.SaveChanges();
                        }
                        else
                        {
                            var updateinfo = new ItemCurrentInfo
                            {
                                ItemId = itemdetail.ItemId,
                                Quentity = itemdetail.Quentity
                            };
                            _context.itemCurrentInfos.Add(updateinfo);
                            _context.SaveChanges();

                        }
                        var historyEntry = new ItemCurrentInfoHistoryModel
                        {
                            Id = 0,
                            ItemId = itemdetail.ItemId,
                            Quentity = itemdetail.Quentity,
                            TransDate = DateTime.Now,
                            StockInOut = StockInOut.In,
                            TransactionType = TransactionType.purchase
                        };
                        _context.InfoHistoryModels.Add(historyEntry);
                        _context.SaveChanges();
                    }

                }
                return true;
            }
            return false;

        }

        public int Delete(int id)
        {
            var existingMasterData = _context.purchaseMasterModels.Find(id);
            if (existingMasterData == null)
            {
                return 0;
            }
            else
            {
                var existingdetailsdata = _context.purchaseMasterDetailModels.Where(x => x.PurchaseMasterId == existingMasterData.Id).ToList();

                foreach (var item in existingdetailsdata)
                {
                    var existingItemStatus = _context.itemCurrentInfos.FirstOrDefault(x => x.ItemId == item.ItemId);
                    if (existingItemStatus != null)
                    {
                        existingItemStatus.Quentity -= item.Quentity;
                        _context.itemCurrentInfos.Update(existingItemStatus);
                        _context.SaveChanges();
                    }

                    var itemsinfohistory = new ItemCurrentInfoHistoryModel
                    {
                        Id = 0,
                        ItemId = item.ItemId,
                        Quentity = item.Quentity,
                        TransDate = DateTime.Now,
                        StockInOut = StockInOut.Out,
                        TransactionType = TransactionType.purchase
                    };
                    _context.InfoHistoryModels.Add(itemsinfohistory);
                    _context.SaveChanges();

                }
                _context.purchaseMasterDetailModels.RemoveRange(existingdetailsdata);
                _context.SaveChanges();


                _context.purchaseMasterModels.Remove(existingMasterData);
                _context.SaveChanges();
                return id;
            }
        }

        public List<PurchaseMasterVM> GetAll()
        {

            List<PurchaseMasterVM> dataList = new List<PurchaseMasterVM>();
            var existingMasterdatas = _context.purchaseMasterModels.ToList();
            if (existingMasterdatas != null)
            {
                foreach (var masterdata in existingMasterdatas)
                {
                    var detailsdata = _context.purchaseMasterDetailModels.Where(x => x.PurchaseMasterId == masterdata.Id).Include(x => x.Item).ToList();
                    var vendorsdata = _context.venders.FirstOrDefault(x => x.Id == masterdata.VenderId);
                    var masterdatas = new PurchaseMasterVM
                    {
                        Id = masterdata.Id,
                        VenderId = masterdata.VenderId,
                        VendorName = vendorsdata.Name,
                        PurchaseDate = masterdata.PurchaseDate,
                        InvoiceNumber = masterdata.InvoiceNumber,
                        BillAmount = masterdata.BillAmount,
                        Discount = masterdata.Discount,
                        NetAmount = masterdata.NetAmount,
                    };

                    masterdatas.PurchaseDetail = (from d in detailsdata

                                                  select new PurchaseDetailVM
                                                  {
                                                      Id = d.Id,
                                                      ItemId = d.ItemId,
                                                      ItemName = d.Item.ItemName,
                                                      Unit = d.Unit,
                                                      Quentity = d.Quentity,
                                                      Price = d.Price,
                                                      Amount = d.Amount
                                                  }).ToList();
                    dataList.Add(masterdatas);
                }
            }
            return dataList;
        }

        public PurchaseMasterVM GetById(int id)
        {
            var masterdata = _context.purchaseMasterModels.Find(id);
            if (masterdata == null)
            {
                return new PurchaseMasterVM();
            }
            else
            {
                var detaildatas = _context.purchaseMasterDetailModels.Where(x => x.PurchaseMasterId == masterdata.Id).Include(x => x.Item).ToList();
                var vendordatas = _context.venders.FirstOrDefault(x => x.Id == masterdata.VenderId);
                var masterdatas = new PurchaseMasterVM
                {
                    Id = masterdata.Id,
                    VenderId = masterdata.VenderId,
                    VendorName = vendordatas.Name,
                    InvoiceNumber = masterdata.InvoiceNumber,
                    PurchaseDate = masterdata.PurchaseDate,
                    BillAmount = masterdata.BillAmount,
                    Discount = masterdata.Discount,
                    NetAmount = masterdata.NetAmount
                };

                masterdatas.PurchaseDetail = (from detail in detaildatas
                                              select new PurchaseDetailVM
                                              {
                                                  Id = detail.Id,
                                                  ItemId = detail.Item.ItemId,
                                                  ItemName = detail.Item.ItemName,
                                                  Unit = detail.Unit,
                                                  Quentity = detail.Quentity,
                                                  Price = detail.Price,
                                                  Amount = detail.Amount
                                              }).ToList();
                return masterdatas;
            }
        }

        public IEnumerable<GetItemsNameVM> GetItemsNames()
        {
            var data = _context.Items.Select(Item => new GetItemsNameVM
            {
                ItemId = Item.ItemId,
                ItemName = Item.ItemName,
                Unit = Item.Unit
            });
            return data;
        }

        public IEnumerable<GetVendersName> GetVendersNames()
        {
            var data = _context.venders.Select(vender => new GetVendersName
            {
                VenderId = vender.Id,
                VenderName = vender.Name,
            });
            return data;
        }
        public bool Update(PurchaseMasterVM model)
        {
            var existingMasterData = _context.purchaseMasterModels.Find(model.Id);
            if (existingMasterData == null)
            {
                return false;
            }

            // Update master data
            existingMasterData.VenderId = model.VenderId;
            existingMasterData.PurchaseDate = model.PurchaseDate;
            existingMasterData.InvoiceNumber = model.InvoiceNumber;
            existingMasterData.PurchaseDate = model.PurchaseDate;
            existingMasterData.BillAmount = model.BillAmount;
            existingMasterData.Discount = model.Discount;
            existingMasterData.NetAmount = model.NetAmount;

            _context.purchaseMasterModels.Update(existingMasterData);
            _context.SaveChanges();

            // Update and remove existing details
            var existingDetailsData = _context.purchaseMasterDetailModels.Where(x => x.PurchaseMasterId == existingMasterData.Id).ToList();
            _context.purchaseMasterDetailModels.RemoveRange(existingDetailsData);
            _context.SaveChanges();

            // Add updated details
            var detailsdata = model.PurchaseDetail.Select(c => new PurchaseDetailModel
            {
                Id = 0,
                ItemId = c.ItemId,
                Unit = c.Unit,
                Quentity = c.Quentity,
                Price = c.Price,
                Amount = c.Amount,
                PurchaseMasterId = existingMasterData.Id
            }).ToList();

            _context.purchaseMasterDetailModels.AddRange(detailsdata);
            _context.SaveChanges();

            // Update item quantities and history
            foreach (var item in detailsdata)
            {
                var itemcurrentinfo = _context.itemCurrentInfos.FirstOrDefault(x => x.ItemId == item.ItemId);
                if (itemcurrentinfo != null)
                {
                    itemcurrentinfo.Quentity += item.Quentity;
                    _context.itemCurrentInfos.Update(itemcurrentinfo);
                    _context.SaveChanges();

                    var historyinfo = new ItemCurrentInfoHistoryModel
                    {
                        ItemId = item.ItemId,
                        Quentity = item.Quentity,
                        TransDate = DateTime.Now,
                        StockInOut = StockInOut.In,
                        TransactionType = TransactionType.purchase
                    };
                    _context.InfoHistoryModels.Add(historyinfo);
                    _context.SaveChanges();
                }
                else
                {
                    var newitemInfo = new ItemCurrentInfo
                    {
                        ItemId = item.ItemId,
                        Quentity = item.Quentity
                    };
                    _context.itemCurrentInfos.Add(newitemInfo);
                    _context.SaveChanges();

                    var historyinfo = new ItemCurrentInfoHistoryModel
                    {
                        ItemId = item.ItemId,
                        Quentity = item.Quentity,
                        TransDate = DateTime.Now,
                        StockInOut = StockInOut.In,
                        TransactionType = TransactionType.purchase
                    };
                    _context.InfoHistoryModels.Add(historyinfo);
                    _context.SaveChanges();
                }
            }

            return true;
        }

        public List<PurchaseReportVM> GetPurchaseReports()
        {
            var query = from pm in _context.purchaseMasterModels
                        join ps in _context.purchaseMasterDetailModels on pm.Id equals ps.PurchaseMasterId
                        join v in _context.venders on pm.VenderId equals v.Id
                        join i in _context.Items on ps.ItemId equals i.ItemId
                        select new PurchaseReportVM
                        {
                            InvoiceNumber = pm.InvoiceNumber,
                            PurchaseDate = pm.PurchaseDate,
                            VenderName = v.Name,
                            ItemName = i.ItemName,
                            Quentity = ps.Quentity,
                            QuentityPrice = ps.Price,
                            BillAmount = pm.BillAmount,
                            Discount = pm.Discount,
                            NetAmount = pm.NetAmount,
                        };
            return query.ToList();           
        }

    }
}

