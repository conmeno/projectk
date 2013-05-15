$(window).scroll(function () {
    var header = $('header');
    if ($(window).scrollTop() >= 38 & !header.hasClass('small')) {
        header.addClass('small');
    }
    if ($(window).scrollTop() < 38 & header.hasClass('small')) {
        header.removeClass('small');
    }

   
});