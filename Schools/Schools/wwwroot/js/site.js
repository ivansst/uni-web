const setModalData = studentId => {
    console.log(studentId);
    $('#addGrade_userId').val(studentId);
    $('#addAbsence_userId').val(studentId);
}

class SchoolScheduleModel {
    constructor(Order, SubjectId, Day) {
        this.Order = Order;
        this.SubjectId = SubjectId;
        this.Day = Day;
    }
}

$('#submitSchedule, #submitEditSchedule').click((e) => {
    let table, url;

    if (e.target.id == "submitSchedule") {
        table = "schedule_table";
        url = "/Schedule/Create/";
    }
    else if (e.target.id == "submitEditSchedule") {
        table = "schedule_edit_table";
        url = "/Schedule/Edit/";
    }


    let rows = document.querySelectorAll(`#${table} tbody tr`);
    let model = [];

    for (let i = 0; i < rows.length; i++) {
        rows[i].children

        model.push(new SchoolScheduleModel(i + 1, parseInt(rows[i].children[0].children[0].value), 1));
        model.push(new SchoolScheduleModel(i + 1, parseInt(rows[i].children[1].children[0].value), 2));
        model.push(new SchoolScheduleModel(i + 1, parseInt(rows[i].children[2].children[0].value), 3));
        model.push(new SchoolScheduleModel(i + 1, parseInt(rows[i].children[3].children[0].value), 4));
        model.push(new SchoolScheduleModel(i + 1, parseInt(rows[i].children[4].children[0].value), 5));
    }

    console.log(model);

    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            window.location = "/Schedule/Index";
        },  
        error: function (err) {
            console.log(err);
        }
    });
})