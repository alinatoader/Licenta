$(document).ready(function () {
    var menuPoints = $('#tmNavbar li');
    menuPoints.removeClass('active');
    $(menuPoints[1]).addClass('active');

    var questions = $('.question-box');
    questions.each(function (index, item) {
        if (index % 4 === 0) {
            var code ='';
            if (index !== 0)
                code = '</div>';
            code += '<div class="row">';
            item.insertAdjacentHTML('beforebegin', code);
        }
        if (questions.length - 1 === index) {
            item.insertAdjacentHTML('afterend', '</div>');
        }
    });
});
