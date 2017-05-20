(function (app) {
    app.controller('DuAnController', DuAnController);

    DuAnController.$inject = ['$scope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox'];
    function DuAnController($scope, service, notification, $state, $mdDialog, $ngBootbox) {
        
        $scope.ListKhachHang = {};
        $scope.showAdd = showAdd;
        $scope.addDuan = addDuan;
        $scope.showDetail = showDetail;
        $scope.cancel = cancel;
        $scope.LoadData = LoadData;
        $scope.viewDetail = viewDetail;
        $scope.showAllEplo = showAllEplo;
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
        $scope.detail = {};
        $scope.LoaiCongTrinh = {};
        $scope.TyLeTheoDT = {};
        $scope.TongChiQL = {};
        $scope.LuongGTQgt = {};
        $scope.LuongGTV21 = {};
        $scope.LuongGTV22 = {};
        $scope.LuongTTQtt = {};
        $scope.LuongDPQdp = {};
        $scope.loadding = false;


        $scope.items = {};
        $scope.EmployeeDirect = {};
        $scope.EmployeeInDirect = {};

        //pagination
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';

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

        function LoadKhachHang() {
            service.get('api/kh/getcustomer', null, function (result) {
                $scope.ListKhachHang = result.data;
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
                fullscreen: $scope.customFullscreen ,// Only for -xs, -sm breakpoints.
                scope: $scope,
                preserveScope: true
                })
               .then(null, null);
        }

        function showDetail(ev, id) {
            var config = {
                params: {
                    id: id
                }
            }
            $mdDialog.show({
                locals: {
                    detail: $scope.detail,
                },
                controller: 'DuAnController',
                templateUrl: '/app/components/duan/chitiet.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,// Only for -xs, -sm breakpoints.
                scope: $scope,
                preserveScope: true
            }).then(null, null);

            service.get('api/duan/getdetail', config, function (result) {
                $scope.detail = result.data;
                $scope.detail.NamQuyetToan = new Date(result.data.NamQuyetToan);
                $scope.detail.NgayKy = new Date(result.data.NgayKy);
                $scope.detail.NgayBatDau = new Date(result.data.NgayBatDau);
                $scope.detail.NgayKetThuc = new Date(result.data.NgayKetThuc);
            }, function (error) {
            });
        }

        LoadKhachHang();
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
            $scope.loadding = true;
            service.post('api/duan/created', $scope.ObjDuAn, function (result) {
                notification.success('Thêm dự án thành công thành công !');
                $mdDialog.cancel();
                $scope.LoadData();
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
                var config = {
                    params: {
                        id: id
                    }
                }
                service.get('api/duan/getbyid', config, function (result) {
                    
                    console.log(result.data);
                    if (result.data.HangMuc.length > 0) {
                        $ngBootbox.alert('Xóa hết các hạng mục công việc trước khí xóa dự án ?', 'Oops!').then(null);
                    } else {
                        $ngBootbox.confirm('bạn có chắc chắn muốn xóa không ? ').then(function () {
                            service.del('api/duan/delete', config, function (result) {
                                notification.success('xóa dữ liệu thành công !');
                                LoadData();
                            }, function (error) { });
                        });
                    }
                }, function (error) { });
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
                    .then(null,null);
                service.get('api/duan/getdetail', config, function (result) {
                    $scope.items = result.data;
                    $scope.items.NamQuyetToan = new Date(result.data.NamQuyetToan);
                    $scope.items.NgayKy = new Date(result.data.NgayKy);
                    $scope.items.NgayBatDau = new Date(result.data.NgayBatDau);
                    $scope.items.NgayKetThuc = new Date(result.data.NgayKetThuc);
                }, function (error) {
                });
            } else {
                notification.error('vui lòng chọn dự án cần chỉnh sửa ');
            }
        }

        $scope.update = function () {
            $scope.loadding = true;
            service.put('api/duan/update', $scope.items, function (result) {
                $mdDialog.cancel();
                $scope.LoadData();
                notification.success("cập nhật dự án số " + result.data.ID + " thành công");
            }, function (error) {
            });
        }
        $scope.refresh = function () {
            LoadData();
        }

        function LoadData(page) {
            $scope.loadding = true;
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 5,
                    keyword:$scope.keyword
                }
            }
            service.get('api/duan/getall', config, function (result) {
                $scope.loadding = false;
                $scope.ListDuAn = result.data.items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
            });
        }

        function showAllEplo(ev, id) {
            var config = {
                params: {
                    idDuan: id,
                    loaiHm: 1
                }
            }
            service.get('api/duan/income', config, function (result) {
                $scope.EmployeeInDirect = result.data;
            }, function (error) { });

            var config1 = {
                params: {
                    idDuan: id,
                    loaiHm: 0
                }
            }
            service.get('api/duan/income', config1, function (result) {
                $scope.EmployeeDirect = result.data;
            }, function (error) { });


            console.log($scope.items);


            $mdDialog.show({
                locals: {
                    EmployeeDirect: $scope.EmployeeDirect,
                    EmployeeInDirect: $scope.EmployeeInDirect
                },
                controller: 'DuAnController',
                templateUrl: '/app/components/duan/ThanhVienDuAn.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                fullscreen: $scope.customFullscreen,
                scope: $scope,
                preserveScope: true
            })
          .then(null, null);

           
        }
        LoadData();
    }
})(angular.module('QLdaConfig'));