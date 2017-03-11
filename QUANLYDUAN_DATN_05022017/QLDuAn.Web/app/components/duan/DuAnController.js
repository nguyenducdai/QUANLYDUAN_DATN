(function (app) {
    app.controller('DuAnController', DuAnController);

    DuAnController.$inject = ['$scope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox'];
    function DuAnController($scope, service, notification, $state, $mdDialog, $ngBootbox) {

        $scope.showAdd = showAdd;
        $scope.addDuan = addDuan;
        $scope.cancel = cancel;
        $scope.viewDetail = viewDetail;
        $scope.ObjDuAn = {
            TongDiem : null,
            DonGiaDiemDiem :null,
            LuongThueNgoai :null,
            TongChiQL :null,
            LuongTTQtt :null,
            LuongDPQdp: null,
            LuongGTQgt :null,
            LuongGTV21 :null,
            LuongGTV22: null,
            Created_at: new Date(),
            Updated_at:null
        };
        $scope.Detail = {};
        $scope.items = {};
        $scope.LoaiCongTrinh = {};
        $scope.TyLeTheoDT = {};
        $scope.TongChiQL = {};
        $scope.LuongGTQgt = {};
        $scope.LuongGTV21 = {};
        $scope.LuongGTV22 = {};
        $scope.LuongTTQtt = {};
        $scope.LuongDPQdp = {};

        var max = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        var arrTiLe = [];
        for (var i = 1; i <= 100; i++) {
            arrTiLe.push(i);
        }

        function loadCbxLoaiCt(arr) {
            var objTemp = {
                data:[]
            }

            for (var i = 0; i < arr.length; i++) {
                objTemp.data.push({
                    value: arr[i],
                    name: arr[i]
                });
            }
            return objTemp.data;
        }

        function LoadCbxHopDong() {
            service.get('api/hd/getall', null, function (result) {
                $scope.HopDong = result.data;
            }, function (error) {
            });
          
        }

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

        LoadCbxHopDong();
        $scope.LoaiCongTrinh = loadCbxLoaiCt(max);
        $scope.TyLeTheoDT = loadCbxLoaiCt(arrTiLe); 
        $scope.TongChiQL = loadCbxLoaiCt(arrTiLe);
        $scope.LuongGTQgt = loadCbxLoaiCt(arrTiLe);
        $scope.LuongGTV21 = loadCbxLoaiCt(arrTiLe);
        $scope.LuongGTV22 = loadCbxLoaiCt(arrTiLe);
        $scope.LuongTTQtt = loadCbxLoaiCt(arrTiLe);
        $scope.LuongDPQdp = loadCbxLoaiCt(arrTiLe);

        function cancel() {
            $mdDialog.cancel();
        }

        function addDuan() {
            service.post('api/duan/created', $scope.ObjDuAn, function (result) {
                notification.success('Thêm bản ghi thành công !');
                $mdDialog.cancel();
                LoadData();
            }, function (error) {
            });
           
        }

        function viewDetail(id) {
            var config = {
                params: {
                    id:id
                }
            }
            service.get('api/duan/getdetail', config, function (result) {
                $scope.Detail = result.data;
            }, function (error) {
            });
        }

        // Function Delete a record
        $scope.delete = function (id) {
            if (id != null) {
                $ngBootbox.confirm('bạn có chắc chắn muốn xóa không ? ').then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    service.del('api/duan/delete', config, function (result) {

                        notification.success('xóa dữ liệu thành công !');
                        LoadData();
                    }, function (error) {

                    });
                });
            } else {
                notification.error('vui lòng chọn dự án cần xóa');
            }
         
        }

        $scope.edit = function edit(id, ev) {
            if (id != null) {
                var config = {
                    params: {
                        id: id
                    }
                }
                $mdDialog.show({
                        locals: {
                            items: $scope.items,
                        },
                        controller: 'DuAnController',
                        templateUrl: '/app/components/duan/edit.html',
                        parent: angular.element(document.body),
                        targetEvent: ev,
                        clickOutsideToClose: false,
                        fullscreen: $scope.customFullscreen,
                        scope: $scope,
                        preserveScope: true
                     })
                    .then(function (answer) {
                        $scope.status = 'You said the information was "' + answer + '".';
                    }, function () {
                        $scope.status = 'You cancelled the dialog.';
                    });
                service.get('api/duan/getdetail', config, function (result) {
                    $scope.items = result.data;
                }, function (error) {
                });
            } else {
                notification.error('vui lòng chọn dự án cần chỉnh sửa ');
            }
        }

        $scope.update = function () {
            service.put('api/duan/update', $scope.items, function (result) {
                $mdDialog.cancel();
                notification.success("cập nhật dự án số " + result.data.ID + " thành công");
                LoadData();
            }, function (error) {
            });
        }
        function LoadData() {
            service.get('api/duan/getall', null, function (result) {
                $scope.ListDuAn = result.data;
            }, function (error) {
            });
        }
        LoadData();
    }
})(angular.module('QLdaConfig'));