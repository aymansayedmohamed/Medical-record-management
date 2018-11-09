var app = angular.module("MedicalRecordManagement", [
    "ui.router",
    "ngMessages",
    "oc.lazyLoad",
    'angular-loading-bar', 'ngAnimate', 'ngStorage'
]);

app.config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', routeConfig]);


