{
    var lis = $('.month').removeClass('active');
    var url = location.href;
    var splitedUrl = url.split('=');
    var month = splitedUrl[1];
    var newActive = $('#li' + month);
    newActive.addClass('active');
}