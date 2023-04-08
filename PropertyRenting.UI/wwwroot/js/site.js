$(document).ready(function () {
    $('.sidebar-nav nav .navbar-nav li a').removeClass('active');
    var url = window.location;
    $('.sidebar-nav nav .navbar-nav li a').filter(function () {
        return this.href == url;
    }).addClass('active');
});
