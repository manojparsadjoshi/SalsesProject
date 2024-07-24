
/// <reference path="currentitemmodel.js" />
/// <reference path="../knockout.js" />

var currentitemcontroller = function () {
    var self = this;
    const baseUrl = "/api/CurrentItemInfoAPI";
    self.CurrentItemList = ko.observableArray([]);
    self.getData = function () {
        ajax.get(baseUrl).then(function (result) {
            self.CurrentItemList(result.map(item => new itemmodel(item)));
            ko.applyBindings(self);  // Apply bindings here
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