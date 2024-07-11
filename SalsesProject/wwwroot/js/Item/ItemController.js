/// <reference path="itemmodel.js" />
const mode = {
    create: 1,
    update: 2
};

var itemController = function () {
    var self = this;
    const baseUrl = "api/ItemAPI";
    self.ItemList = ko.observableArray([]);
    self.NewItem = ko.observable(new itemModel());
    self.SelectedList = ko.observable(new itemModel());
    self.IsUpdate = ko.observable(false);
    self.mode = ko.observable(mode.create);

    self.getdata = function () {
        ajax.get(baseUrl).then(function (result) {
            self.ItemList(result.map(item => new itemModel(item)));
        });
    }
    self.getdata();

    self.AddItem = function () {
        switch (self.mode()) {
            case mode.create:
                ajax.post(baseUrl, ko.toJSON(self.NewItem()))
                    .done(function (result) {
                        self.ItemList.push(new itemModel(result));
                        self.resetForm();
                        self.getdata();
                        $('#itemModal').modal('hide');
                    })
                    .fail(function (err) {
                        console.log(err);
                    });
                break;
            case mode.update:
               // debugger;
                ajax.put(baseUrl+"/" + self.NewItem().itemId(), ko.toJSON(self.NewItem()))
                    .done(function (result) {
                        self.ItemList.replace(self.SelectedList(), new itemModel(result));
                        self.resetForm();
                        self.getdata();
                        $('#itemModal').modal('hide');
                    })
                    .fail(function (err) {
                        console.log(err);
                    });
                break;
            default:
                console.log("Invalid mode");
        }
    };

    self.Deleteitem = function (model) {
        //debugger;
        ajax.delete(baseUrl +"?id="+ model.itemId())
            .done((result) => {
                self.ItemList.remove(model);
            })
            .fail((err) => {
                console.log(err);
            });
    };

    self.selectItem = function (model) {
        self.SelectedList(model);
        self.NewItem(new itemModel(ko.toJS(model)));
        self.IsUpdate(true);
        self.mode(mode.update);
        $('#itemModal').modal('show');
    };

    self.setCreateMode = function () {
        self.resetForm();
        $('#itemModal').modal('hide');
    };

    self.CloseModel = function () {
        self.resetForm();
        $('#itemModal').modal('hide');
    }
    

    self.resetForm = () => {
        self.NewItem(new itemModel());
        self.IsUpdate(false);
        self.mode(mode.create);
    };
}

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url,
            async: false
        })
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
            method: "DELETE",
            url: route,
        });
    }
}