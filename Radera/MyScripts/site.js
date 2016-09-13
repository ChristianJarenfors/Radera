// site.js
(function () {
    
    //Menu toggle 2

    var $sidebarAndWrapper = $("#sidebar, #wrapper");
    var $icon = $("#menuToggle i.glyphicon");

    $("#menuToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("glyphicon-chevron-left");
            $icon.addClass("glyphicon-chevron-right");
        }
        else {
            $icon.addClass("glyphicon-chevron-left");
            $icon.removeClass("glyphicon-chevron-right");
        }
    });

})();