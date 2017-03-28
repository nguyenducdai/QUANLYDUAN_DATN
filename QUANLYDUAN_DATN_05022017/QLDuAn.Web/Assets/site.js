var common = {
    init: function () {
        common.registerEvent();
    },

    registerEvent : function(){
        $('#IdentityLogout').off('click').on('click', function (e) {
            e.preventDefault();
            alert("ok");
        });
    }
}
common.init();