(function (app) {
    app.controller('dsnhomnguoidungController', ['$scope','service','notification', function ($scope,service,notification) {
        
        $scope.Groups = {}

        function loadDsNhomNguoiDung() {
            service.get('api/applicationgroup/getall', null, function (result) {
                $scope.Groups = result.data;
            }, function (error) {
                notification.error(error);
            })
        }

        loadDsNhomNguoiDung();

    }]);
})(angular.module('componentmodule'));