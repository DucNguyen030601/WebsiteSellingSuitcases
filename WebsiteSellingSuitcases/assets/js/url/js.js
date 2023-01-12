
(function ($) {
    /*hello customer*/


/*    modal product*/
    $('li a[data-toggle="modal-ajax"]').on('click', function () {
        var decodeUrl = decodeURIComponent($(this).data('url') + '&&strUrl=' + window.location.href);
        $.get(decodeUrl).done(function (data) {

            $('#PlaceHolderHere').html(data);
            $('#PlaceHolderHere').find('#modalDetails').modal('show')
        });
    });
   /* cart*/
    $('.shopping_cart a').on('click', function () {
        var decodeUrl = decodeURIComponent($(this).data('url') + '?strUrl=' + window.location.href);
        $.get(decodeUrl).done(function (data) {
            
            $('#ShowPartialCart').html(data);
        });
    });
    /*  action nav bar*/
    $(".header_bottom").find('li').attr("class", "")
    var i = $('.breadcrumb_content li').eq(2).attr('title');
    $('#' + i).addClass('active');
    if (!i) { $('#navTrangChu').addClass('active'); }
    /*dang nhap*/
    $('#ThanhPho').on('change', function () {
        $('#Huyen').html(SelectOption($(this).val()));
    });
    function SelectOption(s) {
        var option = '<select name="Huyen" class="nice-select list" tabindex="0">';
        var ND = ["Hải Hậu", "Nam Ninh", "Nghĩa Hưng", "Vụ Bản", "Xuân Thuỷ", "Ý Yên", "Hải Hậu", "Mỹ Lộc", "Xuân Trường"];
        var HN = ["Hoàn Kiếm", "Đống Đa", "Ba Đình", "Hai Bà Trưng", "Hoàng Mai", "Thanh Xuân", "Long Biên", "Nam Từ Liêm", "Bắc Từ Liêm", "Tây Hồ", "Cầu Giấy", "Hà Đông"];
        if (s == "Hà Nội") {
            $.each(HN, function (key, value) {
                option += '<option value="' + value + '">' + value + '</option>';
            });
        }
        if (s == "Nam Định") {
            $.each(ND, function (key, value) {
                option += '<option value="' + value + '">' + value + '</option>';
            });
        }
        return option + '</select>';
    }
    /*  modal Chi tiet hoa don*/
    $('td a[data-target="#modalDetailsBill"]').on('click', function () {
        var decodeUrl = decodeURIComponent($(this).data('url'));
        $.get(decodeUrl).done(function (data) {
            $('#PlaceHolderHere').html(data);
            $('#PlaceHolderHere').find('#modalDetailsBill').modal('show')
        });
    });
    /*  modal Chi tiet khach hang*/
    $('td a[data-target="#modalDetailsCustomer"]').on('click', function () {
       
        var decodeUrl = decodeURIComponent($(this).data('url'));
        $.get(decodeUrl).done(function (data) {
            $('#PlaceHolderHere').html(data);
            $('#PlaceHolderHere').find('#modalDetailsCustomer').modal('show')
        });
    });
    /*  modal Chi tiet nhan vien*/
    $('td a[data-target="#modalDetailsAdmin"]').on('click', function () {
        var decodeUrl = decodeURIComponent($(this).data('url'));
        $.get(decodeUrl).done(function (data) {
            $('#PlaceHolderHere').html(data);
            $('#PlaceHolderHere').find('#modalDetailsAdmin').modal('show')
        });
    });
})(jQuery)
