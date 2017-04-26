(function (app) {
    app.controller('loginController', ['$scope', 'notification','$injector', 'loginService',
        function ($scope, notification, $injector, loginService) {
            $scope.loginData = {
                userName: '',
                password:''
            }
            $scope.doLogin = doLogin;
            function doLogin() {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.data.error != undefined) {
                        notification.error('đăng nhập thất bại');
                    } else {
                        var location = $injector.get('$state');
                        location.go('duan.hangmuc');
                    }
                });
              
            }

        }]);

})(angular.module('QLdaConfig'));