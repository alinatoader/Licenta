$(document).ready(function () {
    var menuPoints = $('#tmNavbar li');
    menuPoints.removeClass('active');
    $(menuPoints[2]).addClass('active');

    $('#add-section-button').click(function () {
        var concepts = $('#concept-selector')[0].options;
        var difficulties = $('#difficulty-selector')[0].options;
        var control = ' <div class="form-group ">' +
            '<input type= "number" min= "1" class="form-control" style= "display:inline-block; width:10%; margin-right: 5px;" value= "1"/>' +
            '<select class="form-control" style="display:inline-block; width:25%; margin-right: 5px;">';
        for (var i = 0; i < concepts.length; i++) {
            control += '<option value="' + concepts[i].value + '">' + concepts[i].label + '</label>';
        }
        control += '</select><select class="form-control" style="display:inline-block; width:15%;">';
        for (var i = 0; i < difficulties.length; i++) {
            control += '<option value="' + difficulties[i].value + '">' + difficulties[i].label + '</label>';
        }
        control += '</select></div>';
        $('#sections-container').append(control);
    });

    $('#generate-test-button').click(function () {
        event.preventDefault();
        var sections = [];
        $('#sections-container .form-group').each(function (index, item) {
            var noQuestions = $(item).find('input').val();
            var conceptId = $(item).find('select')[0].value;
            var difficulty = $(item).find('select')[1].value;
            sections.push({ ConceptId: conceptId, Difficulty: difficulty, NumberOfQuestions: noQuestions });
        });
        var test = { Sections: sections };
        $.ajax({
            type: 'POST',
            url: '/Tests/GenerateTest',
            data: test,
            success: function (response) {
                if (response === 'prof') {
                    window.location.href = '/Tests/Edit';
                }
                else {
                    window.location.href = '/Tests/TakeTest';
                }
            },
            error: function (response) {
                console.log('error');
            }
        });
    });

    $('#save-test-button').click(function () {
        event.preventDefault();
        var name = $('#Name').val();
        $.ajax({
            type: 'POST',
            url: '/Tests/SaveTest',
            data: { Name: name },
            success: function (response) {
                window.location.href = "/Tests/Index";
            },
            error: function (response) {
                console.log('error');
            }
        });
    });

    $('#finish-test-button').click(function () {
        event.preventDefault();
        var questions = [];
        $('.question-container').each(function (index, item) {
            var questionId = $(item).find('input[type="number"]')[0].value;
            var answersIds = [];
            $(item).find('li').each(function (i, box) {
                var answer = $(box).find('input[type="checkbox"]');
                if ($(answer[0]).prop('checked') == true)
                    answersIds.push($(box).find('input[type="number"]')[0].value);

            });
            questions.push({ QuestionId: questionId, AnswerIds: answersIds });
        });
        var test = {
            Name: $('#Name').val(), Questions: questions
        };
        $.ajax({
            type: 'POST',
            url: '/Tests/TakeTest',
            data: test,
            success: function (response) {
                $('.corectness-of-answers').show();
                $('#finish-test-button').hide();
                $('#back-to-tests-button').show();
                $('.modal-body h3').html(parseFloat(response).toFixed(2) + " %");
                $('.modal').modal('show');
            },
            error: function (response) {
                console.log('error');
            }
        });
    });
});