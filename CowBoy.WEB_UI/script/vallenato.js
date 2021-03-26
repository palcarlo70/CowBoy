/*!
 * Vallenato 1.0
 * A Simple JQuery Accordion
 *
 * Designed by Switchroyale
 * 
 * Use Vallenato for whatever you want, enjoy!
 */

$(document).ready(function () {
    //Add Inactive Class To All Accordion Headers
    $('.accordion-header').toggleClass('inactive-header');
    $('.accordion-header-title').toggleClass('inactive-header');
    $('.accordion-header-foto').toggleClass('inactive-header');
    $('.accordion-header-Ricerche').toggleClass('inactive-header');

    //Set The Accordion Content Width
    var contentwidth = $('.accordion-header').width();
    $('.accordion-content').css({ 'width': contentwidth });
    
    //Set The Accordion Content Width
    var contentwidthTitle = $('.accordion-header-title').width();
    $('.accordion-content-title').css({ 'width': contentwidthTitle });
    
    var contentwidthFoto = $('.accordion-header-foto').width();
    $('.accordion-content-foto').css({ 'width': contentwidthFoto });

    var contentwidthRicerche = $('.accordion-header-Ricerche').width();
    $('.accordion-content-Ricerche').css({ 'width': contentwidthRicerche });


    if ($("#ContentPlaceHolder1_hfAccordionAperto").val() == 'accordion-header-Ricerche') {
        //$('.accordion-header-Ricerche').first().toggleClass('active-header').toggleClass('inactive-header');
        //$('.accordion-content-Ricerche').first().slideDown().toggleClass('open-content');
        $('.accordion-header-Ricerche').toggleClass('active-header').toggleClass('inactive-header');
        $('.accordion-header-Ricerche').next().slideToggle().toggleClass('open-content');
    }

    //Open The First Accordion Section When Page Loads
    //$('.accordion-header-title').first().toggleClass('active-header').toggleClass('inactive-header');
    //$('.accordion-content-title').first().slideDown().toggleClass('open-content');

    // The Accordion Effect
    $('.accordion-header').click(function () {
        //codice aggiunto per gestire l'apertura degli accordion
        var cc = document.getElementById('ContentPlaceHolder1_hfIdPartoSalto').value;
        if (cc == "")
        {
            alert('Selezionare una parto o crearne uno nuovo');
        }
        else if ($(this).is('.inactive-header')) {
            $('.active-header').toggleClass('active-header').toggleClass('inactive-header').next().slideToggle().toggleClass('open-content');
            $(this).toggleClass('active-header').toggleClass('inactive-header');
            $(this).next().slideToggle().toggleClass('open-content');
            //alert($(this).toggleClass('active-header').toggleClass('inactive-header')[0].innerHTML.indexOf('Parto'));
           // alert('true');
        }
        else {
            $(this).toggleClass('active-header').toggleClass('inactive-header');
            $(this).next().slideToggle().toggleClass('open-content');
            // alert($(this).toggleClass('active-header').toggleClass('inactive-header')[0].innerHTML.indexOf('Parto'));
           // alert('false');
        }
    });
    
    // The Accordion Effect
    $('.accordion-header-title').click(function () {
        //codice aggiunto per gestire l'apertura degli accordion
       if ($(this).is('.inactive-header')) {
            $('.active-header').toggleClass('active-header').toggleClass('inactive-header').next().slideToggle().toggleClass('open-content');
            $(this).toggleClass('active-header').toggleClass('inactive-header');
            $(this).next().slideToggle().toggleClass('open-content');
       }
        else {
            $(this).toggleClass('active-header').toggleClass('inactive-header');
            $(this).next().slideToggle().toggleClass('open-content');
        }
    });
    
    
    // The Accordion Effect
    $('.accordion-header-foto').click(function () {
        //codice aggiunto per gestire l'apertura degli accordion
        if ($(this).is('.inactive-header')) {
            $('.active-header').toggleClass('active-header').toggleClass('inactive-header').next().slideToggle().toggleClass('open-content');
            $(this).toggleClass('active-header').toggleClass('inactive-header');
            $(this).next().slideToggle().toggleClass('open-content');
        }
        else {
            $(this).toggleClass('active-header').toggleClass('inactive-header');
            $(this).next().slideToggle().toggleClass('open-content');
        }
    });

    $('.accordion-header-Ricerche').click(function () {
        //codice aggiunto per gestire l'apertura degli accordion
        if ($(this).is('.inactive-header')) {
            $('.active-header').toggleClass('active-header').toggleClass('inactive-header').next().slideToggle().toggleClass('open-content');
            $(this).toggleClass('active-header').toggleClass('inactive-header');
            $(this).next().slideToggle().toggleClass('open-content');
        }
        else {
            $(this).toggleClass('active-header').toggleClass('inactive-header');
            $(this).next().slideToggle().toggleClass('open-content');
        }
        $("#MainContent_hfAccordionAperto").val('accordion-header-Ricerche');
    });


    return false;
});