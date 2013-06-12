$(window).scroll(function () {
    var header = $('.header');
    if ($(window).scrollTop() >= 38 & !header.hasClass('small')) {
        header.addClass('small');
        $('#mobileMenu').css("margin-top", '-34px');
        //$('.currentUsername').css("margin-top", '-15px');
    }
    if ($(window).scrollTop() < 38 & header.hasClass('small')) {
        header.removeClass('small');
        $('#mobileMenu').css("margin-top", '0px');
        //$('.currentUsername').css("margin-top", '15px');
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