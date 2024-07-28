/// <reference path="../knockout.js" />
/// <reference path="currentinfohistorymodel.js" />
var CurrentInfoHistory = function () {
    var self = this;
    self.baseUrl = "/api/ItemHistoryAPI";
    self.CurrentHistoryList = ko.observableArray([]);
    self.filteredHistory = ko.observableArray([]);
    self.currentPage = ko.observable(1);
    self.pageSize = ko.observable(10);
    self.searchTerm = ko.observable('');

    self.getData = function () {
        ajax.get(self.baseUrl)
            .then(function (result) {
                console.log("API result:", result);
                self.CurrentHistoryList(result.map(item => new currenthistory(item)));
                self.applyFilter(); // Apply initial filter
            })
            .catch(function (error) {
                console.error("Error fetching data:", error);
            });
    };

    self.applyFilter = function () {
        var filter = self.searchTerm().toLowerCase();
        if (!filter) {
            self.filteredHistory(self.CurrentHistoryList());
        } else {
            self.filteredHistory(ko.utils.arrayFilter(self.CurrentHistoryList(), function (item) {
                return item.itemName().toLowerCase().indexOf(filter) !== -1 ||
                    item.transDateFormatted().toLowerCase().indexOf(filter) !== -1 ||
                    item.stockInOutText().toLowerCase().indexOf(filter) !== -1 ||
                    item.transactionTypeText().toLowerCase().indexOf(filter) !== -1;
            }));
        }
        self.currentPage(1); // Reset to first page when filter changes
    };

    self.searchTerm.subscribe(self.applyFilter);

    self.pagedHistoryList = ko.computed(function () {
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

    self.totalPages = ko.computed(function () {
        return Math.ceil(self.filteredHistory().length / self.pageSize());
    });

    self.currentPageStartIndex = ko.computed(function () {
        return (self.currentPage() - 1) * self.pageSize();
    });

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