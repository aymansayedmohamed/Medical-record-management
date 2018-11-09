app.directive('resourceKey', function () {
    return {
        restrict: 'A',
        link: function (scope, elem, attr) {
            elem.html(alexBankResources[attr.resourceKey]);
        }
    };
});

app.directive('resourcePlaceholder', function () {
    return {
        restrict: 'A',
        link: function (scope, elem, attr) {
            elem.attr('placeholder', alexBankResources[attr.resourcePlaceholder]);
        }
    };
});