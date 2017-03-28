
// lưu thông tin khi đã đăng nhập hoặc chưa đăng nhập trả về cho client
(function (app) {
    'use strict';
    app.service('authData', [
        function () {
            var authData = {}
            var authentication =
                {
                    IsAuthenticate: false,
                    username: ''
                }

            authData.authenticationData = authentication
            return authData;
        }]);

})(angular.module('QLdaCommon'));