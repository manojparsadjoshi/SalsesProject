using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sales.Db;
using Sales.Entity;
using SalsesProject.Models;
using SalsesProject.Models.VM;

namespace Sales.Services.MasterDetail
{
    public class SalesDetailsServices : ISalesDetailsServices

    {
        private readonly ApplicationDbContext _context;
        public SalesDetailsServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(SalesMasterVM model)
        {
            var masterData = new SalesMasterModel()
            {
                Id = 0,
                SalesDate = model.SalesDate,
                CustomerId = model.CustomerId,
                InvoiceNumber = model.InvoiceNumber,
                BillAmount = model.BillAmount,
                Discount = model.Discount,
                NetAmount = model.NetAmount
            };
            var masterdata = _context.masterModels.Add(masterData);
            _context.SaveChanges();

            if (masterdata != null)
            {
                var detail = from a in model.Sales
                             select new SalesDetailsModel()
                             {
                                 Id = 0,
                                 ItemId = a.ItemId,
                                 Unit = a.Unit,
                                 Quantity = a.Quantity,
                                 Price = a.Price,
                                 Amount = a.Amount,
                                 SalesMasterId = masterdata.Entity.Id,
                             };
                _context.DetailsModels.AddRange(detail);
                _context.SaveChanges();

                foreach (var item in model.Sales)
                {


                    var currentItemInfo = _context.itemCurrentInfos.FirstOrDefault(x => x.ItemId == item.ItemId);
                    if (currentItemInfo != null)
                    {
                        currentItemInfo.Quentity -= item.Quantity;
                        _context.itemCurrentInfos.Update(currentItemInfo);
                        _context.SaveChanges();
                    }

                    var historyInfo = new ItemCurrentInfoHistoryModel
                    {
                        Id = 0,
                        ItemId = item.ItemId,
                        Quentity = item.Quantity,
                        TransDate = DateTime.Now,
                        StockInOut = StockInOut.Out,
                        TransactionType = TransactionType.sales
                    };
                    _context.InfoHistoryModels.Add(historyInfo);
                    _context.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public int Delete(int id)
        {
            var existingMasterData = _context.masterModels.Find(id);
            if (existingMasterData == null)
            {
                return 0;
            }
            else
            {
                var existingDetailData = _context.DetailsModels.Where(x => x.SalesMasterId == existingMasterData.Id).ToList();
                if (existingDetailData.Count > 0)
                {
                    foreach (var item in existingDetailData)
                    {

                        var CurrentItemsInfo = _context.itemCurrentInfos.FirstOrDefault(x => x.ItemId == item.ItemId);
                        if (CurrentItemsInfo != null)
                        {
                            CurrentItemsInfo.Quentity -= item.Quantity;
                            _context.itemCurrentInfos.Update(CurrentItemsInfo);
                            _context.SaveChanges();
                        }
                        var historyInfoData = new ItemCurrentInfoHistoryModel
                        {
                            Id = 0,
                            ItemId = item.ItemId,
                            Quentity = item.Quantity,
                            TransDate = DateTime.Now,
                            StockInOut = StockInOut.Out,
                            TransactionType = TransactionType.sales
                        };
                        _context.InfoHistoryModels.Add(historyInfoData);
                        _context.SaveChanges();

                    }
                    _context.DetailsModels.RemoveRange(existingDetailData);
                    _context.SaveChanges();
                };
                _context.masterModels.Remove(existingMasterData);
                _context.SaveChanges();
                return 1;
            }
        }


        public List<SalesMasterVM> GetAll()
        {
            List<SalesMasterVM> datas = new List<SalesMasterVM>();
            var masterdata = _context.masterModels.ToList();
            if (masterdata.Count > 0)
            {
                foreach (var item in masterdata)
                {
                    var customersData = _context.Customers.Find(item.CustomerId);
                    var details = _context.DetailsModels.Where(x => x.SalesMasterId == item.Id).ToList();
                    var data = new SalesMasterVM()
                    {
                        Id = item.Id,
                        SalesDate = item.SalesDate,
                        CustomerId = item.CustomerId,
                        CustomerName = customersData.CustomerName,
                        InvoiceNumber = item.InvoiceNumber,
                        BillAmount = item.BillAmount,
                        Discount = item.Discount,
                        NetAmount = item.NetAmount
                    };
                    data.Sales = (from d in details
                                  select new SalesDetailsVM()
                                  {
                                      Id = d.Id,
                                      ItemId = d.Id,
                                      Unit = d.Unit,
                                      Quantity = d.Quantity,
                                      Price = d.Price,
                                      Amount = d.Amount,
                                  }).ToList();
                    datas.Add(data);
                }
            }
            return datas;
        }

        public SalesMasterVM GetById(int Id)
        {
            var masterdata = _context.masterModels.Find(Id);
            if (masterdata == null)
            {
                return new SalesMasterVM();
            }
            else
            {
                var detail = _context.DetailsModels.Where(x => x.SalesMasterId == masterdata.Id).ToList();
                var data = new SalesMasterVM()
                {
                    Id = masterdata.Id,
                    SalesDate = masterdata.SalesDate,
                    CustomerId = masterdata.CustomerId,
                    InvoiceNumber = masterdata.InvoiceNumber,
                    BillAmount = masterdata.BillAmount,
                    Discount = masterdata.Discount,
                    NetAmount = masterdata.NetAmount
                };
                data.Sales = (from d in detail
                              select new SalesDetailsVM()
                              {
                                  Id = d.Id,
                                  ItemId = d.ItemId,
                                  Unit = d.Unit,
                                  Quantity = d.Quantity,
                                  Price = d.Price,
                                  Amount = d.Amount
                              }).ToList();
                return data;
            }
        }

        public bool Update(SalesMasterVM obj)
        {
            var existingmasterdata = _context.masterModels.Find(obj.Id);
            if (existingmasterdata == null)
            {
                return false;
            }
            else
            {
                existingmasterdata.SalesDate = obj.SalesDate;
                existingmasterdata.CustomerId = obj.CustomerId;
                existingmasterdata.InvoiceNumber = obj.InvoiceNumber;
                existingmasterdata.BillAmount = obj.BillAmount;
                existingmasterdata.Discount = obj.Discount;
                existingmasterdata.NetAmount = obj.NetAmount;

                var masterAdd = _context.masterModels.Update(existingmasterdata);
                _context.SaveChanges();


                var existingdetailsdata = _context.DetailsModels.Where(x => x.SalesMasterId == existingmasterdata.Id);
                foreach (var item in existingdetailsdata)
                {
                    var itemcurrentinfo = _context.itemCurrentInfos.FirstOrDefault(x => x.ItemId == item.ItemId);
                    if (itemcurrentinfo != null)
                    {
                        itemcurrentinfo.Quentity += item.Quantity;
                        _context.itemCurrentInfos.Update(itemcurrentinfo);
                        _context.SaveChanges();
                    }

                    var historyinfo = new ItemCurrentInfoHistoryModel
                    {
                        Id = 0,
                        ItemId = item.ItemId,
                        Quentity = item.Quantity,
                        TransDate = DateTime.Now,
                        StockInOut = StockInOut.In,
                        TransactionType = TransactionType.sales
                    };
                    _context.InfoHistoryModels.Add(historyinfo);
                    _context.SaveChanges();
                };

                _context.DetailsModels.RemoveRange(existingdetailsdata);
                var details = from c in obj.Sales
                              select new SalesDetailsModel
                              {
                                  Id = 0,
                                  ItemId = c.ItemId,
                                  Unit = c.Unit,
                                  Quantity = c.Quantity,
                                  Price = c.Price,
                                  Amount = c.Amount,
                                  SalesMasterId = masterAdd.Entity.Id
                              };
                _context.DetailsModels.AddRange(details);
                _context.SaveChanges();
                foreach (var item in obj.Sales)
                {



                    var currentItemInfo = _context.itemCurrentInfos.FirstOrDefault(x => x.ItemId == item.ItemId);
                    if (currentItemInfo != null)
                    {

                        currentItemInfo.Quentity -= item.Quantity;
                        _context.itemCurrentInfos.Update(currentItemInfo);
                        _context.SaveChanges();


                        var historyInfoData = new ItemCurrentInfoHistoryModel
                        {
                            Id = 0,
                            ItemId = item.ItemId,
                            Quentity = item.Quantity,
                            TransDate = DateTime.Now,
                            StockInOut = StockInOut.In,
                            TransactionType = TransactionType.sales
                        };
                        _context.InfoHistoryModels.Add(historyInfoData);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var newitemsInfo = new ItemCurrentInfo
                        {
                            Id = 0,
                            ItemId = item.ItemId,
                            Quentity = item.Quantity
                        };
                        _context.itemCurrentInfos.Add(newitemsInfo);
                        _context.Add(newitemsInfo);
                        _context.SaveChanges();

                        var historyInfoData = new ItemCurrentInfoHistoryModel
                        {
                            Id = 0,
                            ItemId = item.ItemId,
                            Quentity = item.Quantity,
                            TransDate = DateTime.Now,
                            StockInOut = StockInOut.In,
                            TransactionType = TransactionType.sales
                        };
                        _context.InfoHistoryModels.Add(historyInfoData);
                        _context.SaveChanges();
                    }
                }
                return true;
            };
        }
        public IEnumerable<GetCustomersNameVM> GetCustomersName()
        {
            var customers = _context.Customers.Select(customer => new GetCustomersNameVM
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
            }).ToList();
            return customers;
        }
        public IEnumerable<GetItemsNameVM> GetItemsName()
        {
            var items = _context.Items.Select(item => new GetItemsNameVM
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                Unit = item.Unit              
            }).ToList();
            return items;
        }
    }
}
