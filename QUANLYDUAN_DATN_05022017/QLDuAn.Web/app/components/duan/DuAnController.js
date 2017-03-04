(function (app) {
    app.controller('DuAnController', DuAnController);

    DuAnController.$inject = ['$scope', 'service', 'notification', '$state', '$mdDialog'];
    function DuAnController($scope, service,notification, $state, $mdDialog) {

        $scope.showAdd = showAdd;
        $scope.addDuan = addDuan;
        $scope.cancel = cancel;
        $scope.ObjDuAn = {}

        function showAdd(ev) {
            $mdDialog.show({
                controller:'DuAnController',
                templateUrl: '/app/components/duan/add.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
                })
               .then(function (answer) {
                   $scope.status = 'You said the information was "' + answer + '".';
               }, function () {
                   $scope.status = 'You cancelled the dialog.';
               });
        }

        function cancel() {
            $mdDialog.cancel();
        }

        function addDuan() {
            console.log($scope.ObjDuAn);
        }
    }

})(angular.module('QLdaConfig'));