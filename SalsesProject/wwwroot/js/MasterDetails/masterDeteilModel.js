/// <reference path="../knockout.js" />

var mastermodelVM = function (item, parent) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.salesDate = ko.observable(item.salesDate || '');
    self.customerId = ko.observable(item.customerId || '');
    self.invoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.customerName = ko.observable(item.customerName || '');
    self.discount = ko.observable(item.discount || 0);
    self.sales = ko.observableArray((item.sales || []).map(function (item) {
        return new detailsmodelVM(item, parent);  // Pass parent to detailsmodelVM
    }));

    // Computed property for billAmount
    self.billAmount = ko.computed(function () {
        return this.sales().reduce((sum, item) => sum + item.amount(), 0);
    }, self);

    self.netAmount = ko.computed(function () {
        return this.billAmount() - parseFloat(this.discount() || 0);
    }, self);
}

var detailsmodelVM = function (item, parent) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.itemId = ko.observable(item.itemId || 0);
    self.unit = ko.observable(item.unit || '');
    self.quantity = ko.observable(item.quantity || 0);
    self.price = ko.observable(item.price || 0);
    self.AvailableQuantity = ko.observable(item.AvailableQuantity || 0);

    self.selectedItem = ko.computed(function () {
        return parent.ItemsNameList().find(function (listItem) {
            return listItem.itemId() == self.itemId();
        });
    });

    self.itemId.subscribe(function (newItemId) {
        var selected = self.selectedItem();
        if (selected) {
            self.unit(selected.unit());
            self.AvailableQuantity(selected.AvailableQuantity());
        }
    });

    self.amount = ko.computed(function () {
        return self.quantity() * self.price();
    });
};

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
    self.AvailableQuantity = ko.observable(item.quentity || 0);
}
