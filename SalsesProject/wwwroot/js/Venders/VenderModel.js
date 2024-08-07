﻿/// <reference path="../knockout.js" />

var VendorModel = function (item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    // self.name = ko.observable(item.name || '');
    self.name = ko.observable(item.name || '').extend({
        required: { message: "Vender name is Required." }
    });
    self.contract = ko.observable(item.contract || '').extend({
        required: { message: "Vender Contract Number Is Required." },
        maxLength: { params: 10, message: "Contact No must not exceed 10 characters." },
        minLength: { params: 10, message: "Contact No must be 10 characters long." }
    });
    self.address = ko.observable(item.address || '').extend({
        required: { message: "Vender Address Is Required." }
    });
    self.errors = ko.validation.group(self);
    self.isValid = function () {
        return self.errors().length === 0;
    };
};
