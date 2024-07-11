﻿/// <reference path="masterdeteilmodel.js" />
/// <reference path="../knockout.js" />


const mode = {
    create: 1,
    update: 2
};


var masterDeteilsController = function () {
    var self = this;
    const baseUrl = "api/SalesDetailsAPIServices";

   
    self.SalesList = ko.observableArray([]);
    self.CustomerNameList = ko.observableArray([]);
    self.ItemNameList = ko.observableArray([]);
    self.newSales = ko.observable(new masterModelVM());
    self.selectedSales = ko.observable(new masterModelVM());
    self.IsUpdate = ko.observable(false);


    self.getData = function () {
        ajax.get(baseUrl+"/GetAll").then(function (result) {
            self.SalesList(result.map(item => new masterModelVM(item)));
        });
    }
    self.getData();

    //Get CustomerName

    self.getCustomersName = function () {
        var url = baseUrl + "/GetCustomersName";
        console.log("Fetching products from URL: " + url);

        return ajax.get(url).then(function (data) {
            // console.log("Products received: ", data);
            var mappedProducts = ko.utils.arrayMap(data, (item) => {
                return new customerNameModel(item);
            });
            self.CustomerNameList(mappedProducts);
            console.log("Customers Data: ", self.CustomerNameList());
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching customersname: ", textStatus, errorThrown);
        });
    };

    self.getCustomersName();

    //Get ItemNames
    self.getItemsName = function () {
        var url = baseUrl +"/GetItemsName";
        console.log("Fetching products from URL: " + url);

        return ajax.get(url).then(function (data) {
            // console.log("Products received: ", data);
            var mappedProducts = ko.utils.arrayMap(data, (item) => {
                return new itemNameModel(item);
            });
            self.ItemNameList(mappedProducts);
            console.log("Items Data: ", self.ItemNameList());
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching itemsname: ", textStatus, errorThrown);
        });
    };

    self.getItemsName();

    self.AddSales = function () {
        var salesData = ko.toJS(self.newSales());
        if (self.IsUpdate()) {
            ajax.put(baseUrl + "/Updates", JSON.stringify(salesData))
                .done(function (result) {
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
                })
                .fail(function (err) {
                    console.error("Error updating sales:", err);
                });
        }
        else {

            ajax.post(baseUrl +"/Add", ko.toJSON(self.newSales()))
                .done(function (result) {
                    self.SalesList.push(new mastermodelVM(result));
                    self.resetForm();
                    self.getData();
                    $('#salesModal').modal('hide');
                })
                .fail(function (err) {
                    console.error("Error adding sales:", err);
                });;
        }
    }

    self.DeleteSales = function (model) {
        ////debugger;
        console.log("Deleting sale with ID:", model.id());
        ajax.delete(baseUrl + "/Delete?id=" + model.id())
            .done(function (result) {
                self.SalesList.remove(function (item) {
                    return item.id() === model.id();
                });
            }).fail(function (err) {
                console.error("Error deleting sale:", err);
            });
    };


    self.SelectSale = function (model) {
        // Deep clone the model to avoid reference issues
        var clonedModel = ko.toJS(model);

        // Format the date properly for datetime-local input
        if (clonedModel.salesDate) {
            clonedModel.salesDate = new Date(clonedModel.salesDate).toISOString().slice(0, 16);
        }

        // Update NewSales with the cloned and formatted model
        self.newSales(new masterModelVM(clonedModel));
        self.IsUpdate(false);
        $('#salesModal').modal('show');
    }


    self.resetForm = () => {
        self.newSales(new masterModelVM());
        self.IsUpdate(false);
    };


    self.AddItem = function () {
        self.newSales().sales.push(new detailsModelVM());
    };

    self.removeItem = function (item) {
        self.newSales().sales.Remove(item);
    };
};

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url : url,
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
}