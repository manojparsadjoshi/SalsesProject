var itemModel = function (item) {
    var self = this;
    item = item || {};
    self.itemId = ko.observable(item.itemId || 0);
    self.itemName = ko.observable(item.itemName || '').extend({
        required: { message: "Item Name Is Required." }
    });
    self.unit = ko.observable(item.unit || '').extend({
        required: { message: "Item Unit Is Required." }
    });
    self.category = ko.observable(item.category || '').extend({
        required: { message: "Item Category Is Required." }
    });
    self.categoryName = ko.observable(item.categoryName || '');
    self.errors = ko.validation.group(self);
    self.isValid = function () {
        return self.errors().length === 0;
    };
};

var categorymodel = function (item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.name = ko.observable(item.name || '');
};