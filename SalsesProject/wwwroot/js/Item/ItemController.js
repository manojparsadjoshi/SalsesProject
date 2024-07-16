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
    self.CategoryNameList = ko.observableArray([]);
    self.NewItem().errors = ko.validation.group(self.NewItem());

    // Fetch categories first, then fetch items
    self.init = function () {
        self.getcategoryname().then(function () {
            self.getdata();
        });
    };

    self.getcategoryname = function () {
        var url = baseUrl + "/GetCategory";
        console.log("Fetching categories from URL: " + url);

        return ajax.get(url).then(function (data) {
            var mappedProducts = ko.utils.arrayMap(data, (item) => {
                return new categorymodel(item);
            });
            self.CategoryNameList(mappedProducts);
            console.log("Categories Data: ", self.CategoryNameList());
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching categories: ", textStatus, errorThrown);
        });
    };

    self.getdata = function () {
        return ajax.get(baseUrl).then(function (result) {
            self.ItemList(result.map(item => {
                var mappedItem = new itemModel(item);
                var category = ko.utils.arrayFirst(self.CategoryNameList(), function (cat) {
                    return cat.id() == mappedItem.category();
                });
                mappedItem.categoryName(category ? category.name() : '');
                return mappedItem;
            }));
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching items: ", textStatus, errorThrown);
        });
    };

    ko.validation.init();

    self.AddItem = function () {
        console.log("AddItem called");
        console.log("NewItem data:", ko.toJS(self.NewItem));
        if (self.NewItem().isValid()) {
            var selectedCategory = ko.utils.arrayFirst(self.CategoryNameList(), function (cat) {
                return cat.id() == self.NewItem().category();
            });
            self.NewItem().categoryName(selectedCategory ? selectedCategory.name() : '');

            var data = ko.toJSON(self.NewItem);
            var url = self.mode() === mode.create ? baseUrl : baseUrl + "/" + self.NewItem().itemId();
            var method = self.mode() === mode.create ? 'POST' : 'PUT';

            ajax.request(url, method, data)
                .done(function (result) {
                    if (self.mode() === mode.create) {
                        self.ItemList.push(new itemModel(result));
                    } else {
                        self.ItemList.replace(self.SelectedList(), new itemModel(result));
                    }
                    self.resetForm();
                    self.getdata();
                    self.hideModal();
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    console.error("Error saving item: ", textStatus, errorThrown);
                });
        } else {
            self.NewItem().errors.showAllMessages();
        }
    };

    self.Deleteitem = function (model) {
        ajax.delete(baseUrl + "?id=" + model.itemId())
            .done((result) => {
                self.ItemList.remove(model);
            })
            .fail((jqXHR, textStatus, errorThrown) => {
                console.error("Error deleting item: ", textStatus, errorThrown);
            });
    };

    self.selectItem = function (model) {
        self.SelectedList(model);
        var newItem = new itemModel(ko.toJS(model));
        var selectedCategory = ko.utils.arrayFirst(self.CategoryNameList(), function (cat) {
            return cat.id() == newItem.category();
        });
        newItem.categoryName(selectedCategory ? selectedCategory.name() : '');
        self.NewItem(newItem);
        self.IsUpdate(true);
        self.mode(mode.update);
        self.showModal();
    };

    self.setCreateMode = function () {
        self.resetForm();
        self.showModal();
    };

    self.showModal = function () {
        var modal = new bootstrap.Modal(document.getElementById('itemModal'));
        modal.show();
    };

    self.hideModal = function () {
        var modal = bootstrap.Modal.getInstance(document.getElementById('itemModal'));
        if (modal) {
            modal.hide();
        }
    };

    self.CloseModel = function () {
        self.resetForm();
        self.hideModal();
    };

    self.resetForm = () => {
        self.NewItem(new itemModel());
        self.IsUpdate(false);
        self.mode(mode.create);
    };

    // Initialize the controller
    self.init();
};

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url
        });
    },
    request: function (url, method, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: method,
            url: url,
            data: data
        });
    },
    delete: function (url) {
        return $.ajax({
            method: "DELETE",
            url: url,
        });
    }
};