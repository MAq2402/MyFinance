{
    var lis = $('.month').removeClass('active');
    var url = location.href;
    var month = url.split('=');
    var newActive = $('#li' + month[1]);
    newActive.addClass('active');
}