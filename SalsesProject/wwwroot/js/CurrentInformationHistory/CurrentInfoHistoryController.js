/// <reference path="../knockout.js" />
/// <reference path="currentinfohistorymodel.js" />

var currentinfohistory = function (item) {
    var self = this;
    self.baseUrl = "/api/ItemHistoryAPI";
    self.CurrentHistoryList = ko.observableArray([]);
    self.filteredHistory = ko.observableArray([]);

    // Pagination
    self.currentPage = ko.observable(1);
    self.pageSize = ko.observable(10);

    // Fetch data from API
    self.getData = function () {
        ajax.get(self.baseUrl).then(function (result) {
            //console.log("API result:", result);
            self.CurrentHistoryList(result.map(item => new currenthistory(item)));
            self.filteredHistory(self.CurrentHistoryList());  // Initialize the filtered list
            ko.applyBindings(self);  // Apply bindings here, after data is loaded
        });
    };

    self.totalPages = ko.computed(function () {
        return Math.ceil(self.filteredHistory().length / self.pageSize());
    });

    self.pagedCustomerList = ko.computed(function () {
        if (!self.filteredHistory().length) {
            return [];
        }
        var startIndex = (self.currentPage() - 1) * self.pageSize();
        return self.filteredHistory().slice(startIndex, startIndex + self.pageSize());
    });

    self.nextPage = function () {
        if (self.currentPage() < self.totalPages()) {
            self.currentPage(self.currentPage() + 1);
        }
    };

    self.previousPage = function () {
        if (self.currentPage() > 1) {
            self.currentPage(self.currentPage() - 1);
        }
    };


    self.currentPageStartIndex = ko.computed(function () {
        return (self.currentPage() - 1) * self.pageSize();
    });

    // Initialize data fetch
    self.getData();
};

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url
        });
    }
};
