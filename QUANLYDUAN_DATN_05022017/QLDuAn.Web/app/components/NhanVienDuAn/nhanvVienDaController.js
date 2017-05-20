(function (app) {
    app.controller('nhanvVienDaController', nhanvVienDaController);

    nhanvVienDaController.$inject = ['$scope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox'];
    function nhanvVienDaController($scope, service, notification) {

        $scope.LoadData = LoadData;

        //pagination
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

        function LoadData(page) {
            $scope.loadding = true;

            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 12,
                    keyword: $scope.keyword
                }
            }
         
            service.get('api/appuser/getalluserandproject', config, function (result) {
                $scope.loadding = false;
                $scope.ListNhanVien = result.data.items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
            });
        }
        LoadData();
    }
})(angular.module('QLdaConfig'));