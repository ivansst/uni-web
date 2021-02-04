const setModalData = studentId => {
    console.log(studentId);
    $('#addGrade_userId').val(studentId);
    $('#addAbsence_userId').val(studentId);
}

class TestModel {
    constructor(Order, SubjectId, Day) {
        this.Order = Order;
        this.SubjectId = SubjectId;
        this.Day = Day;
    }
}

$('#submitSchedule').click(() => {

    let rows = document.querySelectorAll('table tbody tr');
    let model = [];

    for (let i = 0; i < rows.length; i++) {
        rows[i].children

        model.push(new TestModel(i + 1, parseInt(rows[i].children[0].children[0].value), 1));
        model.push(new TestModel(i + 1, parseInt(rows[i].children[1].children[0].value), 2));
        model.push(new TestModel(i + 1, parseInt(rows[i].children[2].children[0].value), 3));
        model.push(new TestModel(i + 1, parseInt(rows[i].children[3].children[0].value), 4));
        model.push(new TestModel(i + 1, parseInt(rows[i].children[4].children[0].value), 5));
    }

/*    model.Monday.push({
        Order: i + 1,
        SubjectId: parseInt(rows[i].children[0].children[0].value),
        Day: 1
    });
    model.Tuesday.push({
        Order: i + 1,
        SubjectId: parseInt(rows[i].children[1].children[0].value),
        Day: 2
    })
    model.Wednesday.push({
        Order: i + 1,
        SubjectId: parseInt(rows[i].children[2].children[0].value),
        Day: 3
    })
    model.Thursday.push({
        Order: i + 1,
        SubjectId: parseInt(rows[i].children[3].children[0].value),
        Day: 4
    })
    model.Friday.push({
        Order: i + 1,
        SubjectId: parseInt(rows[i].children[4].children[0].value),
        Day: 5
    })*/

    console.log(model);

/*    $.ajax({
        type: 'POST',
        url: `User/Register/`,
        data: model,
        error: function (err) {
            console.log(err);
        }
    });*/
})