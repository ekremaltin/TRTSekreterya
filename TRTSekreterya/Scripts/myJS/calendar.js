$(document).ready(function () {
    var idKisi = $('#tabKisiID').val();
    FetchEventAndRenderCalendar(idKisi);
    $('#txtStart,#txtEnd').datetimepicker();

});
var events=[];
$('#chkIsFullDay').change(function () {
    if ($(this).is(':checked')) {
        $('#divEndDate').hide();
    }
    else {
        $('#divEndDate').show();
    }
});
function getKisiAd(kisi) {
    var isimler = [];
    for (var i = 0; i < kisi.length; i++) {
        isimler.push(kisi[i].pkKisiAdi);
    }
    return isimler;
}
function getKisiID(kisi) {
    var idlist = [];
    for (var i = 0; i < kisi.length; i++) {
        idlist.push(kisi[i].pkKisiID);
    }
    return idlist;
}
function getSahipID(kisi) {
    var idlist = [];
    for (var i = 0; i < kisi.length; i++) {
        if (kisi[i].pkisSource == true) {
            idlist.push(kisi[i].pkKisiID);
        }
    }
    return idlist;
}
function FetchEventAndRenderCalendar(ID) {
    events = [];
    $.ajax({
        type: "post",
        url: "/Takvim/GetEvents",        
        data: { 'id': ID },
        success: function (data) {
            $.each(data, function (i, v) {
                events.push({
                    eventID: v.planID,
                    title: v.planKisaBilgi,
                    description: v.planUzunBilgi,
                    start: moment(v.planStartTarih),
                    end: v.planEndTarih != null ? moment(v.planEndTarih) : null,
                    place: v.planMekan,
                    color: v.planColor,
                    allDay: v.planFullDay,
                    isComp: v.planisCompleted,
                    ekBilgi: v.planEkBilgi,
                    olusturan: v.planUserID,
                    kisiler: v.planToKisis
                });
            })
            GenerateCalender(events);
        },
        error: function (error) {
            alert('Takvim yüklenemedi!.');
        }
    })
}
function GenerateCalender(events) {
    $($('#whichTab').val()).fullCalendar('destroy');
    $($("#whichTab").val()).empty();
    $($('#whichTab').val()).fullCalendar({
        contentHeight: 400,
        defaultDate: new Date(),
        timeFormat: 'h(:mm)',
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,basicWeek,basicDay,agenda'
        },
        //dayClick: function(){
        //    alert('tıklandı');
        //},
        eventLimit: true,
        eventColor: '#378006',
        events: events,
        eventClick: function (calEvent, jsEvent, view) {
            selectedEvent = calEvent;
            $('#myModal #eventTitle').text(calEvent.title);
            var $description = $('<div/>');
            $description.append($('<p/>').html('<b>Başlama Tarihi:</b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
            if (calEvent.end != null) {
                $description.append($('<p/>').html('<b>Bitiş Tarihi:</b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
            }
            $description.append($('<p/>').html('<b>Randevu Açıklama:</b>' + calEvent.description));
            $description.append($('<p/>').html('<b>Randevu Yeri:</b>' + calEvent.place));
            $description.append($('<p/>').html('<b>Randevu Ek Bilgi:</b>' + calEvent.ekBilgi));
            $description.append($('<p/>').html('<b>Katılımcılar:</b>' + getKisiAd(calEvent.kisiler)));
            $('#myModal #pDetails').empty().html($description);
            $('#myModal').modal();
        },
        selectable: true, //Birden fazla gün seçimi çoklu seçim
        select: function (start, end) {
            selectedEvent = {
                eventID: 0,
                title: '',
                description: '',
                start: start,
                end: end,
                allDay: false,
                color: '',
                kisiler: ''
            };
            $('#multi-select').multiselect('deselectAll', false);
            $('#multi-select').multiselect('updateButtonText');

            openAddEditForm();
            $($('#whichTab').val()).fullCalendar('unselect');
        },
        editable: true,
        eventDrop: function (event) {
            var data = {
                planID: event.eventID,
                planKisaBilgi: event.title,
                planStartTarih: event.start.format('DD/MM/YYYY HH:mm'),
                planEndTarih: event.end != null ? event.end.format('DD/MM/YYYY HH:mm') : null,
                planUzunBilgi: event.description,
                planColor: event.color,
                planFullDay: event.allDay,
                planMekan: event.place,
                planEkBilgi: event.ekBilgi,
                planToKisis: event.kisiler
            };
            SaveEvent(data);
        }
    })
}
$('#btnEdit').click(function () {
    //Open modal dialog for edit event
    openAddEditForm();
})
$('#btnDelete').click(function () {
    if (selectedEvent != null && confirm('Are you sure?')) {
        $.ajax({
            type: "POST",
            url: '/Takvim/DeleteEvent',
            data: { 'eventID': selectedEvent.eventID },
            success: function (data) {
                if (data.status) {
                    //Refresh the calender
                    FetchEventAndRenderCalendar($('#tabKisiID').val());
                    $('#myModal').modal('hide');
                }
            },
            error: function () {
                alert('Failed');
            }
        })
    }
})
$('#btnSave').click(function () {
    //Validation/
    if ($('#txtSubject').val().trim() == "") {
        alert('Subject required');
        return;
    }
    if ($('#txtStart').val().trim() == "") {
        alert('Start date required');
        return;
    }
    if ($('#chkIsFullDay').is(':checked') == false && $('#txtEnd').val().trim() == "") {
        alert('End date required');
        return;
    }
    else {
        var startDate = moment($('#txtStart').val(), "DD.MM.YYYY HH:mm").toDate();
        var endDate = moment($('#txtEnd').val(), "DD.MM.YYYY HH:mm").toDate();
        if (startDate > endDate) {
            alert('Invalid end date');
            return;
        }
    }    
    var ptkisiler = [];
    ptkisiler.push({
        pkID: 0,
        pkKisiID: $('#tabKisiID').val(),
        pkPlanID: $('#hdEventID').val(),
        pkisSource: true
    });
    if ($('#multi-select').val() != null) {
        alert($('#multi-select').val());       
        $.each($('#multi-select').val(), function (i, v) {
            ptkisiler.push({
                pkID: 0,
                pkKisiID: v,
                pkPlanID: $('#hdEventID').val(),
                pkisSource: false
            })
        });        
    }
    var data = {
        planID: $('#hdEventID').val(),
        planKisaBilgi: $('#txtSubject').val().trim(),
        planStartTarih: $('#txtStart').val().trim(),
        planEndTarih: $('#chkIsFullDay').is(':checked') ? null : $('#txtEnd').val().trim(),
        planUzunBilgi: $('#txtDescription').val(),
        planColor: $('#ddThemeColor').val(),
        planFullDay: $('#chkIsFullDay').is(':checked'),
        planMekan: $('#txtMekan').val().trim(),
        planEkBilgi: $('#txtEkBilgi').val(),
        planToKisis: ptkisiler
    }
    SaveEvent(data);
    // call function for submit data to the server 
})
function SaveEvent(data) {
    $.ajax({
        type: "POST",
        url: '/Takvim/SaveEvent',
        data: data,
        success: function (data) {
            if (data.status) {
                //Refresh the calender
                FetchEventAndRenderCalendar($('#tabKisiID').val());
                $('#myModalSave').modal('hide');
            }
        },
        error: function () {
            alert('Failed');
        }
    })
}
function openAddEditForm() {
    
    if (selectedEvent != null) {        
        $('#hdEventID').val(selectedEvent.eventID);
        $('#txtSubject').val(selectedEvent.title);
        $('#txtStart').val(selectedEvent.start.format('DD.MM.YYYY HH:mm'));
        $('#chkIsFullDay').prop("checked", selectedEvent.allDay || false);
        $('#chkIsFullDay').change();
        $('#txtEnd').val(selectedEvent.end != null ? selectedEvent.end.format('DD.MM.YYYY HH:mm') : '');
        $('#txtDescription').val(selectedEvent.description);
        $('#ddThemeColor').val(selectedEvent.color);
        $('#txtMekan').val(selectedEvent.place);
        $('#txtEkBilgi').val(selectedEvent.ekBilgi);
        $('#multi-select').multiselect('select', getKisiID(selectedEvent.kisiler));
        //$('#framework3').multiselect('select', getSahipID(selectedEvent.kisiler));
    }
    $('#myModal').modal('hide');
    $('#myModalSave').modal();
}