const setModalData = studentId => {
    console.log(studentId);
    $('#addGrade_userId').val(studentId);
    $('#addAbsence_userId').val(studentId);
}