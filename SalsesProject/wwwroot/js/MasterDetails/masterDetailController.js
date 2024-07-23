/// <reference path="../knockout.js" />


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
    //self.NewSales().sales.push(new detailsmodelVM());
    //Get All Data
    self.getData = function () {
        ajax.get(baseUrl + "/GetAll").then(function (result) {
            self.SalesList(result.map(item => new mastermodelVM(item)));
        });
    }
    self.getData();
    masterdetailsController.ItemsNameList = self.ItemsNameList;

    // Get CustomerNames

    self.getCustomersName = function () {
        var url = baseUrl + "/GetCustomersName";
        console.log("Fetching products from URL: " + url);

        return ajax.get(url).then(function (data) {
            // console.log("Products received: ", data);
            var mappedProducts = ko.utils.arrayMap(data, (item) => {
                return new customernamemodel(item);
            });
            self.CustomersNameList(mappedProducts);
            console.log("Customers Data: ", self.CustomersNameList());
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching customersname: ", textStatus, errorThrown);
        });
    };

    self.getCustomersName();

    //Get ItemNames
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
            console.log("Items loaded:", self.ItemsNameList()); // Add this line for debugging
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching itemsname: ", textStatus, errorThrown);
        });
    };

    self.getItemsName();

    self.AddSales = function () {
        var salesData = ko.toJS(self.NewSales());
        if (self.IsUpdated()) {
            ajax.put(baseUrl + "/Update", JSON.stringify(salesData))
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

            ajax.post(baseUrl + "/Add", ko.toJSON(self.NewSales()))
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
        var clonedModel = ko.toJS(model);
        if (clonedModel.salesDate) {
            clonedModel.salesDate = new Date(clonedModel.salesDate).toISOString().slice(0, 16);
        }
        self.NewSales(new mastermodelVM(clonedModel));
        self.IsUpdated(true);
        $('#salesModal').modal('show');
    };


    self.resetForm = () => {
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
            data: (data)
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