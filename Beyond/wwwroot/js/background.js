var resizeTimer;
window.addEventListener("resize", () => {
    document.body.classList.add("resize-animation-stopper");
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(() => {
        document.body.classList.remove("resize-animation-stopper");
    }, 400);
});

document.addEventListener("DOMContentLoaded", () => document.body.classList.remove("preload"))

var home = document.getElementById('home');
var destinations = document.getElementById('destinations');
var crew = document.getElementById('crew');
var bod = document.getElementById("bod-bg");
var register

window.addEventListener('load', (e) => {
    bod.classList.add('loaded');
});

var cls = ["home", "crew", "destinations"];
var loc = window.location.pathname;
if (loc === "/Crew") {
    bod.classList.remove(...cls);
    bod.classList.add("crew");
}else if (loc === "/Home" || loc==="/") {
    bod.classList.remove(...cls);
    bod.classList.add("home");
}else if (loc === "/Destinations") {
    bod.classList.remove(...cls);
    bod.classList.add("destinations");
}else if (loc === "/Identity/Account/Register" || loc === "/Identity/Account/Login") {
    bod.classList.remove(...cls);
    bod.classList.add("reglog");
} else if (loc === "/Tickets/All") {
    bod.classList.remove(...cls);
    bod.classList.add("crew");
} else {
    bod.classList.remove(...cls);
    bod.classList.add("destinations");
}
