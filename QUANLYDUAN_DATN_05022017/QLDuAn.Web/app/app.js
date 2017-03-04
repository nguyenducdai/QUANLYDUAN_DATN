/// <reference path="E:\DATA_DRIVE_2017\QUANLYDUAN_DATN\QUANLYDUAN_DATN_05022017\QLDuAn.Web\Assets/Admin/Librarys/angular/angular.js" />
/// khai bao cac module cua he thong cau hinh cho ung dung
/// <reference path="components/home/index.html" />
(function () {
    angular.module('QLdaConfig', ['componentmodule', 'QLdaCommon']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("admin", {
            url: "/admin",
            templateUrl: "/app/components/home/index.html",
            controller: "HomeAdminController"
        });
        $urlRouterProvider.otherwise('/admin');
    }
})();