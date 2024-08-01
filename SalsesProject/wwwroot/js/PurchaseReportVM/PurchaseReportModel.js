/// <reference path="../knockout.js" />

var purchaseReportVM = function (item) {
    var self = this;
    item = item || {};
    self.invoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.purchaseDate = ko.observable(item.purchaseDate ? new Date(item.purchaseDate).toLocaleDateString() : '');
    self.venderName = ko.observable(item.venderName || '');
    self.itemName = ko.observable(item.itemName || '');
    self.quantity = ko.observable(item.quentity || 0);
    self.quantityPrice = ko.observable(item.quentityPrice || 0);
    self.billAmount = ko.observable(item.billAmount || 0);
    self.discount = ko.observable(item.discount || 0);
    self.netAmount = ko.observable(item.netAmount || 0);
};
