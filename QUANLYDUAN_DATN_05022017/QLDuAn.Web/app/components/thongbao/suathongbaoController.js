(function (app) {
    app.controller('suathongbaoController', ['$scope', 'service', 'notification', '$state', '$stateParams', function ($scope, service, notification, $state, $stateParams) {


        $scope.chooseMF = chooseMF;
        $scope.upFile = upFile;
        $scope.remove = remove;
        $scope.getDetail = getDetail;
        $scope.capNhatThongBao = capNhatThongBao;

        $scope.ThongBao = {
            Created_at: new Date(),
            Updated_at: null
        }
        $scope.MoreFile = [];

        function getDetail(id) {
            var config = {
                params: {
                    id:id
                }
            }
            service.get('api/tb/getbyid', config, function (result) {
                $scope.ThongBao = result.data;
                $scope.MoreFile = JSON.parse($scope.ThongBao.MoreFile);
            }, function (error) {

            });
        }

        function chooseMF() {
            var ckFinder = new CKFinder();
            ckFinder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.path = fileUrl;
                });
            }
            ckFinder.popup();
        }

        function upFile() {

            var status = false;

            var item = {
                name: rename($scope.file_name.trim(), $scope.path),
                path: $scope.path
            }


            for (var i = 0; i < $scope.MoreFile.length; i++) {
                if ($scope.MoreFile[i].path == $scope.path) {
                    status = true;
                    break;
                }
            }
            if (status) {
                notification.error('tệp tải lên đã tồn tại');
            } else {
                $scope.MoreFile.push(item);
            }
        }

        function rename(fileName, path) {
            return fileName.replace('.', '') + path.substr(path.lastIndexOf('.'));
        }

        function remove(path) {
            for (var i = 0; i < $scope.MoreFile.length; i++) {
                if ($scope.MoreFile[i].path == path) {
                    $scope.MoreFile.splice(i, 1);
                }
            }
        }

        function capNhatThongBao() {
            $scope.ThongBao.MoreFile = JSON.stringify($scope.MoreFile);
            service.put('api/tb/updated', $scope.ThongBao, function (result) {
                notification.success('Cập nhật thông báo thành công');
                $state.go('thongbao');
            }, function (error) {
                notification.error(error);
            });
        }
        getDetail($stateParams.id);

    }]);
})(angular.module('componentmodule'));