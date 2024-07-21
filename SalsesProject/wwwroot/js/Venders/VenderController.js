/// <reference path="../knockout.js" />
/// <reference path="vendermodel.js" />

const mode = {
    create: 1,
    update: 2
};

var VendorController = function () {
    var self = this;
    const baseUrl = '/api/VenderAPI';
    self.IsUpdated = ko.observable(false);
    self.NewVendor = ko.observable(new VendorModel());
    self.SelectedVendor = ko.observableArray([]);
    self.CurrentVendor = ko.observableArray([]);
    self.mode = ko.observable(mode.create);
    self.vendorToDelete = ko.observable();

    self.GetDatas = function () {
        ajax.get(baseUrl + "/GetAll").then(function (result) {
            self.CurrentVendor(result.map(item => new VendorModel(item)));
        });
    }

    self.GetDatas();

    self.AddVendor = function () {
        var vendorData = ko.toJS(self.IsUpdated() ? self.SelectedVendor : self.NewVendor);
        switch (self.mode()) {
            case 1:
                ajax.post(baseUrl, JSON.stringify(vendorData))
                    .done(function (result) {
                        self.CurrentVendor.push(new VendorModel(result));
                        self.GetDatas();
                        self.CloseModel();
                        $('#vendorModal').modal('hide');
                    });
                break;
            case 2:
                ajax.put(baseUrl, JSON.stringify(vendorData))
                    .done(function (result) {
                        self.CurrentVendor.replace(self.SelectedVendor(), new VendorModel(result));
                        self.CloseModel();
                        self.GetDatas();
                        $('#vendorModal').modal('hide');
                    });
                break;
        }
    }

    self.DeleteVendor = function (model) {
        self.vendorToDelete(model);  // No need to create a new VendorModel here
        setTimeout(function () {
            $('#deleteConfirmModal').modal('show');
        }, 100);
    };

    self.confirmDelete = function () {
        var model = self.vendorToDelete();
        if (model && model.id && model.id()) {
            var id = model.id();
            console.log("Attempting to delete vendor with ID:", id);
            ajax.delete(baseUrl + "?id=" + id)
                .done((result) => {
                    console.log("Delete successful", result);
                    self.CurrentVendor.remove(model);
                    $('#deleteConfirmModal').modal('hide');
                })
                .fail((jqXHR, textStatus, errorThrown) => {
                    console.error("Delete failed", jqXHR.status, textStatus, errorThrown);
                    console.error("Response:", jqXHR.responseText);
                    alert("Failed to delete vendor. Please try again.");
                    $('#deleteConfirmModal').modal('hide');
                });
        } else {
            console.error("No valid vendor ID to delete");
            alert("No valid vendor selected for deletion.");
            $('#deleteConfirmModal').modal('hide');
        }
    };

    self.SelectVendor = function (model) {
        self.SelectedVendor(model);
        self.IsUpdated(true);
        self.mode(mode.update);
        $('#vendorModel').modal('show');
    }

    self.CloseModel = function () {
        self.ResetForm();
        self.GetDatas();
    }

    self.ResetForm = function () {
        self.NewVendor(new VendorModel());
        self.SelectedVendor(new VendorModel());
        self.IsUpdated(false);
    }
};

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
            data: data
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
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "DELETE",
            url: route,
        });
    }
};