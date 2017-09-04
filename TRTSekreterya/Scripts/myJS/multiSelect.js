$(document).ready(function () {
    var kisiSelect = $("#multi-select");
    kisiSelect.empty();
    $.ajax({
        type: 'GET',
        url: '/Kisi/getKisiler',
        dataType: 'json',
        success: function (data) {
            $.each(data, function (index, option) {
                kisiSelect.append('<option value=' + option.Value + '>' + option.Text + '</option>')
            });
            kisiSelect.multiselect();
        }
    });
    
});