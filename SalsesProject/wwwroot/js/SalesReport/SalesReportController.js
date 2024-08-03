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

    self.downloadExcel = function () {
        var data = self.filteredSalesReportList();
        var csvContent = "data:text/csv;charset=utf-8,";

        // Add headers
        csvContent += "S.N,Date,InvoiceNumber,CustomerName,ItemName,Quantity,Price,BillAmount,Discount,NetAmount\n";

        // Add data rows
        data.forEach(function (item, index) {
            csvContent += [
                index + 1,
                item.salesDate(),
                item.invoiceNumber(),
                item.customerName(),
                item.itemName(),
                item.quentity(),
                item.quentityPrice(),
                item.billAmount(),
                item.discountAmount(),
                item.netAmount()
            ].join(",") + "\n";
        });

        var encodedUri = encodeURI(csvContent);
        var link = document.createElement("a");
        link.setAttribute("href", encodedUri);
        link.setAttribute("download", "sales_report.csv");
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    };

    self.downloadPDF = function () {
        const { jsPDF } = window.jspdf;
        var doc = new jsPDF();

        var data = self.filteredSalesReportList();
        var columns = ["S.N", "Date", "InvoiceNumber", "CustomerName", "ItemName", "Quantity", "Price", "BillAmount", "Discount", "NetAmount"];
        var rows = [];

        data.forEach(function (item, index) {
            rows.push([
                index + 1,
                item.salesDate(),
                item.invoiceNumber(),
                item.customerName(),
                item.itemName(),
                item.quentity(),
                item.quentityPrice(),
                item.billAmount(),
                item.discountAmount(),
                item.netAmount()
            ]);
        });

        doc.autoTable({
            head: [columns],
            body: rows,
        });
        doc.save('sales_report.pdf');
    };

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
