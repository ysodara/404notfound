$(document).ready(function () {
    
    $("#registerModal").modal('show');

    $("#registerBtn").click(function () {
        $("#registerModal").modal('show');
    });
    //registerBtn

    $("#subAndClose").click(function () {
        $("#registerModal").modal('hide');
    });
}); 


function myFunction2() {
    var x = document.getElementById("myDIV2");

    var z = document.getElementById("myDIV3");
    x.style.display = "block";

    z.style.display = "none";
}

function myFunction3() {

    var y = document.getElementById("myDIV2");
    var z = document.getElementById("myDIV3");
    z.style.display = "block";
    y.style.display = "none";

}