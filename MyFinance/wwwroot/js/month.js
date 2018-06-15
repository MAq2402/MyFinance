{
    var lis = $('.month').removeClass('active');
    var url = location.href;
    var splitedUrl = url.split('=');
    var month = splitedUrl[2];
    if (month == null) {
        var today = new Date();
        month = today.getMonth()+1
    }
    var newActive = $('#li' + month);
    newActive.addClass('active');
}