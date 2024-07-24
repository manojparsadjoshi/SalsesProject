/// <reference path="../knockout.js" />

var currenthistory = function (item) {
    var self = this;  // Use 'var' to avoid creating a global variable
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.itemId = ko.observable(item.itemId || 0);
    self.quentity = ko.observable(item.quentity || 0);  // Keep this as 'quentity' to match your API
    self.transDate = ko.observable(item.transDate || "");
    self.stockInOut = ko.observable(item.stockInOut || 0);
    self.transactionType = ko.observable(item.transactionType || 0);
}
