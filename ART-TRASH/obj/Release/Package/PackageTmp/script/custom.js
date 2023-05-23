// document load
$(document).ready(function(){
    setWinSizes();
    leftMenuHover();
    subMenuHover();
    subMenuClick();
    $('.hamburger-menu').click(function (e) { 
        $('.main-container').toggleClass('collapsed');
    });

    // dataTable
    $('#dataTable').DataTable( {
        select: true
    } );

    // Dropdown Hover Function
    // $('.dropdown .dropdown-toggle').mouseover(function(){ 
    //     $(this).dropdown("toggle");
    // });
    // $('.dropdown .dropdown-toggle, .dropdown-menu').mouseleave(function(){ 
    //     $(this).dropdown("toggle");
    // });

});
// window resize
// $(window).resize(function(){
//     setWinSizes();
//     leftMenuHover();
//     subMenuHover();
//     subMenuClick();
// });


/*  Set Window Height and Width - Start */
function setWinSizes() {
    var winHeight = $(window).height();
    var winWidth = $(window).width();
    var headHeight = $('.header-top').innerHeight();
    var containerHeight = winHeight - headHeight - 20;
    if(winWidth >= 992){
        $('.inner-container').css({"height": containerHeight, "padding-top": "10px"});
    }else{
        $('.inner-container').css({"height": "auto", "padding-top": headHeight});
    }
}

function showToast(){
    var toastElList = [].slice.call(document.querySelectorAll('.toast'))
    var toastList = toastElList.map(function(toastEl) {
    // Creates an array of toasts (it only initializes them)
        return new bootstrap.Toast(toastEl) // No need for options; use the default options
    });
    toastList.forEach(toast => toast.show()); // This show them
}

//Left Menu Hover
function leftMenuHover(){
    var winWidth = $(window).width();
    $('.left-menu').mouseover(function () {
        if(winWidth >= 992){
            $('.main-container').removeClass('collapsed');
        }
    });
    $('.left-menu').mouseleave(function () {
        if(winWidth >= 992){
            $('.main-container').addClass('collapsed');
        }
    });
}

// Sub Menu Hover
function subMenuHover(){
    var winWidth = $(window).width();
    $('.nav-item.have-submenu').mouseover(function(){
        if(winWidth >= 992){
            $(this).addClass('active');
        }
    });
    $('.nav-item.have-submenu').mouseleave(function(){
        if(winWidth >= 992){
            $(this).removeClass('active');
        }
    });
}

// SubMenu Click
function subMenuClick(){
    $('.nav-item.have-submenu > .nav-link').click(function(){
        var winWidth = $(window).width();
        if(winWidth < 992){
            if($(this).parent('.have-submenu').hasClass('active')){
                $(this).parent('.have-submenu').removeClass('active');
            }else{
                $('.have-submenu').removeClass('active')
                $(this).parent('.have-submenu').addClass('active');
            }
        }
    });
}