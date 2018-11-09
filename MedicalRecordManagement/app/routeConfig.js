
function routeConfig($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {

    //Redirect any unmatched url
    $urlRouterProvider.otherwise("/");
    $stateProvider

    //home
    .state('Home', {
        url: "/",
        templateUrl: "/app/Views/home.html",
        controller: "homeController",
        resolve: {
            deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                return $ocLazyLoad.load({
                    files: [
                            //TODO add all the required css and js
                            '/app/Services/medicalHistory.service.js',
                            '/app/Controllers/home.controller.js'
                    ]
                });
            }]
        }
    })
        .state('ABOUTUS', {
        url: "/About Us",
        templateUrl: "/app/Views/aboutus.html",
        /*controller: "",
        resolve: {
            deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                return $ocLazyLoad.load({
                    files: [
                            //TODO add all the required css and js

                            
                    ]
                });
            }]
        }*/
        })


    $ocLazyLoadProvider.config({
        debug: false,
        events: true
    });

}

