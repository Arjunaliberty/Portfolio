var model = {
    user: ko.observable(),
    displayCallForm: ko.observable(true) 
}




/* Обработка чекбоксов в фильтре */
$("#jur-radio").bind("click", function () {
    $("#jur").css("display", "block")
    $("#phys").css("display", "none");
    $("#ind").css("display", "none");
});
$("#phys-radio").bind("click", function () {
    $("#jur").css("display", "none")
    $("#phys").css("display", "block");
    $("#ind").css("display", "none");
});
$("#ind-radio").bind("click", function () {
    $("#jur").css("display", "none")
    $("#phys").css("display", "none");
    $("#ind").css("display", "block");
});
$("#domain-box").bind("click", function () {
    if (!this.checked) {
        $("#tld-box").attr("disabled", "true");
        $("#dms-box").attr("disabled", "true");
    } else {
        $("#tld-box").removeAttr("disabled");
        $("#dms-box").removeAttr("disabled");
    }
});
$("#reset-box-button").bind("click", function () {
    $("#tld-box").removeAttr("disabled");
    $("#dms-box").removeAttr("disabled");
});

/* Подсказки по услугам */
$(".lable-status-yellow").hover(function () {
        var parent = $(this).parent();
        parent.find(".lable-status-info").css("display", "block");
    },
    function () {
        var parent = $(this).parent();
        parent.find(".lable-status-info").css("display", "none");
    }
);
$(".lable-status-red").hover(function () {
    var parent = $(this).parent();
    parent.find(".lable-status-info").css("display", "block");
    },
    function () {
        var parent = $(this).parent();
        parent.find(".lable-status-info").css("display", "none");
    }
);



$(document).ready(function () {
    ko.applyBindings(model);
});

