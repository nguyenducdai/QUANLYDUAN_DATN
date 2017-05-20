(function (app) {
    app.controller('loginController', ['$scope', 'notification', '$injector', 'loginService',
        function ($scope, notification, $injector, loginService) {
            $scope.loginData = {
                userName: '',
                password: ''
            }
            $scope.doLogin = doLogin;
            $scope.loadding = false;
            function doLogin() {
                $scope.loadding = true;
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.data.error != undefined) {
                        notification.error(response.data.error);
                    } else {
                        $scope.loadding = false;
                        var location = $injector.get('$state');
                        location.go('duan.hangmuc');
                    }
                });

            }

        }]);

})(angular.module('QLdaConfig'));