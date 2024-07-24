
/// <reference path="currentitemmodel.js" />
/// <reference path="../knockout.js" />

var currentitemcontroller = function () {
    var self = this;
    const baseUrl = "/api/CurrentItemInfoAPI";
    self.CurrentItemList = ko.observableArray([]);

    self.getData = function () {
        ajax.get(baseUrl).then(function (result) {
            console.log("API result:", result);
            self.CurrentItemList(result.map(item => {
                console.log("Mapping item:", item);
                return new itemmodel({
                    id: item.id,
                    itemId: item.itemId,
                    itemName: item.itemName || `Item ${item.itemId}`, // Fallback if itemName is not provided
                    quentity: item.quentity
                });
            }));
            console.log("Mapped CurrentItemList:", self.CurrentItemList());
        });
    }

    self.getData();
}

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url
        });
    }
}