


var mobileNav = document.getElementById("mobile-nav");
var primaryNav = document.getElementById("primary-nav");
if (mobileNav != null) {

    mobileNav.addEventListener("click", (e) => {
        // e.preventDefault();
        var visibility = primaryNav.getAttribute("data-visible");

        if (visibility === "false") {
            primaryNav.setAttribute("data-visible", "true");
            mobileNav.setAttribute("aria-expanded", "true");
        } else if (visibility === "true") {
            mobileNav.setAttribute("aria-expanded", "false");
            primaryNav.setAttribute("data-visible", "false");
        }

    });
}