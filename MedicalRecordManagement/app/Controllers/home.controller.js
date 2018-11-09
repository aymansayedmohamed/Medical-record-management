'use strict';

angular.module("MedicalRecordManagement").controller('homeController', ['$rootScope', '$scope', 'MedicalHistoryApi', function ($rootScope, $scope, MedicalHistoryApi) {

    debugger;
    $scope.MedicalHistory = [];

    $scope.SocialNumber = "123456789";

    $scope.error = 'false';

    $scope.Delivered = 'false';


    $scope.GetMedicalRecords = function ()
    {
        $scope.Error='false';

        MedicalHistoryApi.Get($scope.SocialNumber).then((response) => {

            $scope.MedicalHistory = response.data;

        }).catch((error) => {

            $scope.Error = 'true';

            });
    }


    $scope.DeliverByMail = function () {

        MedicalHistoryApi.DeliverByMail($scope.SocialNumber).then((response) => {

            $scope.Delivered = 'true';

        }).catch((error) => {

            $scope.Delivered = 'false';
        });
    }

}]);



