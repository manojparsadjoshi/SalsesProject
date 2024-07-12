/// <reference path="../knockout.js" />
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


    self.getdata = function () {
        ajax.get(baseUrl).then(function (result) {
            self.customerList(result.map(item => new customerModel(item)));
        });
    }
    self.getdata();

    self.AddCustomer = function () {
        switch (self.mode()) {
            case mode.create:
                ajax.post(baseUrl+"/Add", ko.toJSON(self.newCustomer()))
                    .done(function (result) {
                        self.customerList.push(new customerModel(result));
                        self.getdata();
                        self.CloseModel();
                    })
                    .fail(function (err) {
                        console.log(err);
                    });
                break;
            case mode.update:
                ajax.put(baseUrl + "/id", ko.toJSON(self.newCustomer()))
                    .done(function (result) {
                        self.customerList.replace(self.newCustomer());
                        self.resetForm();
                        self.getdata();
                        self.CloseModel();
                    })
                    .fail(function (err) {
                        console.log(err);
                    });
                break;
            default:
                console.log("Invalid mode");
        }
    };
    self.DeleteCustomer = function (model) {
       // debugger;
        ajax.delete(baseUrl+"/id?id=" + model.customerId())
            .done((result) => {
                self.customerList.remove(model);
            })
            .fail((err) => {
                console.log(err);
            });
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