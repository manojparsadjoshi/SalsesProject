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
                    VenderId = model.VendorId,
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
                            Quentity
                            = itemdetail.Quentity,
                            TransDate = DateTime.Now,
                            StockInOut = StockInOut.In,
                            TransactionType = TransactionType.sales
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
            throw new NotImplementedException();
        }

        public List<PurchaseMasterVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public PurchaseMasterVM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetItemsNameVM> GetItemsNames()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetVendersName> GetVendersNames()
        {
            throw new NotImplementedException();
        }

        public bool Update(PurchaseMasterVM model)
        {
            throw new NotImplementedException();
        }
    }
}
