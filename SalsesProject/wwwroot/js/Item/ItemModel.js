/// <reference path="../knockout.js" />
var itemModel = function (item)
{
    var self = this;
    item = item || {};
    self.itemId = ko.observable(item.itemId || 0);
    self.itemName = ko.observable(item.itemName || '');
    self.unit = ko.observable(item.unit || '');
    self.category = ko.observable(item.category || '');
}
