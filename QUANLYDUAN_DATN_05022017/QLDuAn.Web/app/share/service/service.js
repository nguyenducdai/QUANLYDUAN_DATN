/// <reference path="E:\DATA_DRIVE_2017\QUANLYDUAN_DATN\QUANLYDUAN_DATN_05022017\QLDuAn.Web\Assets/librarys/angular/angular.js" />
(function (app) {
    app.service('service', service);

    service.$inject = ['$http','authenticationService'];

    function service($http, authenticationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }


        function post(url, params, success, failure) {
            authenticationService.setHeader();
            $http.post(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }

        function put(url, params, success, failure) {
            authenticationService.setHeader();
            $http.put(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }

        function del(url, params, success, failure) {
            authenticationService.setHeader();
            $http.delete(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }

        function get(url, params, success, failure) {
            authenticationService.setHeader();
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }
    }

   

})(angular.module("QLdaCommon"));