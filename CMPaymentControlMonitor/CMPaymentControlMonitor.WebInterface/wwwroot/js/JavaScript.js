function changeCardColorToRed() {
    var allCards = document.getElementsByClassName("card");

    for (var i = 0; i < allCards.length; i++) {
        if (parseInt(allCards[i].dataset.cardFailedTransactions) > 0) {
            allCards[i].className += " card-with-failed-transactions";
            $(allCards[i]).children().children(".card-title").css("color", "white");

        }
    }
}
changeCardColorToRed();

//history filter
function filterFunction(checkID) {
    var table, tr, td, i; 
    table = document.getElementById("check-table-history");
    tr = table.getElementsByTagName("tr");

    //looks at check id cell in row to compare the changed checkbox with correct checkid
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[tr.length + 2];
         if (td && td.innerHTML == checkID) {
                if (tr[i].style.display == "") {
                    tr[i].style.display = "none";
                    } else {
                        tr[i].style.display = "";
                    }
                } 
            }
        }