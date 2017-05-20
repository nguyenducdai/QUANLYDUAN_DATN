(function (app) {
    app.service('notification', notification);

    function notification() {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        };

        function success(msg) {
            toastr.success(msg);
        }

        function info(msg) {
            toastr.info(msg);
        }

        function warning(msg) {
            toastr.warning(msg);
        }
        function error(msg) {
            toastr.error(msg);
        }

        return {
            success: success,
            info: info,
            warning: warning,
            error: error
        }
    }

})(angular.module('QLdaCommon'));