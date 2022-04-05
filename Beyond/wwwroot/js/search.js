document.getElementById("search")
    .addEventListener("keyup", function (event) {
        event.preventDefault();
        if (event.keyCode === 13) {
            Search();
        }
    });


function Search() {
    var searchInput = document.getElementById("search");
    var tickets = document.getElementsByClassName("card");
    var searchText = searchInput.value.toLowerCase();
    if (searchText === "") {
        for (let ticket of tickets) {
            ticket.style.display = "";
        }
    } else {
        for (let ticket of tickets) {
            var header = ticket.children[1].textContent.toLowerCase();
            if (header.includes(searchText)) {
                ticket.style.display = "";
            } else {
                ticket.style.display = "none";
            }
        }
    }
    
}