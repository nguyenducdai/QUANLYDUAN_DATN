(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authData', 'authenticationService',
        function ($http, $q,authData, authenticationService) {
        var deferred;
        var userInfo;
        //hàm thực hiện login
        this.login = function (userName, password) {
            deferred = $q.defer();
            var data = "grant_type=password&username=" + userName + "&password=" + password;

            $http.post('/Token', data, {
                headers:
                    { 'Content-Type': 'application/x-www-form-urlencoded' }
            })
            .then(function (response) {
                userInfo = {
                    accessToken: response.data.access_token,
                    userName: userName
                }
                authenticationService.setToken(userInfo);
                authData.authenticationData.IsAuthenticate = true;
                authData.authenticationData.username = userName;
                deferred.resolve(null);
            },
            function (err, status) {
                authenticationService.removeToken();
                authData.authenticationData.IsAuthenticate = false;
                authData.authenticationData.username = '';
                deferred.resolve(err);
            });
            return deferred.promise;
        }


        this.logout = function () {
            authenticationService.removeToken();
            authData.authenticationData.IsAuthenticate = false;
            authData.authenticationData.username = '';
        }

    }]);
})(angular.module('QLdaCommon'));