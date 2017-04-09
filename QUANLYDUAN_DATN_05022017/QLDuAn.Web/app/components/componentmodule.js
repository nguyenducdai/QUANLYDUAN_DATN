/// <reference path="duanhangmuc/duanhangmucController.js" />
/// <reference path="E:\DATA_DRIVE_2017\QUANLYDUAN_DATN\QUANLYDUAN_DATN_05022017\QLDuAn.Web\Assets/librarys/angular/angular.js" />
/// <reference path="hangmuc/index.html" />
/// <reference path="" />
(function () {
    angular.module('componentmodule', ['QLdaCommon']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('thanhvien', {
            url: '/thanhvien',
            parent: 'baseTemplate',
            templateUrl: '/app/components/thanhvien/index.html',
            controller: 'thanhvienController'
        })
         .state('nhomnguoidung', {
             url: '/dsnguoidung',
             parent: 'baseTemplate',
             templateUrl: '/app/components/nhomnguoidung/dsnhomnguoidung.html',
             controller: 'dsnhomnguoidungController'
         })
        .state('suanhomnguoidung', {
            url: '/suanhomnguoidung/:id',
            parent: 'baseTemplate',
            templateUrl: '/app/components/nhomnguoidung/suanhomnguoidung.html',
            controller: 'suanhomnguoidungController'
        })
        .state('themnhomnguoidung', {
            url: '/themnhomnguoidung',
            parent: 'baseTemplate',
            templateUrl: '/app/components/nhomnguoidung/themnhomnguoidung.html',
            controller: 'themnhomnguoidungController'
        })

        .state('hopdong', {
            url: '/hopdong',
            parent: 'baseTemplate',
            templateUrl: '/app/components/hopdong/index.html',
            controller: 'danhsachHdController'
        })
        .state('hd-create', {
            url: '/hd/themmoi',
            parent: 'baseTemplate',
            templateUrl: '/app/components/hopdong/add.html',
            controller: 'ThemHdController'
        })
         .state('duan', {
             url: '/duan',
             parent: 'baseTemplate',
            templateUrl: '/app/components/duan/index.html',
            controller: 'DuAnController'
         })
        .state('themhangmuc', {
            url: '/themhangmuc/:idDuAn-:LoaiHM',
            parent: 'baseTemplate',
            templateUrl: '/app/components/duanhangmuc/add.html',
            controller: 'themduanhangmucController'
        })
        .state('chitietduan', {
            url: '/chitet/:id',
           // parent: 'baseTemplate',
             templateUrl: '/app/components/duanhangmuc/chitetduan.html',
             controller: 'chitietduanController'
        })
         .state('hm_edit', {
             url: '/sua-hang-muc/:IdHangMuc',
             parent: 'baseTemplate',
            templateUrl: '/app/components/duanhangmuc/edithm.html',
            controller: 'suaduanhmController'
         })
         .state('khachhang', {
             url: '/khachhang',
             parent: 'baseTemplate',
             templateUrl: '/app/components/khachhang/danhsachkhachhang.html',
             controller: 'danhsachkhachhangController'
         })
        .state('thongbao', {
            url: '/thong-bao',
            parent: 'baseTemplate',
            templateUrl: '/app/components/thongbao/thongbao.html',
            controller: 'thongbaoController'
        })
         .state('themthongbao', {
             url: '/them-thong-bao',
             parent: 'baseTemplate',
             templateUrl: '/app/components/thongbao/themthongbao.html',
             controller: 'themthongbaoController'
         })
         .state('suathongbao', {
             url: '/cap-nhat-thong-bao/:id',
             parent: 'baseTemplate',
             templateUrl: '/app/components/thongbao/suathongbao.html',
             controller: 'suathongbaoController'
         });
    }
})();