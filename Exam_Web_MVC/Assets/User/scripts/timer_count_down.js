function timer_count_down(minutes) {
    var secondLeft = minutes * 60;
    var h, m, s;
    var timer = function () {
        if (secondLeft < 0) {
            clearInterval(Interval);
            document.getElementsByClassName("hours")[0].innerHTML = "";
            document.getElementsByClassName("minutes")[0].innerHTML = "";
            document.getElementsByClassName("seconds")[0].innerHTML = "Hết giờ";
            document.getElementsByClassName("hours")[1].innerHTML = "";
            document.getElementsByClassName("minutes")[1].innerHTML = "";
            document.getElementsByClassName("seconds")[1].innerHTML = "Hết giờ";
            alert("Bạn đã hết thời gian làm bài");
            document.getElementById("submit_btn").click();

        } else {
            h = parseInt(secondLeft / 3600);
            m = parseInt(secondLeft / 60) - 60 * h;
            s = secondLeft - h * 3600 - m * 60;
            if (h < 10) {
                document.getElementsByClassName("hours")[0].innerHTML = "0" + h + ":";
                document.getElementsByClassName("hours")[1].innerHTML = "0" + h + ":";
            } else {
                document.getElementsByClassName("hours")[0].innerHTML = h + ":";
                document.getElementsByClassName("hours")[1].innerHTML = h + ":";
            }
            if (m < 10) {
                document.getElementsByClassName("minutes")[0].innerHTML = "0" + m + ":";
                document.getElementsByClassName("minutes")[1].innerHTML = "0" + m + ":";
            } else {
                document.getElementsByClassName("minutes")[0].innerHTML = m + ":";
                document.getElementsByClassName("minutes")[1].innerHTML = m + ":";
            }
            if (s<10) {
                document.getElementsByClassName("seconds")[0].innerHTML = "0" + s;
                document.getElementsByClassName("seconds")[1].innerHTML = "0" + s;

            } else {
                document.getElementsByClassName("seconds")[0].innerHTML = s;
                document.getElementsByClassName("seconds")[1].innerHTML = s;
            }

            if (secondLeft < 90) {
                document.getElementsByClassName("clock")[0].classList.add("btn-danger")
                document.getElementsByClassName("clock")[0].classList.remove("btn-secondary")
                document.getElementsByClassName("clock")[1].classList.add("btn-danger")
                document.getElementsByClassName("clock")[1].classList.remove("btn-secondary")
            }
        }
        secondLeft--;
    }
    var Interval = setInterval(timer, 1000);
    
}