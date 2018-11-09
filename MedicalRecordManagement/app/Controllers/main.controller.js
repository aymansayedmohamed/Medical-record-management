
app.controller('mainController', ['$scope', '$rootScope', '$window', '$location', mainController]);

function mainController($scope, $rootScope, $window, $location) {

    $scope.selectedDiscount = -1;
    $scope.selectedPoints = -1;
    $scope.selectHeader = function (index) {
        $scope.selectedDiscount = -1;
        $scope.selectedPoints = -1;
    };

    $scope.selectDiscount = function (index) {
        $scope.selectedDiscount = index;
    };

    $scope.selectPoints = function (index) {
        $scope.selectedPoints = index;
    };

    $scope.CommonParameters = {
        IsArabicMode: false,
        CurrentLanguage: 'en',
        textDirection: 'ltr',
        LanguageLink: 'ar'
    };

    $scope.SetCurrentLanguage = function () {

        if ($location.absUrl().toLowerCase().indexOf('/ar') == -1) {
            $scope.CommonParameters.CurrentLanguage = 'en';
            $scope.CommonParameters.textDirection = "ltr";
            $scope.CommonParameters.IsArabicMode = false;
            $scope.CommonParameters.LanguageLink = 'ar';

            $rootScope.Language = 'en-US';

        }
        else {
            $scope.CommonParameters.CurrentLanguage = 'ar';
            $scope.CommonParameters.IsArabicMode = true;
            $scope.CommonParameters.textDirection = "rtl";
            $scope.CommonParameters.LanguageLink = 'en';

            $rootScope.Language = 'ar-eg';
        }

    }
    $scope.GetTheCurrentUrlWithNewLanguage = function () {
        /*var currentUrl = $location.absUrl();
        var splitIndex = currentUrl.indexOf('/'+$scope.CommonParameters.CurrentLanguage+'/');
        var len=$scope.CommonParameters.CurrentLanguage.length;
        if (splitIndex == -1) {
            splitIndex = currentUrl.indexOf(location.origin);
            len = location.origin.length;
        }
        var secondPart = currentUrl.slice(splitIndex + len);
        var newUrl = $scope.CommonParameters.LanguageLink + secondPart;
        return newUrl;*/

        var currentUrl = $location.absUrl();
        var secondPart = '';
        var splitIndex = currentUrl.indexOf('/'+$scope.CommonParameters.CurrentLanguage+'/');
        if (splitIndex == -1) {
            splitIndex = location.origin.length;
            secondPart = currentUrl.slice(splitIndex);
            return $scope.CommonParameters.LanguageLink + secondPart;
        }
        secondPart = currentUrl.slice(splitIndex + $scope.CommonParameters.CurrentLanguage.length+1);
        console.log("second part=>"+ secondPart);
        var newUrl = $scope.CommonParameters.LanguageLink + secondPart;
        console.log("new url=>" + newUrl);
        return newUrl;

    }
    $scope.SetCurrentLanguage();
    if (this.alexBankResources == null) {
        if ($scope.CommonParameters.IsArabicMode) {
            $('head').append('<script type="text/javascript" src="/app/Resources/alexBank.AR.js"/>');
        }
        else {
            $('head').append('<script type="text/javascript" src="/app/Resources/alexBank.EN.js"/>');
        }

    }
}

