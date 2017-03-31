(function (app) {
    app.controller('dsnhomnguoidungController', ['$scope','service','notification','$ngBootbox', function ($scope,service,notification,$ngBootbox) {
        
        $scope.Groups = {}

        function loadDsNhomNguoiDung() {
            service.get('api/applicationgroup/getall', null, function (result) {
                $scope.Groups = result.data;
            }, function (error) {
                notification.error(error);
            })
        }

        $scope.Del = function (id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa không').then(function(){
                var config = {
                    params: {
                        id:id
                    }
                }
                service.del('api/applicationgroup/delete', config, function (result) {
                    loadDsNhomNguoiDung();
                    notification.success('Xóa nhóm người dùng thành công');
                }, function (error) {
                    notification.error(error);
                })

            });
            
        }
        loadDsNhomNguoiDung();

    }]);
})(angular.module('componentmodule'));