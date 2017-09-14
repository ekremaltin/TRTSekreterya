$(function () {   

    var tabs = $("#tabs").tabs();

    // Modal dialog init: custom buttons and a "close" callback resetting the form inside
    var dialog = $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        height: 700 ,
        width: 500,
        resizable: false,
        buttons: {
            Ekle: function () {
                addTab();
                $(this).dialog("close");
            },
            Kapat: function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            form[0].reset();
        },

        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }

    });



    // AddTab form: calls addTab function on submit and closes the dialog
    var form = dialog.find("form").on("submit", function (event) {
        addTab();
        dialog.dialog("close");
        event.preventDefault();
    });

    $('#btnSaveTab').button().on("click", function () {
        addTab();
        $('#myModaltab').modal('hide');
    });

    // Actual addTab function: adds new tab using the input from the form above
    function addTab() {       
        $.ajax({
            type: 'POST',
            url: '/Kisi/ChangeTakvimKey',
            data: { 'ids': $("#tabKeySelect").val() },
            datatype: 'json',
            success: function (data) {
                if (data.status) {
                    location.reload();
                }                
            },
            error:function () {
                alert('En az bir kişi seçili olmalı');
            }            
        });        
    }

    // AddTab button: just opens the dialog
    $("#add_tab")
      .button()
      .on("click", function () {
          $('#myModaltab').modal({
              show: {
                  effect: "blind",
                  duration: 1000
              },
              hide: {
                  effect: "explode",
                  duration: 1000
              }
          });
          //dialog.dialog("open");
      });

    // Close icon: removing the tab on click
    tabs.on("click", "span.ui-icon-close", function () {
        var panelId = $(this).closest("li").remove().attr("aria-controls");
        $("#" + panelId).remove();
        tabs.tabs("refresh");
    });

    tabs.on("keyup", function (event) {
        if (event.altKey && event.keyCode === $.ui.keyCode.BACKSPACE) {
            var panelId = tabs.find(".ui-tabs-active").remove().attr("aria-controls");
            $("#" + panelId).remove();
            tabs.tabs("refresh");
        }
    });

    $(document).on('click', 'li[id^=sb]', function (e) {
        var ID = $(this).children().children().attr('id');
        var text = $(this).children().attr('href');
        $('#whichTab').val(text);   
        $('#tabKisiID').val(ID);
        FetchEventAndRenderCalendar(ID);
    })
});