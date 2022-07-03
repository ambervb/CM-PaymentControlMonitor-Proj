function checkCommentFieldValue() {
    console.log("Banaan");
    var nameValue = $('#hackermanCommentField').val().toString();
    console.log(nameValue);
    if (nameValue.includes("\<script")) {
//        $('#hackermanCommentField').css("background-image", "url('~/Easter eggs/Images/Hackerman.jpg')");
        $('body').css('background-color', 'black');
        $('body').css('background-image', 'url("/Easter eggs/Images/Hackerman.jpg")');
        $('body').css('background-size', 'contain');
        $('body').css('background-repeat', 'no-repeat');
        $('body').css('background-position', 'center');
        $('body').css('background-size', 'contain');

        $('.elevated-card').remove();

        alert("I'm in.");
    }
}