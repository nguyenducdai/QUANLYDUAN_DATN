(function (app) {
    app.controller('chitietduanController', chitietduanController);
    chitietduanController.$inject = ['$scope', '$rootScope', 'service', 'notification', '$state', '$mdDialog', '$ngBootbox', '$stateParams', '$filter'];

    function chitietduanController($scope, $rootScope, service, notification, $state, $mdDialog, $ngBootbox, $stateParams, $filter) {

        $scope.ThongTinDuAn = {};

        $scope.cbxTrangThai = [
            {
                "TrangThai": true,
                "TenHienThi" :"hoàn thành"
            },
            {
                "TrangThai": false,
                "TenHienThi": " chưa hoàn thành"
            }
        ]

        $rootScope.cbxListDa = {};

        $rootScope.cbxSelected = {};

        $scope.HangMucDuAnTT = {};

        $scope.HangMucDuAnGt = {};

        $scope.LoaiHangMucDaTt = LoaiHangMucDaTt;

        $scope.movebin = movebin;

        $rootScope.changeSelected = changeSelected;

        $rootScope.exportExcel = exportExcel;

        $rootScope.exportPDF = exportPDF;

        $scope.filter = filter;

        $scope.selectAllTT = selectAllTT;
        $scope.selectAllGT = selectAllGT;

        $scope.deleteAll = deleteAll;

        $scope.checkDate = checkDate;

        $scope.upadteStatus = upadteStatus;

        $rootScope.fullScreen = fullScreen;

        $rootScope.totalPoint = totalPoint;

        $scope.isActive = 0;

        $scope.incomeDirect = {}
        $scope.incomeIndirect = {}
        $scope.seleted = {}
        $scope.seletedGt = {}

        $scope.loadding = false;
        $scope.loaddingbar = false;

        function checkDate(rowDate, status) {
            var current = $filter('date')(new Date(), 'dd-MM-yyyy');
            var compare = $filter('date')(rowDate, 'dd-MM-yyyy');

            var dateNow = parseDate(current).getTime();
            var endDate = compare ? parseDate(compare).getTime():null;
            if (dateNow > endDate && !status && endDate != null) {
                return -1;
            } else if (dateNow == endDate && !status && endDate != null) {
                return 1;
            } else if(status){
                return 0;
            }
        }

        function parseDate(str) {
            var mdy = str.split('-');
            return new Date(mdy[2], mdy[1], mdy[0]);
        }
        
        function totalPoint() {
            var effect = 'slide';
            var options = { direction: 'right' };
            var duration = 200;
            $('#right-sidebar-nav').toggle(effect, options, duration);
            loadInCome(0);
            loadInCome(1);
        }
   
        function loadInCome(loaiHm) {
            $scope.loaddingbar = true;
            var config = {
                params: {
                    idDuan: $rootScope.cbxSelected.ID,
                    loaiHm: loaiHm
                }
            }
            service.get('api/duan/income', config, function (result) {
                $scope.loaddingbar = false;
                if (loaiHm == 0) {
                    $scope.incomeDirect = result.data;
                } else {
                    $scope.incomeIndirect = result.data;
                }
            }, function (error) {
                notification.error('có lỗi trong quá trình sử lý');
            })
        }

        function fullScreen() {
            var el = document.body;
            var requestMethod = el.requestFullScreen || el.webkitRequestFullScreen || el.mozRequestFullScreen || el.msRequestFullscreen;
            if (requestMethod) {
                requestMethod.call(el);
            } else if (typeof window.ActiveXObject !== "undefined") { // Older IE.
                var wscript = new ActiveXObject("WScript.Shell");
                if (wscript !== null) {
                    wscript.SendKeys("{F11}");
                }
            }
        }

        function deleteAll(loaihm) {
            $ngBootbox.confirm("bạn có chắc chắn muốn xóa các hạng mục được chọn không ? ").then(function () {
                var listID = [];
                if (loaihm == 0) {
                    $.each($scope.seleted, function (i, item) {
                        listID.push(item.ID);
                    });
                    var config = {
                        params: {
                            listId: JSON.stringify(listID)
                        }
                    }
                    service.get('api/hm/movebinmuti', config, function (result) {
                        displayGT(loaihm);
                        notification.success('Đã di chuyển ' + listID.length + 'hạng mục vào thùng rác thành công');
                    }, function (error) { });
                } else {
                    $.each($scope.seletedGt, function (i, item) {
                        listID.push(item.ID);
                    });
                    console.log(listID);
                    var config = {
                        params: {
                            listId: JSON.stringify(listID)
                        }
                    }

                    service.get('api/hm/movebinmuti', config, function (result) {
                        displayGT(loaihm);
                        notification.success('Đã di chuyển ' + listID.length + 'hạng mục vào thùng rác thành công');
                    }, function (error) { });
                }


            });
          
           
        }

        $scope.isAll = false;
        function selectAllTT() {
            if ($scope.isAll === false) {
                angular.forEach($scope.HangMucDuAnTT, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.HangMucDuAnTT, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }

         
        }

        $scope.isAllGt = false;
        function selectAllGT() {
            if ($scope.isAllGt == false) {
                angular.forEach($scope.HangMucDuAnGt, function (item) {
                    item.checked = true;
                });
                $scope.isAllGt = true;
            } else {
                angular.forEach($scope.HangMucDuAnGt, function (item) {
                    item.checked = false;
                });
                $scope.isAllGt = false;
            }
           
        }

    

 
        $scope.$watch("HangMucDuAnTT", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.seleted = checked;
                $(".btn_delall").removeAttr('disabled');
            } else {
                $(".btn_delall").attr('disabled', 'disabled');
            }

        }, true);

        $scope.$watch("HangMucDuAnGt", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.seletedGt = checked;
                $(".btn_delall").removeAttr('disabled');
            } else {
                $(".btn_delall").attr('disabled', 'disabled');
            }

        }, true);


        function upadteStatus(id,status,stProject) {
            var config = {
                params: {
                    id: id,
                    status: status
                }
            }
            service.get('api/hm/updatestatus', config, function (result) {
                displayGT(stProject);
                notification.success('cập nhật trạng thái thành công !');
            }, function (error) {
            });
        }
        //pagination
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.valueFilter = '';


        $scope.displayGT = displayGT;
        var flag = '';

        function filter(loaihangmuc) {
            LoaiHangMucDaTt(loaihangmuc);
        }

        function LoadData(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 5,
                    keyword: $scope.keyword
                }
            }
            service.get('api/duan/getall', config, function (result) {
                $rootScope.cbxListDa = result.data.items;
                if (!$stateParams.id) {
                    $rootScope.cbxSelected.ID = $rootScope.cbxListDa[0].ID;
                } else {
                    $rootScope.cbxSelected.ID = parseInt($stateParams.id);
                }
                LoadById();
                LoaiHangMucDaTt(0);
            }, function (error) {
            });
        }

        function displayGT(loaihangmuc) {
            flag = loaihangmuc;
            $scope.isActive = loaihangmuc;
            LoaiHangMucDaTt(loaihangmuc);
        }
        //loai hang muc tt=0
        function LoaiHangMucDaTt(loaihangmuc, page) {
            $scope.loadding = true;
            page = page || 0;
            var config = {
                params: {
                    idDuAn: $rootScope.cbxSelected.ID,
                    LoaiHm: loaihangmuc,
                    page: page,
                    pageSize: 10,
                    keyword: $scope.keyword,
                    filter: $scope.valueFilter

                }
            }

            service.get('api/hm/gethangmucduan', config, function (result) {
                $scope.loadding = false;
                if (loaihangmuc == 0) {
                    $scope.HangMucDuAnTT = result.data.items;
                    for (var i = 0; i < $scope.HangMucDuAnTT.length; i++) {
                        if ($scope.HangMucDuAnTT[i].HeSoTg != null) {
                            $scope.HangMucDuAnTT[i].DiemHM = $scope.HangMucDuAnTT[i].DiemDanhGia * $scope.HangMucDuAnTT[i].NhomCongViec.HeSoCV * $scope.HangMucDuAnTT[i].HeSoTg.HeSoTgdk * $scope.HangMucDuAnTT[i].HeSoLap.Hesl * $scope.HangMucDuAnTT[i].HesoKcn;
                        } else {
                            $scope.HangMucDuAnTT[i].DiemHM = 0;
                        }
                        
                    }
                } else {
                    $scope.HangMucDuAnGt = result.data.items;
                    for (var i = 0; i < $scope.HangMucDuAnGt.length; i++) {
                        if ($scope.HangMucDuAnGt[i].HeSoTg != null) {
                            $scope.HangMucDuAnGt[i].DiemHM = $scope.HangMucDuAnGt[i].DiemDanhGia * $scope.HangMucDuAnGt[i].NhomCongViec.HeSoCV * $scope.HangMucDuAnGt[i].HeSoTg.HeSoTgdk * $scope.HangMucDuAnGt[i].HeSoLap.Hesl * $scope.HangMucDuAnGt[i].HesoKcn;
                        }
                        else {
                            $scope.HangMucDuAnGt[i].DiemHM = 0;
                        }
                    }
                }
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;
                
            }, function (error) {
            });
        }

        function movebin(id, loaiHm) {

            $ngBootbox.confirm("bạn có chắc chắn muốn xóa không ? ").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                service.get('api/hm/movebin', config, function (result) {
                    displayGT(loaiHm);
                    notification.success('Đã di chuyển hạng mục ' + id + ' vào thùng rác ');
                }, function (error) { });
            });
           
        }

        function LoadById() {
            var config = {
                params: {
                    id: $rootScope.cbxSelected.ID
                }
            }
            service.get('api/duan/getInfo', config, function (result) {
                $scope.ThongTinDuAn = result.data;
            }, function (error) {
            });
        }

        function changeSelected() {
                LoadById();
            if (flag = '' || flag==0) {
                displayGT(0)
            } else {
                displayGT(1);
            }
        }

        function exportExcel() {
            $scope.loadding = true;
            var config = {
                params: {
                    idDuAn : $rootScope.cbxSelected.ID
                }
            }
            service.get('api/duan/exportexcel', config, function (result) {
                $scope.loadding = false;
                if (result.status = 200) {
                    window.location.href = result.data.Message;
                    notification.success('tải xuống file excel thành công');
                }
            }, function (error) {
                notification.success('lỗi xuất file');
                $scope.loadding = false;
            });
        }

        function exportPDF() {
            $scope.loadding = true;
            var config = {
                params: {
                    idDuAn: $rootScope.cbxSelected.ID
                }
            }
            service.get('api/duan/exportpdf', config, function (result) {
                $scope.loadding = false;
                if (result.status = 200) {
                    window.location.href = result.data;
                    notification.success('tải xuống pdf thành công');
                }
            }, function (error) {
                notification.success('lỗi xuất file');
                $scope.loadding = false;
            });
        }

        LoadData();
    }

})(angular.module('QLdaConfig'));