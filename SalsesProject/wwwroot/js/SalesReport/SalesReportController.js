/// <reference path="../knockout.js" />
/// <reference path="salesreportmodelvm.js" />
var salesReportController = function () {
    var self = this;
    var baseUrl = "/api/SalesDetailsAPIServices/GetSalesReports";
    self.salesReportList = ko.observableArray([]);
    self.searchTerm = ko.observable('');

    self.getData = function () {
        return ajax.get(baseUrl).then(function (result) {
            self.salesReportList(result.map(item => new salesReportModelVM(item)));
        });
    }

    self.filteredSalesReportList = ko.computed(function () {
        var filter = self.searchTerm().toLowerCase();
        if (!filter) {
            return self.salesReportList();
        } else {
            return ko.utils.arrayFilter(self.salesReportList(), function (item) {
                return item.customerName().toLowerCase().indexOf(filter) > -1 ||
                    item.itemName().toLowerCase().indexOf(filter) > -1;
            });
        }
    });

    // Call getData when the controller is instantiated
    self.getData();
}

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url
        });
    }
};