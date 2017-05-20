(function (app) {
    app.service('authenticationService', ['$http', '$q', '$window','authData',

    function ($http, $q, $window,authData) {

           // khởi tạo 1 tokenInfo để lưu thông tin token info

           //có chê : server cấu hình token để trả về 1 token mã hóa base 64;
           // check dang nhap thành cong thi set token -> $window.sessionStorage
           // moi khi hoat dong nó se set token vào header ->T thì ok , F->redirect

           var tokenInfo;

        this.setToken = function (data) {
            tokenInfo = data ;
            $window.sessionStorage['token_info'] = JSON.stringify(tokenInfo);
        }

        this.getToken = function () {
            return tokenInfo
        }

        this.removeToken = function () {
            tokenInfo = null;
            $window.sessionStorage['token_info'] = null;
        }

        this.setHeader = function () {
            delete $http.defaults.headers.common['X-Requested-With'];
            if ((authData.authenticationData != undefined) && (authData.authenticationData.accessToken != undefined) && (authData.authenticationData.accessToken != null) && (authData.authenticationData.accessToken != "")) {
                $http.defaults.headers.common['Authorization'] = 'Bearer ' + authData.authenticationData.accessToken;
                $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
            }

        }

        // sessionInti
        this.init = function () {
            var tokenInfo = $window.sessionStorage['token_info'];
            if (tokenInfo != null) {
                tokenInfo = JSON.parse(tokenInfo);
                if (tokenInfo != null) {
                    authData.authenticationData.IsAuthenticate = true;
                    authData.authenticationData.username = tokenInfo.userName;
                    authData.authenticationData.accessToken = tokenInfo.accessToken;
                }
               
            }
        }

        this.validateRequest = function () {
            var deferred = $q.defer();
            var url = 'api/home/TestMethod';
            $http.get(url).then(function () {
                deferred.resolve(null);
            }, function (error) {
                deferred.resolve(error);
            });
            return deferred.prosime;
        }

        this.init();
       }
    ]);

})(angular.module('QLdaCommon'));