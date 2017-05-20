(function (app) {
    app.controller('binController', ['$scope', '$stateParams', 'service', 'notification', '$ngBootbox', '$filter', function ($scope, $stateParams, service, notification, $ngBootbox, $filter) {

        $scope.binHM = {};
        $scope.rehibilitate = rehibilitate;
        $scope.remove = remove;
        $scope.selectAllBin = selectAllBin;
        $scope.deleteAll = deleteAll;
        $scope.restoreAll = restoreAll;
        $scope.seleted = {};

        function rehibilitate(id) {
            var config = {
                params: {
                    id: id
                }
            }
            service.get('api/hm/restore', config, function (result) {
                load();
                notification.success('khôi phục hạng mục ' + id + ' thành công');
            }, function (error) { });
        }

        function remove(id) {
            var config = {
                params: {
                    id: id,
                    LoaiHm: $stateParams.LoaiHM,
                    idDuAn: $stateParams.idDuAn
                }
            }
            service.del('api/hm/delete', config, function (result) {
                load();
                notification.success('xóa hạng mục ' + id + ' thành công');
            }, function (error) { });
        }

        function load() {
            var config = {
                params: {
                    idDuAn: $stateParams.idDuAn,
                    LoaiHm: $stateParams.LoaiHM,
                }

            }
            service.get('api/hm/getIsdelete', config, function (result) {
                $scope.binHM = result.data;
            }, function (error) {
            });
        }

        function deleteAll() {
            var listID = [];
            $.each($scope.seleted, function (i, item) {
                listID.push(item.ID);
            });
            if (listID.length > 0) {
            $ngBootbox.confirm("bạn có chắc chắn muốn xóa các hạng mục được chọn không ? ").then(function () {
                
                var config = {
                    params: {
                        listId: JSON.stringify(listID),
                        LoaiHm: $stateParams.LoaiHM,
                        idDuAn: $stateParams.idDuAn
                    }
                }
                service.del('api/hm/deleteMuti', config, function (result) {
                    load();
                    notification.success('Đã xóa ' + listID.length + ' hạng mục thành công');
                }, function (error) { });

            });
            }
            else {
                notification.success('không có hạng mục nào cần xóa');
            }
        }

        function restoreAll() {
            $ngBootbox.confirm("khôi phục tất cả ?  ").then(function () {
                var listID = [];
                $.each($scope.seleted, function (i, item) {
                    listID.push(item.ID);
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listID)
                    }
                }
                service.get('api/hm/restoreall', config, function (result) {
                    load();
                    notification.success('Đã khôi phục' + listID.length + ' hạng mục thành công');
                }, function (error) { });


            });
        }

        $scope.isAllBin = false;
        function selectAllBin() {
            if ($scope.isAllBin == false) {
                angular.forEach($scope.binHM, function (item) {
                    item.checked = true;
                });
                $scope.isAllBin = true;
            } else {
                angular.forEach($scope.binHM, function (item) {
                    item.checked = false;
                });
                $scope.isAllBin = false;
            }

        }

        $scope.$watch("binHM", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.seleted = checked;
                $(".btn_delall").removeAttr("disabled");
            } else {
                $(".btn_delall").attr('disabled', 'disabled');
            }

        }, true);


        load();
    }]);
})(angular.module('componentmodule'));