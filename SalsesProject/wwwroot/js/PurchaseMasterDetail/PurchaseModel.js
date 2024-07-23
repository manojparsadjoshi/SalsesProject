/// <reference path="../knockout.js" />

var masterpurchaseVM = function (item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.venderId = ko.observable(item.venderId || 0);
    self.vendorName = ko.observable(item.vendorName || '');
    self.invoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.billAmount = ko.observable(item.billAmount || 0);
    self.discount = ko.observable(item.discount || 0);
    self.netAmount = ko.observable(item.netAmount || 0);
    self.purchaseDetail = ko.observableArray((item.purchaseDetail || []).map(function (detail) {
        return new detailpurchaseVM(detail);
    }));

    self.updateBillAmount = function () {
        var total = self.purchaseDetail().reduce(function (sum, item) {
            return sum + item.amount();
        }, 0);
        self.billAmount(total);
        self.updateNetAmount();
    };

    self.updateNetAmount = function () {
        var net = self.billAmount() - self.discount();
        self.netAmount(net);
    };

    self.discount.subscribe(self.updateNetAmount);
};

var detailpurchaseVM = function (item) {
    var self = this;
    item = item || {};
    self.itemId = ko.observable(item.itemId || 0);
    self.itemName = ko.observable(item.itemName || '');
    self.unit = ko.observable(item.unit || '');
    self.quentity = ko.observable(item.quentity || 0);
    self.price = ko.observable(item.price || 0);
    self.amount = ko.computed(function () {
        return (parseFloat(self.quentity()) || 0) * (parseFloat(self.price()) || 0);
    });

    self.quentity.subscribe(function () {
        self.amount.notifySubscribers();
    });
    self.price.subscribe(function () {
        self.amount.notifySubscribers();
    });
};

function itemnamemodel(data) {
    var self = this;
    data = data || {};
    self.itemId = ko.observable(data.itemId);
    self.itemName = ko.observable(data.itemName);
    self.unit = ko.observable(data.unit);
}


var vendornamemodel = function (item) {
    var self = this;
    item = item || {};
    self.venderId = ko.observable(item.venderId || 0);
    self.venderName = ko.observable(item.venderName || '');
};