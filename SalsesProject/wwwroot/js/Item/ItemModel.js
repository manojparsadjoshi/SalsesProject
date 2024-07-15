/// <reference path="../knockout.js" />
var itemModel = function (item)
{
    var self = this;
    item = item || {};
    self.itemId = ko.observable(item.itemId || 0);
    //Adding index Validation also.
    self.itemName = ko.observable(item.itemName || '').extend({
        required: { message: "Item Name Is Required." }
    });

    self.unit = ko.observable(item.unit || '').extend({
        required: { message: "Item Unit Is Required." }
    });
    self.category = ko.observable(item.category || '').extend({
        required: { message: "Item Category Is Required." }
    });

    self.errors = ko.validation.group(self);

    self.isValid = ko.computed(function () {
        return self.errors().length === 0;
    });
}
