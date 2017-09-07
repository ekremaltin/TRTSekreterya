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
      
    var tabSelect = $("#tabKeySelect");
    tabSelect.empty();
    $('#add_tab').click(function () {
        $.ajax({
            type: 'POST',
            url: '/Kisi/getBirimKisiler',
            data: { 'id': $('#birimIDforTab').val() },
            dataType: 'json',
            success: function (data) {
                $.each(data.liste, function (index, option) {
                    tabSelect.append('<option value=' + option.Value + '>' + option.Text + '</option>')
                });
                tabSelect.multiselect({
                    nonSelectedText: 'Yeni Takvim Kişisi Seçiniz',
                    includeSelectAllOption: true, //Tüm seçim
                    selectAllJustVisible: false, //Görünmeyenlerinde tümünün seçimi
                    selectAllText: 'Herkesi seç!',
                    enableFiltering: true, //Arama aktif etme
                    enableCaseInsensitiveFiltering: true
                });
                tabSelect.multiselect('select', getSelectedID(data.trueList))
            }
        });

    });
    function getSelectedID(kisiIDList) {
        var selectedList = [];
        for (var i = 0; i < kisiIDList.length; i++) {
            selectedList.push(kisiIDList[i]);
        }
        return selectedList;
    }
});
