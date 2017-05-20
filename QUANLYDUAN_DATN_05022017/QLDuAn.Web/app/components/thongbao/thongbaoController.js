(function (app) {
    app.controller('thongbaoController', ['$scope', 'service', 'notification', '$state', '$ngBootbox', function ($scope, service, notification, $state, $ngBootbox) {

        $scope.loadThongBao = loadThongBao;
        $scope.remove = remove;
        $scope.viewDetail = viewDetail;
        $scope.ThongBao = {};
        $scope.ThongBaoChiTiet = {};
        $scope.templateUrl = '';
        $scope.alert = 'click vào thông báo để xem chi tiết';

        //pagination
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.MoreFile = {};
        $scope.Time = new Date().toLocaleTimeString();

        function loadThongBao(page) {

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 12,
                    keyword: $scope.keyword
                }
            }

            service.get('api/tb/getall', config, function (result) {
                $scope.ThongBao = result.data.items;
                for (var i = 0; i < result.data.items.length; i++) {
                    $scope.ThongBao[i].MoreFile = JSON.parse(result.data.items[i].MoreFile);
                }
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
            });
        }

        function remove(id) {
            $ngBootbox.confirm('bạn có chắn muốn xóa thông báo này không').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                service.del('api/tb/deleted', config, function (result) {
                    loadThongBao();
                    notification.success('xoa thông báo số ' + id + ' thành công');
                }, function (error) {
                });
            });
        }
        
        function viewDetail(id) {
            $scope.alert = '';
            $scope.templateUrl = '/app/components/thongbao/chitietthongbao.html';
            var config = {
                params: {
                        id:id
                }
            }
            service.get('api/tb/getbyid', config, function (result) {
                $scope.ThongBaoChiTiet = result.data;
                $scope.ThongBaoChiTiet.MoreFile = JSON.parse(result.data.MoreFile);

            }, function (error) {
            });
           
        }
        loadThongBao();
    }]);
})(angular.module('componentmodule'));