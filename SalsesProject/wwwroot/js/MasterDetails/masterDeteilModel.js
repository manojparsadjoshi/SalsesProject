/// <reference path="../knockout.js" />


var mastermodelVM = function (item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.salesDate = ko.observable(item.salesDate || '');
    self.customerId = ko.observable(item.customerId || '');
    self.invoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.customerName = ko.observable(item.customerName || '');
    //self.billAmount = ko.observable(item.billAmount || 0);
    self.discount = ko.observable(item.discount || 0);
    //self.netAmount = ko.observable(item.netAmount || 0);
    self.sales = ko.observableArray((item.sales || []).map(function (item) {
        return new detailsmodelVM(item);
    }));

    // Computed property for billAmount
    self.billAmount = ko.computed(function () {
        return this.sales().reduce((sum, item) => sum + item.amount(), 0);
    }, self);

    self.netAmount = ko.computed(function () {
        return this.billAmount() - parseFloat(this.discount() || 0);
    }, self);

}

var detailsmodelVM = function (item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.itemId = ko.observable(item.itemId || 0);
    self.unit = ko.observable(item.unit || '');
    self.quantity = ko.observable(item.quantity || 0);
    self.price = ko.observable(item.price || 0);
    self.amount = ko.observable(item.amount || 0);

    // Computed observable for the amount
    self.amount = ko.computed(function () {
        return self.quantity() * self.price();
    });

    // Add this subscription to update the unit when itemId changes
    self.itemId.subscribe(function (newItemId) {
        var selectedItem = masterdetailsController.ItemsNameList().find(function (item) {
            return item.itemId() == newItemId;
        });
        if (selectedItem) {
            self.unit(selectedItem.unit());
        }
    });
}

var customernamemodel = function (item) {
    var self = this;
    item = item || {};
    self.customerId = ko.observable(item.customerId || 0);
    self.customerName = ko.observable(item.customerName || '');
}

var itemnamemodel = function (item) {
    var self = this;
    item = item || {};
    self.itemId = ko.observable(item.itemId || 0);
    self.itemName = ko.observable(item.itemName || '');
    self.unit = ko.observable(item.unit || 0);
} 