/// <reference path="duanhangmuc/duanhangmucController.js" />
/// <reference path="E:\DATA_DRIVE_2017\QUANLYDUAN_DATN\QUANLYDUAN_DATN_05022017\QLDuAn.Web\Assets/librarys/angular/angular.js" />
/// <reference path="hangmuc/index.html" />
/// <reference path="" />
(function () {
    angular.module('componentmodule', ['QLdaCommon']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("hangmuc", {
            url: '/hangmuc',
            templateUrl: '/app/components/hangmuc/index.html',
            controller: 'hangmuccontroller'
        })
        .state('hopdong', {
            url: '/hopdong',
            templateUrl: '/app/components/hopdong/index.html',
            controller: 'danhsachHdController'
        })
        .state('hd-create', {
            url: '/hd/themmoi',
            templateUrl: '/app/components/hopdong/add.html',
            controller: 'ThemHdController'
        })
         .state('duan', {
            url: '/duan',
            templateUrl: '/app/components/duan/index.html',
            controller: 'DuAnController'
         })
        .state('themhangmuc', {
            url: '/themhangmuc/:id',
            templateUrl: '/app/components/duanhangmuc/index.html',
            controller: 'duanhangmucController'
        });
    }
})();