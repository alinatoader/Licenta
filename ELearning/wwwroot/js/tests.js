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
                window.location.href = '/Tests/Edit';
            },
            error: function (response) {
                console.log('error');
            }
        });
    });
});