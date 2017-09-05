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
            $("#multi-select").multiselect({
                nonSelectedText: 'Toplantıya Katımlımcı Ekleyiniz',
                includeSelectAllOption: true, //Tüm seçim
                selectAllJustVisible: false, //Görünmeyenlerinde tümünün seçimi
                selectAllText: 'Herkesi seç!',
                enableFiltering: true, //Arama aktif etme
                enableCaseInsensitiveFiltering: true
            });
        }
    });
    
});