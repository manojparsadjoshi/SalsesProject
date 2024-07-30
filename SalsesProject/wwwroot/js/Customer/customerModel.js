/// <reference path="../jquery.js" />
/// <reference path="../knockout.js" />
var customerModel = function (item)
{
    var self = this;
    item = item || {};
    self.customerId = ko.observable(item.customerId || 0);

    self.customerName = ko.observable(item.customerName || '').extend({
        required: { message: "Full Name Is Required." }
    });
    self.contactNumber = ko.observable(item.contactNumber || '').extend({
        required: { message: "Contract Number Is Required." }
    });
    self.address = ko.observable(item.address || '').extend({
        required: { message: "Address Is Required." }
    });

    self.errors = ko.validation.group(self);

    self.isValid = ko.computed(function () {
        return self.errors().length === 0;
    });

}
