$(document).ready(function () {
    var menuPoints = $('#tmNavbar li');
    menuPoints.removeClass('active');
    $(menuPoints[2]).addClass('active');
});