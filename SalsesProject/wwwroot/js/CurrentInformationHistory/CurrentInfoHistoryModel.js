/// <reference path="../knockout.js" />

var currenthistory = function (item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.itemId = ko.observable(item.itemId || 0);
    self.itemName = ko.observable(item.itemName || "");
    self.quentity = ko.observable(item.quentity || 0);
    self.transDate = ko.observable(item.transDate || "");
    self.transactionType = ko.observable(item.transactionType || 0);
    self.stockInOut = ko.observable(item.stockInOut || 0);
    self.transDateFormatted = ko.observable(item.transDateFormatted || "");
    self.stockInOutText = ko.observable(item.stockInOutText || "");
    self.transactionTypeText = ko.observable(item.transactionTypeText || "");
}
