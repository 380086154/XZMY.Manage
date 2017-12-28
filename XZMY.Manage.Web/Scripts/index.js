
$(function () {
    var btn_closetab_click = function (btn) {
        var tag = $(this).attr("tag");
        var tab = $(this).attr("tab");
        $("#tabs-li-" + tab + "-" + tag).remove();
        $("#tabs-" + tab + "-" + tag).remove();
        $("#" + tab).tabs("refresh");
    };

    var btn_newtab_click = function (btn) {
        var tag = $(this).attr("tag");
        var tab = $(this).attr("tab");
        var title = $(this).attr("title");
        var url = $(this).attr("url");

        if ($("#btn-" + tab + "-" + tag + "").length > 0) {
            $("#btn-" + tab + "-" + tag + "").click();
            return;
        }

        $("#" + tab + "-heads").append("<li id='tabs-li-" + tab + "-" + tag + "'><a href='#tabs-" + tab + "-" + tag + "' id='btn-" + tab + "-" + tag + "'>" + title + "</a><a href='#' tab='" + tab + "' tag='" + tag + "' class='btn-closetab'>x</a></li>");
        $("#" + tab).append("<div id='tabs-" + tab + "-" + tag + "'><iframe height='100%' marginheight='0' id='iframe-" + tab + "-" + tag + "' class='content-iframe' frameborder='0' scrolling='no' src='" + url + "'></div>");
        $("#" + tab).tabs("refresh");
        $(".btn-closetab").each(function (e) {
            $(this).click(btn_closetab_click);
        });
        $("#btn-" + tab + "-" + tag + "").click();

        var t = setInterval(function () {
            var ifr_el = $(".content-iframe");
            if (ifr_el.length <= 0) return;
            ifr_el.each(function (i, e) {
                var subWeb = document.frames ? document.frames[e.attr('id')].document : e.contentDocument;
                if (subWeb == null) return;
                e.style.height = (subWeb.body.scrollHeight + 50) + "px";
                clearInterval(t);
            });
        }, 50);

    };

    $(".btn-closetab").each(function (e) {
        $(this).click(btn_closetab_click);
    });

    $(".menuitem-newtab").each(function (e) {
        $(this).click(btn_newtab_click);
    });


});