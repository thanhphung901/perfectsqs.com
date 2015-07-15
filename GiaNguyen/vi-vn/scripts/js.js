$(function () {
    var pull = $('#pull');
    btnClose = $('#btnClose');
    menu = $('.navx > ul');
    navx = $('.navx');
    menuli = $('.navx > ul > li');
    menuli_ul = $('.navx > ul > li>ul');

    menuHeight = menu.height();

    $(pull).on('click', function (e) {
        e.preventDefault();
        menu.slideToggle();
    });
    $(btnClose).on('click', function (e) {
        e.preventDefault();
        menu.slideToggle();
    });
    var w1 = $(window).width();
    if (w1 < 768) {
        navx.addClass("navxMb");
        menuli_ul.addClass('dropdown');
        menuli.on('click', function () {

            if ($(this).find('ul.dropdown').is(':hidden')) {
                $(this).find('ul.dropdown').slideToggle();
                $(this).find('ul.dropdown').addClass('open');
            }
            else {
                $(this).find('ul.dropdown').slideToggle();
                return;
            }

        });
    }

    $(window).resize(function () {
        var w = $(window).width();
        if (w < 768) {
        }

    });
	$( ".navx li" ).has( "ul" ).addClass("parent");
});

//$(function () {
//	$( ".navy > ul > li" ).has( "ul" ).addClass("parent");
//	$( ".navy >ul > li> a" ).append("<span class='caretL'></span>");
//});

$(function () {
$( ".content-media> ul >li" ).prepend("<i class='fa fa-arrow-circle-right'></i>");
$( ".content-media ul ul> li" ).prepend("<i class='fa fa-arrow-circle-o-right'></i>");
});