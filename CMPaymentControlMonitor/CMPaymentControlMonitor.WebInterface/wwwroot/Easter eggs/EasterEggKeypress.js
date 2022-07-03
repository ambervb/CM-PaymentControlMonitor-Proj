// easter egg uses animate.css
var hornbachSecret = "7279827866656772"; //hornbach

var konamiSecret = "38384040373937396665"; // Konami
var leftMouseClick = "1";
var isKonamiCodeActivated = false;
var loginCard = document.getElementById("loginCard");
var amountOfLives = 3;

var input = "";
var timer;

//The following function sets a timer that checks for user input. You can change the variation in how long the user has to input by changing the number in ‘setTimeout.’ In this case, it’s set for 500 milliseconds or ½ second.
$(document).keyup(function (e) {
    input += e.which;
    timer = setTimeout(function () { input = ""; }, 3000);
    check_input();
});

$(document).mousedown(function(e) {
    input += e.which;
    timer = setTimeout(function () { input = ""; }, 1500);
    check_input();
});

//Once the time is up, this function is run to see if the user’s input is the same as the secret code
function check_input() {
    if (input === hornbachSecret) {
        // the code used to reveal the hornbach jpg
        console.log("Kama na ya yippe yippe yay!");
        $('.nav-logo').fadeOut("2000", function () {
            $(this).attr("src", "/Easter eggs/Images/hornbach.jpg");
            $(this).fadeIn("2000");
        });

        $('.navbar-text').fadeOut("2000", function () {
            $(this).html("Es gibt immer was zu tun");
            $(this).fadeIn("2000");
        });
        //$('#audio')[0].play();
    } else if (input === konamiSecret) {
        isKonamiCodeActivated = true;
        startMiniGame();
    } else if ((isKonamiCodeActivated && input === leftMouseClick)) {
        if (amountOfLives === 0) {
            isKonamiCodeActivated = false;
            return;
        }

        // Change link passive to link attack.
        $("#link-figure").removeClass("link-passive");
        $("#link-figure").addClass("link-attack");

        // Wobble the login card.
        loginCard.classList.add("animated");
        loginCard.classList.add("wobble");

        setTimeout(function () {
            // Delete wobble
            loginCard.classList.remove("animated");
            loginCard.classList.remove("wobble");

            // Set link to passive again.
            $("#link-figure").removeClass("link-attack");
            $("#link-figure").addClass("link-passive");
        }, 1000);

        // Grayscale 1 heart.
        if (amountOfLives === 3) { // Grayscale first heart.
            loginCard.childNodes[3].childNodes[3].classList.add("grayscale");
        } else if (amountOfLives === 2) { // Grayscale second heart.
            loginCard.childNodes[3].childNodes[5].classList.add("grayscale");
        } else if (amountOfLives === 1) {
            // Grayscale third heart.
            loginCard.childNodes[3].childNodes[7].classList.add("grayscale");

            // Set timeout to control the hinge animation and the game over link figure.
            setTimeout(function () {
                loginCard.classList.add("animated");
                loginCard.classList.add("hinge");

                $("#link-figure").removeClass("link-passive");
                $("#link-figure").addClass("link-game-over");
            }, 1001);

            // Set timeout to control the fade in for game over image.
            setTimeout(function () {
                $("#game-over").fadeIn(1000);
            }, 2000);
        }

        // decrease amount of lives.
        amountOfLives--;
    }
}

function startMiniGame() {
    // Add the link figure to the login page.
    $('#link-figure').addClass("link-passive");

    // ----- {Start to add hearts.} -----
    setTimeout(function() {
        loginCard.childNodes[3].childNodes[3].classList.add("heart-1");
    }, 300);

    setTimeout(function(){
        loginCard.childNodes[3].childNodes[5].classList.add("heart-2");
    }, 500);

    setTimeout(function() {
        loginCard.childNodes[3].childNodes[7].classList.add("heart-3");
    }, 700);
    // ----- {Stop adding hearts.} -----
}