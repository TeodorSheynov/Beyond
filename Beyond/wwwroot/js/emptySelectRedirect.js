function Redirect(element) {

    window.$("#panel-container").load(`/Control/${element.value}`);
}