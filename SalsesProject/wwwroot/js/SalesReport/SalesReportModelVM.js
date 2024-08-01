/// <reference path="../knockout.js" />

var salesReportModelVM = function (item) {
    var self = this;
    item = item || {};
    self.salesDate = ko.observable(item.salesDate || '');
    self.invoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.customerName = ko.observable(item.customerName || '');
    self.itemName = ko.observable(item.itemName || '');
    self.quentityPrice = ko.observable(item.quentityPrice || 0);
    self.quentity = ko.observable(item.quentity || 0);
    self.discountAmount = ko.observable(item.discountAmount || 0);
    self.quentityAmount = ko.observable(item.quentityAmount || 0);
    self.netAmount = ko.observable(item.netAmount || 0);
    self.billAmount = ko.observable(item.billAmount || 0);
};