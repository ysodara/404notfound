$(document).ready(function () {
    $("#btnShowModal").click(function () {
        $("#addAthleteModal").modal('show');
    });
    
    $("#subAndClose").click(function () {
        $("#addAthleteModal").modal('hide');
    });
});  