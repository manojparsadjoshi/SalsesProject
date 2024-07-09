/// <reference path="../knockout.js" />
var customerModel = function (item)
{
    var self = this;
    item = item || {};
    self.customerId = ko.observable(item.customerId || 0);
    self.customerName = ko.observable(item.customerName || '');
    self.contactNumber = ko.observable(item.contactNumber || '');
    self.address = ko.observable(item.address || '');
}