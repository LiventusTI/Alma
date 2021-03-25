var seconds = 3600;

function countdown() {
    seconds = seconds - 1;
    if (seconds < 0) {
        // Chnage your redirection link here
        if (confirm("Tiempo de sesión ?")) {
            window.setTimeout('countdown()', 3600);  //start the timer again
            location.reload();
        }
        else {
            var url = 'Login';
            window.location.href = url;
        }
        //window.location = "https://duckdev.com";
    } else {
        //alert(seconds);
        // Update remaining seconds
        //document.getElementById("countdown").innerHTML = seconds;
        // Count down using javascript
        window.setTimeout("countdown()", 3600);
    }
}

// Run countdown function
countdown();