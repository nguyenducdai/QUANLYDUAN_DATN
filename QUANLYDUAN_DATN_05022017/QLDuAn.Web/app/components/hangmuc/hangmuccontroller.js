(function (app) {
    app.controller("hangmuccontroller", hangmuccontroller);

    hangmuccontroller.$inject = ['$scope', 'service', '$state', 'notification', '$ngBootbox'];

    function hangmuccontroller($scope, service, $state, notification, $ngBootbox) {

        $scope.HangMuc = {
            TrangThai: true,
            LoaiHangMuc:true,
            Created_at:new Date(),
            Updated_at: new Date()
        }

        var status = 0;

        $scope.ThemHangMuc = ThemHangMuc;
        $scope.deleteHangMuc = deleteHangMuc;
        $scope.editHangMuc = editHangMuc;
        $scope.clear = clear;
       
        function ThemHangMuc() {
            if (status == 0) {
                service.post('/api/hm/create', $scope.HangMuc, function (result) {
                    loadData();
                    notification.success('Thêm hạng mục thành công');
                }, function () {
                    notification.success('thất bại');
                });
            } else if (status == 1) {
                service.put('api/hm/update', $scope.HangMuc, function (result) {
                    loadData();
                    notification.success('cập nhật hạng mục thành công');

                }, function () {
                    console.log('có lỗi sảy ra');
                });
            }
        }

        function deleteHangMuc(id) {
            var config = {
                params:{
                    id:id
                }
            }
            $ngBootbox.confirm('bạn có chắc chắn muôn xóa hạng mục này không ?').then(function () {
                service.del('api/hm/delete', config, function (result) {
                    notification.success('xóa hạng mục thành công');
                    loadData();
                }, function () {
                    console.log('có lỗi sảy ra');
                });
            });
        }

        function editHangMuc(id) {
            var config = {
                params: {
                    id: id
                }
            }
            service.get('api/hm/getbyid', config, function (result) {
                status =1;
                $scope.HangMuc = result.data;

            }, function () {
                console.log('có lỗi sảy ra');
            });
        }

        function clear() {
            status = 0;
            $scope.HangMuc = {
                TrangThai: true,
                LoaiHangMuc: true,
                Created_at: new Date(),
                Updated_at: new Date()
            }
        }

        function loadData() {
            service.get('api/hm/getall', null, function (result) {
                $scope.ListHangMuc = result.data;
            }, function () {
                console.log('có lỗi sảy ra');
            });
        }
        loadData();

       
    }

})(angular.module('componentmodule'));