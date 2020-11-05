/**
 * This part will get the access token and userid for storing in the database. It will log both in the console area.
 * */
// get the url
var url = window.location.href;
//getting the access token from url
var access_token = url.split("#")[1].split("=")[1].split("&")[0];
// get the userid
var userId = url.split("#")[1].split("=")[2].split("&")[0];
console.log(access_token);
console.log(userId);

/**Setting Up Variables
 * */
var stepsToday = 0;
var milesToday = 0;
var caloriesToday = 0;
var floorsToday = 0;
var sedindaryMinToday = 0;

/**
 * This part will retrieve my FitBit data. It will log it in the console area.
 * */
var xhr = new XMLHttpRequest();
xhr.open('GET', 'https://api.fitbit.com/1/user/' + userId + '/activities/date/today.json');
xhr.setRequestHeader("Authorization", 'Bearer ' + access_token);
xhr.onload = function () {
    if (xhr.status === 200) {
        console.log(xhr.responseText);
        var activities = xhr.responseText;
        var json = JSON.parse(activities);
        stepsToday = json["summary"]["steps"];
        milesToday = json["summary"]["distances"][0]["distance"];
        caloriesToday = json["summary"]["caloriesOut"];
        floorsToday = json["summary"]["floors"];
        sedindaryMinToday = json["summary"]["sedentaryMinutes"];
        sendData();
    }
};
xhr.send();

function sendData() {
    $.ajax({
        url: '/Athlete/Home/FitBit',
        type: 'POST',
        data: { 'userID': userId, 'token': access_token },
        //dataType: 'json',
        //contentType: 'application/json; charset=utf-8',
        success: function () {
            successFitBit()
        },
        error: function (error) {
            alert('error');
        }
    });
}

function successFitBit() {
    //delete fitbit image and button
    var fitBitImage = document.getElementById("fitbitImage");
    fitBitImage.parentNode.removeChild(fitBitImage);

    //Help and Hints visibility
    var help = document.getElementById("helpandhints");
    help.style.visibility = 'visible';

    //Algorithm data pull
    var height = document.getElementById("height").value;
    var sex = document.getElementById("sex").value;
    var weight = document.getElementById("weight").value;
    var age = document.getElementById("age").value;
    var averageCals = 0;

    //IF DATA EXISTS, USE ALGO
    if (height != 0 && sex != null && weight != 0 && age != 0) {
        //Convert age to date and then to age
        height = parseInt(height);
        weight = parseInt(weight);
        age = new Date(age);
        age = _calculateAge(age);

        //Algorithm usage
        if (sex == "F") {
            //female algo
            averageCals = 655.1 + (4.35 * weight) + (4.7 * height) - (4.7 * age);
        }
        if (sex == "M") {
            //male algo
            averageCals = 66 + (6.2 * weight) + (12.7 * height) - (6.76 * age);
        }

        //Per Hour Converstions
        var avgCalsPerHour = Math.round(averageCals / 24);
        var hoursPassed = new Date(Date.now());
        hoursPassed = hoursPassed.getHours();

        //FitBit Comparisons
        var calsPerHour = Math.round(caloriesToday / hoursPassed);

        ///////OUTPUT FOR EACH EVENT////////
        if (avgCalsPerHour > calsPerHour) {
            //OUTPUT IF BELOW AVERAGE (Make Red Background)
            var table = document.getElementById("myTable");
            table.style.visibility = 'visible';
            $('#myTable').append($('<tr><th rowspan="5" style="color: white;font-size: xx-large;width: 180px; height: 155px; border: none;" id="fitbit_steps">' + stepsToday + '</th></tr><tr><th>Miles</th><td>' + milesToday + '</td></tr><tr><th>Calories</th><td>' + caloriesToday + '</td></tr><tr><th>Floors</th><td>' + floorsToday + '</td></tr><th>Sedintary Mins</th><td>' + sedindaryMinToday + '</td></tr><th colspan="3" id="fitbit_cals_below" style="color: white;font-size: xxx-large;width: 210px; height: 200px; padding-right: 80px; padding-bottom: 15px;">' + calsPerHour + '</th>'));
        }
        else if (avgCalsPerHour <= calsPerHour) {
            //OUTPUT IF ABOVE AVERAGE (Make Green Background)
            var table = document.getElementById("myTable");
            table.style.visibility = 'visible';
            $('#myTable').append($('<tr><th rowspan="5" style="color: white;font-size: xx-large;width: 180px; height: 155px; border: none;" id="fitbit_steps">' + stepsToday + '</th></tr><tr><th>Miles</th><td>' + milesToday + '</td></tr><tr><th>Calories</th><td>' + caloriesToday + '</td></tr><tr><th>Floors</th><td>' + floorsToday + '</td></tr><th>Sedintary Mins</th><td>' + sedindaryMinToday + '</td></tr><th colspan="3" id="fitbit_cals_above" style="color: white;font-size: xxx-large;width: 210px; height: 200px; padding-right: 50px;">' + calsPerHour + '</th>'));
        }
    }
    else {
        //OUTPUT IF NO DATA IS GIVEN
        var table = document.getElementById("myTable");
        table.style.visibility = 'visible';
        $('#myTable').append($('<tr><th rowspan="5" style="color: white;font-size: xx-large;width: 180px; height: 155px; border: none;" id="fitbit_steps">' + stepsToday + '</th></tr><tr><th>Miles</th><td>' + milesToday + '</td></tr><tr><th>Calories</th><td>' + caloriesToday + '</td></tr><tr><th>Floors</th><td>' + floorsToday + '</td></tr><th>Sedintary Mins</th><td>' + sedindaryMinToday + '</td></tr>'));
    }
}

function _calculateAge(birthday) { // birthday is a date
    var ageDifMs = Date.now() - birthday.getTime();
    var ageDate = new Date(ageDifMs); // miliseconds from epoch
    return Math.abs(ageDate.getUTCFullYear() - 1970);
}

function myFunction1() {
    var x = document.getElementById("recordFormAdding");
    x.style.display = "block";
}