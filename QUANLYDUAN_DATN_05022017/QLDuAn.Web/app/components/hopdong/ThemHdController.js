(function (app) {
    app.controller('ThemHdController', ThemHdController);

    ThemHdController.$inject = ['$scope','service', '$state','notification'];

    function ThemHdController($scope, service, $state, notification) {

        $scope.HopDong = {
            NgayBatDau: new Date("dd/mm/yyyy"),
            NgayKetThuc: new Date(),
            NgayKy: new Date()
        }

        $scope.ThemHopDong = ThemHopDong;

        function ThemHopDong($scope, service, $state, notification) {
               

        }

        function converDate(dd, mm, yy) {
            var date = new Date();
            var dd = date.getDay;
            var mm = date.getMonth;
        };

    }

})(angular.module('componentmodule'));