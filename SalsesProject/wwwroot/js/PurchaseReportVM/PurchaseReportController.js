/// <reference path="../knockout.js" />
/// <reference path="purchasereportmodel.js" />

var purchasecontroller = function () {
    var self = this;
    var baseUrl = "api/PurchaseMasterDetailAPI/Report";
    self.purchaseReportList = ko.observableArray([]);
    self.searchTerm = ko.observable('');

    self.getData = function () {
        return ajax.get(baseUrl).then(function (result) {
            self.purchaseReportList(result.map(item => new purchaseReportVM(item)));
        });
    };

    self.filteredPurchaseReportList = ko.computed(function () {
        var filter = self.searchTerm().toLowerCase();
        if (!filter) {
            return self.purchaseReportList();
        } else {
            return ko.utils.arrayFilter(self.purchaseReportList(), function (item) {
                return item.venderName().toLowerCase().indexOf(filter) > -1 ||
                    item.itemName().toLowerCase().indexOf(filter) > -1;
            });
        }
    });

    self.downloadExcel = function () {
        var data = self.filteredPurchaseReportList();
        var csvContent = "data:text/csv;charset=utf-8,";

        // Add headers
        csvContent += "S.N,Date,InvoiceNumber,VendorName,ItemName,Quantity,Price,UnitAmount,BillAmount,Discount,NetAmount\n";

        // Add data rows
        data.forEach(function (item, index) {
            csvContent += [
                index + 1,
                item.purchaseDate(),
                item.invoiceNumber(),
                item.venderName(),
                item.itemName(),
                item.quantity(),
                item.quantityPrice(),
                item.billAmount(),
                item.discount(),
                item.netAmount()
            ].join(",") + "\n";
        });

        var encodedUri = encodeURI(csvContent);
        var link = document.createElement("a");
        link.setAttribute("href", encodedUri);
        link.setAttribute("download", "purchase_report.csv");
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    };

    self.downloadPDF = function () {
        const { jsPDF } = window.jspdf;
        var doc = new jsPDF();

        var data = self.filteredPurchaseReportList();
        var columns = ["S.N", "Date", "InvoiceNumber", "VendorName", "ItemName", "Quantity", "Price", "UnitAmount", "BillAmount", "Discount", "NetAmount"];
        var rows = [];

        data.forEach(function (item, index) {
            rows.push([
                index + 1,
                item.purchaseDate(),
                item.invoiceNumber(),
                item.venderName(),
                item.itemName(),
                item.quantity(),
                item.quantityPrice(),  
                item.billAmount(),
                item.discount(),
                item.netAmount()
            ]);
        });

        doc.autoTable({
            head: [columns],
            body: rows,
        });
        doc.save('purchase_report.pdf');
    };

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
