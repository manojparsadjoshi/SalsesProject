/// <reference path="../knockout.js" />

var VendorModel = function (item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
   // self.name = ko.observable(item.name || '');
    self.name = ko.observable(item.name || '').extend({
        require: { message: "Vender name is Required." }
    });
    self.contract = ko.observable(item.contract || '').extend({
        require: { message: "Vender Contract Number Is Required." }
    });
    self.address = ko.observable(item.address || '').extend({
        required: { message: "Vender Address Is Required." }
    });
    self.isValid = function () {
        return self.errors().length === 0;
    };
}
