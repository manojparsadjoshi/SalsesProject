/// <reference path="../knockout.js" />

var VendorModel = function (item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.name = ko.observable(item.name || '');
    self.contract = ko.observable(item.contract || '');
    self.address = ko.observable(item.address || '');
}
