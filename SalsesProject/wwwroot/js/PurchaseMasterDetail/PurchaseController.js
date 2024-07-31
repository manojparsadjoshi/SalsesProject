/// <reference path="../knockout.js" />
/// <reference path="purchasemodel.js" />

var purchasemasterdetailcontroller = function () {
    var self = this;
    const baseUrl = "/api/PurchaseMasterDetailAPI";
    self.PurchaseMasterDetailList = ko.observableArray([]);
    self.VendorsNameList = ko.observableArray([]);
    self.ItemsNameList = ko.observableArray([]);
    self.selectedPurchase = ko.observableArray([]);
    self.NewPurchaseOrder = ko.observable(new masterpurchaseVM());
    self.IsUpdated = ko.observable(false);
    self.purchaseToDelete = ko.observable();

    self.getData = function () {
        console.log("Fetching data...");
        ajax.get(baseUrl).then(function (result) {
            console.log("Data received:", result);
            self.PurchaseMasterDetailList(result.map(item => new masterpurchaseVM(item)));
            console.log("PurchaseMasterDetailList updated:", self.PurchaseMasterDetailList());
        }).catch(function (error) {
            console.error("Error fetching data:", error);
        });
    }

    self.AddPurchase = function () {
        var purchaseData = ko.toJS(self.NewPurchaseOrder());

        // Check if there are any detail items
        if (!purchaseData.purchaseDetail || purchaseData.purchaseDetail.length === 0) {
            toastr.error("Some field is required!");
            alert("Add at least one item.");
            return;
        }

        if (self.IsUpdated()) {
            ajax.put(baseUrl, JSON.stringify(purchaseData))
                .done(function (result) {
                    var updatedPurchase = new masterpurchaseVM(result);
                    var index = self.PurchaseMasterDetailList().findIndex(function (item) {
                        return item.id() === updatedPurchase.id();
                    });
                    if (index >= 0) {
                        self.PurchaseMasterDetailList().replace(self.PurchaseMasterDetailList()[index], updatedPurchase);
                    }
                    self.resetForm();
                    self.getData();
                    $('#purchaseModal').modal('hide');
                    toastr.success(" Purchase Update Successfully.");
                })
                .fail(function (err) {
                    console.error("Error updating purchase:", err);
                    if (err.responseJSON && err.responseJSON.message) {
                        alert("Error: " + err.responseJSON.message);
                    } else {
                        alert("An error occurred while updating the purchase. Please check all inputs and try again.");
                    }
                });
        } else {
            ajax.post(baseUrl, ko.toJSON(self.NewPurchaseOrder()))
                .done(function (result) {
                    self.PurchaseMasterDetailList.push(new masterpurchaseVM(result));
                    self.resetForm();
                    self.getData();
                    $('#purchaseModal').modal('hide');
                    toastr.success(" New Purchase Add Successfully.");
                })
                .fail(function (err) {
                    console.error("Error adding purchase:", err);
                    if (err.responseJSON && err.responseJSON.message) {
                        alert("Error: " + err.responseJSON.message);
                    } else {
                        alert("An error occurred while adding the purchase. Please check all inputs and try again.");
                    }
                });
        }
    }

    self.DeletePurchase = function (model) {
        self.purchaseToDelete(model);
        setTimeout(function () {
            $('#deleteConfirmModal').modal('show');
        }, 100);
    };
    self.confirmDelete = function () {
        var model = self.purchaseToDelete();
        if (model) {
            ajax.delete(baseUrl + "?id=" + model.id())
                .done(function () {
                    self.PurchaseMasterDetailList.remove(function (item) {
                        return item.id() === model.id();
                    });
                    $('#deleteConfirmModal').modal('hide');
                    toastr.success("Purchase Delete Successfully.");
                })
                .fail(function (err) {
                    console.error("Error deleting purchase:", err);
                    $('#deleteConfirmModal').modal('hide');
                });
        }
    };

    //self.DeletePurchase = function (model) {
    //    ajax.delete(baseUrl + "?id=" + model.id())
    //        .done(function () {
    //            self.PurchaseMasterDetailList.remove(function (item) {
    //                return item.id() === model.id();
    //            });
    //        }).fail(function (err) {
    //            console.error("Error deleting purchase:", err);
    //        });
    //};
    //Delete functionality
    //self.DeleteCustomer = function (model) {
    //    self.customerToDelete(model);
    //    setTimeout(function () {
    //        $('#deleteConfirmModal').modal('show');
    //    }, 100);
    //};
    //self.confirmDelete = function () {
    //    var model = self.customerToDelete();
    //    if (model) {
    //        ajax.delete(baseUrl + "/id?id=" + model.customerId())
    //            .done((result) => {
    //                self.customerList.remove(model);
    //                $('#deleteConfirmModal').modal('hide');
    //            })
    //            .fail((err) => {
    //                console.log(err);
    //                $('#deleteConfirmModal').modal('hide');
    //            });
    //    }
    //};

    self.SelectVendor = function (model) {
        var purchasedata = ko.toJS(model);
        var newPurchaseData = new masterpurchaseVM(purchasedata);
        newPurchaseData.purchaseDetail(purchasedata.purchaseDetail.map(function (detail) {
            return new detailpurchaseVM(detail);
        }));

        self.NewPurchaseOrder(newPurchaseData);
        self.IsUpdated(true);
        $('#purchaseModal').modal('show');
        ko.tasks.runEarly();
    }

    self.getVendorsName = function () {
        var url = baseUrl + "/VenderName";
        ajax.get(url).then(function (result) {
            self.VendorsNameList(result.map(item => new vendornamemodel(item)));
            console.log("Vendor list:", self.VendorsNameList());
        }).catch(function (error) {
            console.error("Error fetching vendors:", error);
            alert("Failed to load vendors. Please refresh the page.");
        });
    }

    self.getItemsName = function () {
        var url = baseUrl + "/ItemName";
        ajax.get(url).then(function (result) {
            console.log("Item data received:", result);
            self.ItemsNameList(result.map(item => new itemnamemodel({
                itemId: item.itemId,
                itemName: item.itemName,
                unit: item.unit
            })));
            console.log("Mapped item list:", self.ItemsNameList());
        }).catch(function (error) {
            console.error("Error fetching items:", error);
        });
    }

    self.AddItem = function () {
        console.log("Adding new item...");
        self.NewPurchaseOrder().purchaseDetail.push(new detailpurchaseVM());
    };

    self.updateUnit = function (item, event) {
        var selectedItemId = event.target.value;
        var selectedItem = self.ItemsNameList().find(function (item) {
            return item.itemId() == selectedItemId;
        });
        if (selectedItem) {
            item.unit(selectedItem.unit());
        }
    };

    self.removeItem = function (item) {
        self.NewPurchaseOrder().purchaseDetail.remove(item);
        self.recalculateAmounts(); // Recalculate amounts after removing the item
    };

    self.recalculateAmounts = function () {
        var totalAmount = 0;
        self.NewPurchaseOrder().purchaseDetail().forEach(function (detail) {
            detail.amount(detail.quentity() * detail.price());
            totalAmount += detail.amount();
        });
        self.NewPurchaseOrder().billAmount(totalAmount);
        self.NewPurchaseOrder().netAmount(totalAmount - self.NewPurchaseOrder().discount());
    };

    self.setCreateMode = function () {
        self.resetForm();
        $('#purchaseModal').modal('show');
    };

    self.resetForm = () => {
        self.NewPurchaseOrder(new masterpurchaseVM());
        self.IsUpdated(false);
    };

    // Initialize data
    self.getData();
    self.getVendorsName();
    self.getItemsName();
}

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url,
            async: true,
        });
    },
    post: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "POST",
            url: url,
            data: data
        });
    },
    put: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "PUT",
            url: url,
            data: data
        });
    },
    delete: function (route) {
        return $.ajax({
            method: "DELETE",
            url: route,
        });
    }
};
