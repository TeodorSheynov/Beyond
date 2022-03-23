$(document).ready(function () {

    window.$(".side-menu").click(function () {
        window.$("#panel-container").load(`/Control/${(this).dataset.menu}`);
    });

});