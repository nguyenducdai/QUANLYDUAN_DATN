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

    app.filter('toDate', function () {
        return function (input) {
            var date = new Date();
            var dd = date.getDate();
            var mm = date.getMonth() + 1; //January is 0!

            var yyyy = date.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var date = mm + '/' + dd + '/' + yyyy;
            return date;
        }
    });

    app.filter('number_fix', function () {
        return function (amount) {
          
            return amount;
        };
    });
    
})(angular.module('QLdaCommon'));