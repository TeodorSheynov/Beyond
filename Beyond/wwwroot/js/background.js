var resizeTimer;
window.addEventListener("resize", () => {
    document.body.classList.add("resize-animation-stopper");
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(() => {
        document.body.classList.remove("resize-animation-stopper");
    }, 400);
});

document.addEventListener("DOMContentLoaded", () => document.body.classList.remove("preload"))


var bod = document.getElementById("bod-bg");
window.addEventListener('load', (e) => {
    bod.classList.add('loaded');
});

var cls = ["home", "crew", "destinations", "reglog"];
var loc = window.location.href.toLowerCase();
var register = loc.indexOf("/identity/account/register") !== -1;
var login = loc.indexOf("/identity/account/login") !== -1;
var destinations = loc.indexOf("/destinations") !== -1;
if (destinations) {
    bod.classList.remove(...cls);
    bod.classList.add("destinations");
} else if (register || login) {
    bod.classList.remove(...cls);
    bod.classList.add("reglog");
} else {
    bod.classList.remove(...cls);
    bod.classList.add("crew");
}
