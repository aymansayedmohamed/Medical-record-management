
var controllerProvider = null;
var provide = null;

(function () {
    'use strict';

    app = angular.module('MedicalRecordManagement.pages', [
      'ui.router',
      'ngMessages'
    ], function ($controllerProvider, $provide) {
        controllerProvider = $controllerProvider;
        provide = $provide;
    });
})();
