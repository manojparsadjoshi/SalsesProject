﻿/// <reference path="../knockout.js" />

const mode = {
    create: 1,
    update: 2
};

var masterdetailsController = function () {
    var self = this;
    const baseUrl = "/api/SalesDetailsAPIServices";
    self.SalesList = ko.observableArray([]);
    self.CustomersNameList = ko.observableArray([]);
    self.ItemsNameList = ko.observableArray([]);
    self.NewSales = ko.observable(new mastermodelVM());
    self.SelectedSales = ko.observable(new mastermodelVM());
    self.IsUpdated = ko.observable(false);
    self.purchaseToDelete = ko.observable(null);

    // Get All Data
    self.getData = function () {
        ajax.get(baseUrl + "/GetAll").then(function (result) {
            self.SalesList(result.map(item => new mastermodelVM(item)));
        });
    };
    self.getData();
    masterdetailsController.ItemsNameList = self.ItemsNameList;

    // Get Customer Names
    self.getCustomersName = function () {
        var url = baseUrl + "/GetCustomersName";
        return ajax.get(url).then(function (data) {
            var mappedProducts = ko.utils.arrayMap(data, (item) => {
                return new customernamemodel(item);
            });
            self.CustomersNameList(mappedProducts);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching customersname: ", textStatus, errorThrown);
        });
    };
    self.getCustomersName();

    // Get Item Names
    self.getItemsName = function () {
        var url = baseUrl + "/GetItemsName";
        return ajax.get(url).then(function (data) {
            var mappedProducts = ko.utils.arrayMap(data, (item) => {
                return new itemnamemodel({
                    itemId: item.itemId,
                    itemName: item.itemName,
                    unit: item.unit
                });
            });
            self.ItemsNameList(mappedProducts);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching itemsname: ", textStatus, errorThrown);
        });
    };
    self.getItemsName();

    // Add Sales
    self.AddSales = function () {
        var salesData = ko.toJS(self.NewSales());

        // Check if there are any valid detail items
        var validItems = salesData.sales.filter(item => item.itemId && item.quantity > 0);
        if (validItems.length === 0) {
            alert("Please add at least one valid item to the sale.");
            return;
        }

        // Proceed with adding / updating only valid items
        salesData.sales = validItems;

        if (self.IsUpdated()) {
            ajax.put(baseUrl + "/Update", JSON.stringify(salesData))
                .done(function (result) {
                    if (result) {
                        debugger;
                        var updatedSales = new mastermodelVM(result);
                        var index = self.SalesList().findIndex(function (item) {
                            return item.id() === updatedSales.id();
                        });
                        if (index >= 0) {
                            self.SalesList.replace(self.SalesList()[index], updatedSales);
                        }
                        self.resetForm();
                        self.getData();
                        $('#salesModal').modal('hide');
                        alert("Updated Successfully"); // Display success message
                    } else {
                        alert( "Failes to update sales.");// Display error message
                    }      
                })
                .fail(function (err) {
                    console.error("Error updating sales:", err);
                    if (err.responseJSON && err.responseJSON.message) {
                        alert("Error updating sale: " + err.responseJSON.message);
                    } else {
                        alert("An error occurred while updating the sale. Please check all inputs and try again.");
                    }
                });
        } else {
            ajax.post(baseUrl + "/Add", JSON.stringify(salesData))
                .done(function (result) {
                    debugger;
                    if (result) {
                        debugger;
                        self.SalesList.push(new mastermodelVM(result));
                        self.resetForm();
                        self.getData();
                        $('#salesModal').modal('hide');
                        debugger;
                        alert( "Added successfully."); //Display success message
                    }
                    else
                    {
                        debugger;
                        alert("Failed to add sales.");//Dispaly error message
                    }
                })
                .fail(function (err) {
                    console.error("Error adding sales:", err);
                    if (err.responseJSON && err.responseJSON.message) {
                        alert("Error adding sale: " + err.responseJSON.message);
                    } else {
                        alert("An error occurred while adding the sale. Please check all inputs and try again.");
                    }
                });
        }
    };

    self.DeleteSales = function (model) {
        self.purchaseToDelete(model);
        setTimeout(function () {
            $('#deleteConfirmModal').modal('show');
        }, 100);
    };

    self.confirmDelete = function () {
        var model = self.purchaseToDelete();
        if (model) {
            ajax.delete(baseUrl + "/Delete?id=" + model.id())
          
                .done(function () {
                    self.SalesList.remove(function (item) {
                        return item.id() === model.id();
                    });
                    $('#deleteConfirmModal').modal('hide');
                })
                .fail(function (err) {
                    console.error("Error deleting sale:", err);
                    $('#deleteConfirmModal').modal('hide');
                });
        }
    };

    self.SelectSale = function (model) {
        var clonedModel = ko.toJS(model);
        if (clonedModel.salesDate) {
            clonedModel.salesDate = new Date(clonedModel.salesDate).toISOString().slice(0, 16);
        }
        self.NewSales(new mastermodelVM(clonedModel));
        self.IsUpdated(true);
        $('#salesModal').modal('show');
    };

    self.resetForm = function () {
        self.NewSales(new mastermodelVM());
        self.IsUpdated(false);
        self.NewSales().sales.push(new detailsmodelVM());
    };

    self.openCreateModal = function () {
        self.resetForm();
        $('#salesModal').modal('show');
    };

    self.AddItem = function () {
        self.NewSales().sales.push(new detailsmodelVM());
    };
    self.removeItem = function (item) {
        self.NewSales().sales.remove(item);
    };
};

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url,
            async: false,
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

