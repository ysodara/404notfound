$(document).ready(function () {
    $("#btnShowModal").click(function () {
        $("#loginModal").modal('show');
    });
    
    $("#subAndClose").click(function () {
        $("#loginModal").modal('hide');
    });
});  