(function (app) {
    app.filter('ConverFilter', function () {
        return function (input , str1 , str2) {
            if (input == true) {
                return str1;
            } else {
                return str2;
            }
        }

    });
    
})(angular.module('QLdaCommon'));