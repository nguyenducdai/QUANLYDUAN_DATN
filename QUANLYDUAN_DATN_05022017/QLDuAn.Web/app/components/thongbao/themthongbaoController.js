(function (app) {
    app.controller('themthongbaoController', ['$scope','service','notification','$state', function ($scope,service,notification,$state) {

        $scope.chooseMF = chooseMF;
        $scope.upFile = upFile;
        $scope.remove = remove;
        $scope.themThongBao = themThongBao;
        $scope.MoreFile = [];
        $scope.ThongBao = {
            Created_at: new Date(),
            Updated_at:null
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
            var item = {
                name: rename($scope.file_name.trim(),$scope.path),
                path: $scope.path
            }
            $scope.MoreFile.push(item);
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

        function themThongBao() {
            $scope.ThongBao.MoreFile = JSON.stringify($scope.MoreFile);
            service.post('api/tb/created', $scope.ThongBao, function (result) {
                notification.success('Thêm thông báo thành công');
                $state.go('thongbao');
            }, function (error) {

            });
        }

    }]);
})(angular.module('componentmodule'));