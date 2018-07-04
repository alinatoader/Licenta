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
        var question = { Text: text, AssignmentId: assignmentId, Answers: answers };
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

    $('#question-reject-button').click(function () {
        event.preventDefault();
        var comment = $("#Comment").val();
        $.ajax({
            type: 'GET',
            url: $('#question-reject-button').attr('href') + "?" + $.param({ comment: comment }),
            dataType: 'json',
            success: function (response) {
                window.location.href = '/Questions/IncomingQuestions';
            },
            error: function (response) {
                window.location.href = '/Questions/IncomingQuestions';
            }
        });
    });

    $('#question-detail-form').submit(function () {
        event.preventDefault();
        var concepts = $('#Concepts').val();
        var assignmentId = Number(document.getElementById('AssignmentId').value);
        var text = document.getElementById('Text').value;
        var answers = [];
        var questionId = $('#Id').val();
        var comment = $('#Comment').val();
        var status = $('#Status').val();
        var studentId = $('#StudentId').val();
        var dif = $('#Difficulty').val();
        $('.answer-row').each(function (index, item) {
            var correct = $(item).find('input[type="checkbox"]').is(':checked');
            var ansText = $(item).find('input[type="text"]').val();
            var id = $('#Answers_' + index + '__Id').val();
            answers.push({ Correct: correct, Text: ansText, Id: id, QuestionId: questionId });
        });
        var qconcepts = [];
        for (var i = 0; i < concepts.length; i++) {
            qconcepts.push({ QuestionId: questionId, ConceptId: concepts[i] });
        }
        var question = {
            Text: text,
            StudentId: studentId,
            AssignmentId: assignmentId,
            Answers: answers,
            Id: questionId,
            Comment: comment,
            Status: status,
            Difficulty: dif,
            QuestionConcepts: qconcepts
        };
        $.ajax({
            type: 'POST',
            url: $('#question-detail-form').attr('action'),
            data: question,
            dataType: 'json',
            success: function (response) {
                window.location.href = '/Questions/IncomingQuestions';
            },
            error: function (response) {
                window.location.href = '/Questions/IncomingQuestions';
            }
        });
    });
});
