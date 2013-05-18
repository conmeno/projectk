$(window).scroll(function () {
    var header = $('.header');
    if ($(window).scrollTop() >= 38 & !header.hasClass('small')) {
        header.addClass('small');
        $('#steps-home').css("top", '65px');
    }
    if ($(window).scrollTop() < 38 & header.hasClass('small')) {
        header.removeClass('small');
        $('#steps-home').css("top", '100px');
    }

   
   
});
$(document).ready(function () {
    $('.panel,#steps-home').hover(function () {
        var offset = $('.panel').position();
        $('#steps-home').show();
        //$('#steps-home').css("top",(offset.top + $('.panel').height()) + 'px');
       
    }, function () {
        $('#steps-home').hide();
    });
});