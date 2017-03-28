/// <reference path="E:\DATA_DRIVE_2017\QUANLYDUAN_DATN\QUANLYDUAN_DATN_05022017\QLDuAn.Web\Assets/Admin/Librarys/angular/angular.js" />
/// khai bao cac module cua he thong cau hinh cho ung dung
/// <reference path="components/home/index.html" />
/// <reference path="share/view/base.html" />
(function () {
    angular.module('QLdaConfig', ['componentmodule', 'QLdaCommon'])
        .config(config)
        .config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('baseTemplate', {
                url: '',
                templateUrl: '/app/share/view/base.html',
                abstract: true,
            })
            .state('login', {
                url: '/dang-nhap',
                templateUrl: '/app/components/dangnhap/login.html',
                controller: 'loginController'
            })
            .state("home", {
                url: "/admin",
                templateUrl: "/app/components/home/index.html",
                parent: 'baseTemplate',
                controller: "HomeAdminController"
        });
        $urlRouterProvider.otherwise('/dang-nhap');
    }
    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/dang-nhap');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/dang-nhap');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();