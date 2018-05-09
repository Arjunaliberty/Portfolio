$(document).ready(function () {


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
    $(".lable-status-yellow").hover( function () {
            var parent = $(this).parent();
            parent.find(".lable-status-info").css("display", "block");
        },
        function () {
            var parent = $(this).parent();
            parent.find(".lable-status-info").css("display", "none");
    });

    $(".lable-status-red").hover( function () {
            var parent = $(this).parent();
            parent.find(".lable-status-info").css("display", "block");
        },
        function () {
            var parent = $(this).parent();
            parent.find(".lable-status-info").css("display", "none");
        });
    /* Обработка клика по ссылке сервиса и отправки данных на сервер*/
    $('[data-ajax-link]').bind("click", function () {
        $('#call-form-container').remove();
        $('#call-form-container').css("display", "block");
        var link = $(this).attr('data-ajax-link');
        $.ajax({
            type: "GET",
            url: link,
            dataType: "json",
            success: function (data) {
                $('body').append('<div id="call-form-container"></div >');
                $('#call-form-container').append('<div class="call-form-view"></div>');
                $('.call-form-view').append('<form id="call-form" action="/Call/SaveLog/' + data.CurrentService +'" method="post"></form>');
                $('.call-form-view').append('<div class="call-form-history"></div >');
                $('#call-form').append('<div id="call-form-content"></div>');
                $('#call-form').append('<div class="call-form-view-button"><button id="#call-form-button-submit" type="submit"><b>Сохранить</b></button ><button type="reset">Очистить</button><button id="call-form-button-close" type="button">Закрыть</button></div >');
                if (data.User.ContractType == 1) {
                    $('#call-form-content').append(
                        '<div id="call-form-user-name">' + data.User.LegalRequisites.NameOrganization + '</div>'
                        + '<div id="call-form-user-info">Юр.<span style="padding-left: 1.8em">' + data.User.Email + '</span><span style="padding-left: 1.8em">' + data.User.LegalRequisites.Email + '</span></div>'
                        + '<div id="call-form-user-phone"><span><input type="radio" name="Phone" value="' + data.User.LegalRequisites.Phone + '" />' + data.User.LegalRequisites.Phone + '</span><span style="padding-left: 1.8em"><input type="radio" name="Phone" value="' + data.User.LegalRequisites.Fax + '" />' + data.User.LegalRequisites.Fax + '.ф</span><span style="padding-left: 1.8em"><input id="additional-phone-control" type="radio" name="Phone" /></span><span><input id="additional-phone" type="text" name="Phone" placeholder="Другой номер" disabled /></span></div >'
                    );
                    for (var i = 0; i < data.User.Services.length; i++) {
                        $('#call-form-content').append(
                            '<div id="call-form-user-services"><span style="padding-left: 1.8em">' + (data.User.Services[i].ServiceType == 1 ? 'Домен' : 'Хост') + '</span><span style="padding-left: 1.8em">' + data.User.Services[i].Name + '</span><span style="padding-left: 1.8em">' + data.User.Services[i].ExDate.substring(0, 10) + '</span>'
                            + '<div><select name="Result"><option value="" selected >Выбрать результат звонка</option ><option value="Уведомлен, оплатят">Уведомлен, оплатят</option><option value="Уведомлен, отказ">Уведомлен, отказ</option><option value="Не берет трубку">Не берет трубку</option><option value="Временно не доступен">Временно не доступен</option><option value="Номер занят">Номер занят</option><option value="Контакты не актуальны">Контакты не актуальны</option><option value="Иное">Иное</option></select>'
                            + '<textarea name="Comment" placeholder="Примечание" rows="1" cols="250"></textarea> </div></div><hr /></div>'

                        );
                    }
                    $('.call-form-history').append('<span>История звонков</span>');
                    for (var i = 0; i < data.User.Services.length; i++) {
                        $('.call-form-history').append(
                            '<hr /><div><span>' + (data.User.Services[i].ServiceType == 1 ? 'Домен' : 'Хост') + '</span><span>' + data.User.Services[i].Name + '</span><span>' + (data.User.Services["0"].CallLog.Result != null ? data.User.Services["0"].CallLog.Result : 'No data') + '</span><span>' + (data.User.Services["0"].CallLog.Phone != null ? data.User.Services["0"].CallLog.Phone : 'No data') + '</span><span>' + (data.User.Services["0"].CallLog.Employee.Role == 3 ? 'administrator' : 'No data') + '</span><span></span><div>' + (data.User.Services["0"].CallLog.Comment == null ? data.User.Services["0"].CallLog.Comment : 'No data') +'</div></div>'
                        );
                    }
                }
                if (data.User.ContractType == 2) {
                    $('#call-form-content').append(
                        '<div id="call-form-user-name">' + data.User.PhysicalRequisites.FIO + '</div>'
                        + '<div id="call-form-user-info">Физ.<span style="padding-left: 1.8em">' + data.User.Email + '</span><span style="padding-left: 1.8em">' + data.User.LegalRequisites.Email + '</span></div>'
                        + '<div id="call-form-user-phone"><span><input type="radio" name="Phone" value="' + data.User.PhysicalRequisites.Phone + '" />' + data.User.PhysicalRequisites.Phone + '</span><span style="padding-left: 1.8em"><input type="radio" name="Phone" value="' + data.User.PhysicalRequisites.Fax + '" />' + data.User.PhysicalRequisites.Fax + '.ф</span><span style="padding-left: 1.8em"><input id="additional-phone-control" type="radio" name="Phone" /></span><span><input id="additional-phone" type="text" name="Phone" placeholder="Другой номер" disabled /></span></div >'
                    );
                    for (var i = 0; i < data.User.Services.length; i++) {
                        $('#call-form-content').append(
                            '<div id="call-form-user-services"><span style="padding-left: 1.8em">' + (data.User.Services[i].ServiceType == 1 ? 'Домен' : 'Хост') + '</span><span style="padding-left: 1.8em">' + data.User.Services[i].Name + '</span><span style="padding-left: 1.8em">' + data.User.Services[i].ExDate.substring(0, 10) + '</span>'
                            + '<div><select name="Result"><option value="" selected > Выбрать результат звонка</option ><option value="Уведомлен, оплатят">Уведомлен, оплатят</option><option value="Уведомлен, отказ">Уведомлен, отказ</option><option value="Не берет трубку">Не берет трубку</option><option value="Временно не доступен">Временно не доступен</option><option value="Номер занят">Номер занят</option><option value="Контакты не актуальны">Контакты не актуальны</option><option value="Иное">Иное</option></select>'
                            + '<textarea name="Comment" placeholder="Примечание" rows="1" cols="250"></textarea> </div></div><hr /></div>'

                        );
                    }
                    $('.call-form-history').append('<span>История звонков</span>');
                    for (var i = 0; i < data.User.Services.length; i++) {
                        $('.call-form-history').append(
                            '<hr /><div><span>' + (data.User.Services[i].ServiceType == 1 ? 'Домен' : 'Хост') + '</span><span>' + data.User.Services[i].Name + '</span><span>' + (data.User.Services["0"].CallLog.Result != null ? data.User.Services["0"].CallLog.Result : 'No data') + '</span><span>' + (data.User.Services["0"].CallLog.Phone != null ? data.User.Services["0"].CallLog.Phone : 'No data') + '</span><span>' + (data.User.Services["0"].CallLog.Employee.Role == 3 ? 'administrator' : 'No data') + '</span><span></span><div>' + (data.User.Services["0"].CallLog.Comment == null ? data.User.Services["0"].CallLog.Comment : 'No data') + '</div></div>'
                        );
                    }
                }
                if (data.User.ContractType == 3) {
                    $('#call-form-content').append(
                        '<div id="call-form-user-name">' + data.User.IndividualRequisites.FIO + '</div>'
                        + '<div id="call-form-user-info">ИП<span style="padding-left: 1.8em">' + data.User.Email + '</span><span style="padding-left: 1.8em">' + data.User.LegalRequisites.Email + '</span></div>'
                        + '<div id="call-form-user-phone"><span><input type="radio" name="Phone" value="' + data.User.IndividualRequisites.Phone + '" />' + data.User.IndividualRequisites.Phone + '</span><span style="padding-left: 1.8em"><input type="radio" name="Phone" value="' + data.User.IndividualRequisites.Fax + '" />' + data.User.IndividualRequisites.Fax + '.ф</span><span style="padding-left: 1.8em"><input id="additional-phone-control" type="radio" name="Phone" /></span><span><input id="additional-phone" type="text" name="Phone" placeholder="Другой номер" disabled /></span></div >'
                    );

                    for (var i = 0; i < data.User.Services.length; i++) {
                        $('#call-form-content').append(
                            '<div id="call-form-user-services"><span style="padding-left: 1.8em">' + (data.User.Services[i].ServiceType == 1 ? 'Домен' : 'Хост') + '</span><span style="padding-left: 1.8em">' + data.User.Services[i].Name + '</span><span style="padding-left: 1.8em">' + data.User.Services[i].ExDate.substring(0, 10) + '</span>'
                            + '<div><select name="Result"><option value="" selected > Выбрать результат звонка</option ><option value="Уведомлен, оплатят">Уведомлен, оплатят</option><option value="Уведомлен, отказ">Уведомлен, отказ</option><option value="Не берет трубку">Не берет трубку</option><option value="Временно не доступен">Временно не доступен</option><option value="Номер занят">Номер занят</option><option value="Контакты не актуальны">Контакты не актуальны</option><option value="Иное">Иное</option></select>'
                            + '<textarea name="Comment" placeholder="Примечание" rows="1" cols="250"></textarea> </div></div><hr /></div>'

                        );
                    }
                    $('.call-form-history').append('<span>История звонков</span>');
                    for (var i = 0; i < data.User.Services.length; i++) {
                        $('.call-form-history').append(
                            '<hr /><div><span>' + (data.User.Services[i].ServiceType == 1 ? 'Домен' : 'Хост') + '</span><span>' + data.User.Services[i].Name + '</span><span>' + (data.User.Services["0"].CallLog.Result != null ? data.User.Services["0"].CallLog.Result : 'No data') + '</span><span>' + (data.User.Services["0"].CallLog.Phone != null ? data.User.Services["0"].CallLog.Phone : 'No data') + '</span><span>' + (data.User.Services["0"].CallLog.Employee.Role == 3 ? 'administrator' : 'No data') + '</span><span></span><div>' + (data.User.Services["0"].CallLog.Comment == null ? data.User.Services["0"].CallLog.Comment : 'No data') + '</div></div>'
                        );
                    }
                }

                /* Обработчик на кнопку закрыть */
                $('#call-form-button-close').bind('click', function () {
                    //$('#call-form-container').css("display", "none");
                    $('#call-form-container').remove();
                });
                /* Обработчик на кнопку сохранить */
                $('#call-form-button-submit').bind('click', function () {
                    //$('#call-form-container').css("display", "none");
                    $('#call-form-container').remove();
                });
                /* Обработка чекбокса дополнительного номера */
                $('#additional-phone-control').bind('click', function () {
               
                });
            }
        });
    });
 });