$(document).ready(function () {
    var menuPoints = $('#tmNavbar li');
    menuPoints.removeClass('active');
    $(menuPoints[0]).addClass('active');

    var questions = $('.question-box');
    questions.each(function (index, item) {
        if (index % 4 === 0) {
            var code = '';
            if (index !== 0)
                code = '</div>';
            code += '<div class="row">';
            item.insertAdjacentHTML('beforebegin', code);
        }
        if (questions.length - 1 === index) {
            item.insertAdjacentHTML('afterend', '</div>');
        }
    });

    $('#add-answer-button').click(function () {
        $('#answers-container').append('<div class="form-group row answer-row">' +
            '<input type= "checkbox" />' +
            '<input type="text" class="form-control" placeholder="Raspuns" style="margin-left: 5px;" />' +
            '<span asp-validation-for="Text" class="text-danger"></span>' +
            '</div >');
    });

    $('.create-question-form').submit(function () {
        event.preventDefault();
        var assignmentId = Number(document.getElementById('Assignment').value);
        var text = document.getElementById('Text').value;
        var answers = [];
        $('#answers-container .answer-row').each(function (index, item) {
            var correct = $(item).find('input[type="checkbox"]').is(':checked');
            var ansText = $(item).find('input[type="text"]').val();
            answers.push({ Correct: correct, Text: ansText });
        });
        var question = { Text: text, StudentId: 1, AssignmentId: 1, Answers: answers };
        $.ajax({
            type: 'POST',
            url: $('.create-question-form:visible').attr('action'),
            data: question,
            dataType: 'json',
            success: function (response) {
                $('.text-info').html(response.responseText);
            },
            error: function (response) {
                $('.text-info').html(response.responseText);
            }
        });
    });

    $('#back-target').attr('href', localStorage.getItem('previous'));

    $('.summary-button').click(function () {
        $(this).next('.modal').modal('show');
    });
});
