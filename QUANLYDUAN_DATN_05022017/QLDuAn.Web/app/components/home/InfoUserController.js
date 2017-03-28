(function (app) {
    app.controller('InfoUserController', ['$rootScope', 'authData', 'loginService', '$state', function ($rootScope, authData, loginService, $state) {
        $rootScope.Logout = function () {
            loginService.logout();
            $state.go('login');
        };

        $rootScope.InfoUserData = authData.authenticationData;
    }]);
})(angular.module('QLdaConfig'));