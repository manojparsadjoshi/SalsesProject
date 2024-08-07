﻿/// <reference path="../knockout.js" />
/// <reference path="../jquery.js" />
/// <reference path="customermodel.js" />

const mode = {
    create: 1,
    update: 2
};

var customerController = function () {
    var self = this;
    const baseUrl = "api/CustomerAPI";
    self.customerList = ko.observableArray([]);
    self.newCustomer = ko.observable(new customerModel());
    self.IsUpdate = ko.observable(false);
    self.selectedCustomer = ko.observable(new customerModel());
    self.mode = ko.observable(mode.create);
    // filled data validation
    self.newCustomer().errors = ko.validation.group(self.newCustomer());

    self.customerToDelete = ko.observable();
    //pagination
    self.currentPage = ko.observable(1);
    self.pageSize = ko.observable(10);
    //search bar
    self.searchTerm = ko.observable('');

    //get data from API
    self.getdata = function () {
        ajax.get(baseUrl).then(function (result) {
            self.customerList(result.map(item => new customerModel(item)));
        });
    }
    self.getdata();

   
    ko.validation.init();
    self.AddCustomer = function () {
        if (self.newCustomer().isValid()) {
            switch (self.mode()) {
                case mode.create:
                    ajax.post(baseUrl + "/Add", ko.toJSON(self.newCustomer()))
                        .done(function (result) {
                            if (result.success) {
                                self.customerList.push(new customerModel(result));
                                self.getdata();
                                self.CloseModel();
                                toastr.success("New Customer Add Successfully.");
                            } else {
                                alert("Customer name already exist!");
                            }
                        })
                        .fail(function (err) {
                            console.log(err);
                        });
                    break;
                case mode.update:
                    ajax.put(baseUrl + "/id", ko.toJSON(self.newCustomer()))
                        .done(function (result) {
                            if (result.success) {
                                self.customerList.replace(self.newCustomer());
                                self.resetForm();
                                self.getdata();
                                self.CloseModel();
                                toastr.success("New Customer Update Successfully.");
                            } else {
                                alert("Name already exist!");
                            }
                        })
                        .fail(function (err) {
                            console.log(err);
                        });
                    break;
                default:
                    console.log("Invalid mode");
            }
        } else {
            self.newCustomer().errors.showAllMessages();
        }
    };
    //self.DeleteCustomer = function (model) {
    //   // debugger;
    //    ajax.delete(baseUrl+"/id?id=" + model.customerId())
    //        .done((result) => {
    //            self.customerList.remove(model);
    //        })
    //        .fail((err) => {
    //            console.log(err);
    //        });
    //};

    //search function
    self.filteredCustomerList = ko.computed(function () {
        var filter = self.searchTerm().toLowerCase();
        if (!filter) {
            return self.customerList();
        } else {
            return ko.utils.arrayFilter(self.customerList(), function (customer) {
                return customer.customerName().toLowerCase().indexOf(filter) !== -1 ||
                    customer.contactNumber().toLowerCase().indexOf(filter) !== -1 ||
                    customer.address().toLowerCase().indexOf(filter) !== -1;
            });
        }
    });


    //pegination function
    self.totalPages = ko.computed(function () {
        return Math.ceil(self.filteredCustomerList().length / self.pageSize());
    });
    self.pagedCustomerList = ko.computed(function () {
        var startIndex = (self.currentPage() - 1) * self.pageSize();
        return self.filteredCustomerList().slice(startIndex, startIndex + self.pageSize());
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


    //Delete functionality
    self.DeleteCustomer = function (model) {
        self.customerToDelete(model);
        setTimeout(function () {
            $('#deleteConfirmModal').modal('show');
        }, 100);
    };
    self.confirmDelete = function () {
        var model = self.customerToDelete();
        if (model) {
            ajax.delete(baseUrl + "/id?id=" + model.customerId())
                .done((result) => {
                    self.customerList.remove(model);
                    $('#deleteConfirmModal').modal('hide');
                    toastr.success(" Customer Delete Successfully.");
                })
                .fail((err) => {
                    console.log(err);
                    $('#deleteConfirmModal').modal('hide');
                });
        }
    };

    self.setCreateMode = function () {
        self.resetForm();
        $('#customerModel').modal('hide');
    };

    self.resetForm = () => {
        self.newCustomer(new customerModel());
        self.IsUpdate(false);
        self.mode(mode.create);
    };
    self.SelectCustomer = function (model) {
       // debugger;
        self.selectedCustomer(model);
        self.newCustomer(new customerModel(ko.toJS(model)));
        self.IsUpdate(true);
        self.mode(mode.update);
        $('#customerModel').modal('show');
    }

    self.CloseModel = function () {
        self.resetForm();
        $('#customerModel').modal('hide');
    }
}            




var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url,
            async: false,
        });
    },
    post: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "POST",
            url: url,
            data: (data)
        });
    },
    put: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "PUT",
            url: url,
            data: data
        });
    },
    delete: function (route) {
        return $.ajax({
            method: "DELETE",
            url: route,
        });
    }
};