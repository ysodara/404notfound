//<input name="firstName" id="firstName" type="text" class="feedback-input" placeholder="First Name" />
//    <input name="lastName" id="lastName" type="text" class="feedback-input" placeholder="Last Name" />
//    <input name="email" id="email" type="text" class="feedback-input" placeholder="Email" />
//    <input name="subject" id="subject" type="text" class="feedback-input" placeholder="subject" />
//    <textarea name="text" class="feedback-input" placeholder="Comment"></textarea>
function SendEmail() {
    var name = document.getElementById("Name").value;
    var email = document.getElementById("email").value;
    var subject = document.getElementById("subject").value;
    var message = document.getElementById("message").value;

    $.ajax({
        type: 'Post',
        url: "/Home/SendEmail?name=" + name + '&cutomerEmail=' + email + '&subject=' + subject + '&message=' + message,
        success: show,
        error: errorOnAjax
    }); 
}
function show(data) { console.log('Succeed ajax');}
function errorOnAjax() {
    console.log('Error on AJAX return');
}