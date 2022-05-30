// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    // Send an AJAX request
    $.getJSON('api/Orders')   
        .done(function (data) {
            // On success, 'data' contains a list of products.
            let orderArray = data;
            drawTable(orderArray);
        });

    $.getJSON('api/SelectGets/GetSales')
        .done(function (data) {
            // On success, 'data' contains a list of salespeople.
            let SalesPersonArray = data;
            let salesPersonSelect = document.getElementById("salesPersonSelect");
            SalesPersonArray.forEach(function (value) {
                salesPersonSelect.appendChild(new Option((value.firstName + " " + value.lastName) , value.salesPersonId));
            });

        });

    $.getJSON('api/SelectGets/GetStores')
        .done(function (data) {
            // On success, 'data' contains a list of salespeople.
            let StoreArray = data;
            let storeSelect = document.getElementById("storeSelect");
            StoreArray.forEach(function (value) {
                storeSelect.appendChild(new Option(value.city, value.storeId));
            });
            StoreArray.forEach(function (value) {
                storePerformanceSelect.appendChild(new Option(value.city, value.storeId));
            });
        });

    $.getJSON('api/SelectGets/GetCds')
        .done(function (data) {
            // On success, 'data' contains a list of salespeople.
            let cdArray = data;
            let cdSelect = document.getElementById("cdSelect");
            cdArray.forEach(function (value) {
                cdSelect.appendChild(new Option(value.cdname, value.cdId));
            });

        });
});
function formatItem(item) {
    return item.counted; // + '   ' + item.subject + '   ' + item.details
}

function drawTable(orderArray) {
    // get the reference for the table
    // creates a <table> element
    var tbl = document.getElementById('ordersTable');
    while (tbl.rows.length > 1) {  // clear, but don't delete the header
        tbl.deleteRow(1);
    }

    // creating rows
    for (var r = 0; r < orderArray.length; r++) {
        var row = document.createElement("tr");

        var cell0 = document.createElement("td");
        var cell1 = document.createElement("td");
        var cell2 = document.createElement("td");
        var cell3 = document.createElement("td");
        var cell4 = document.createElement("td");
        var cell5 = document.createElement("td");

        cell0.appendChild(document.createTextNode(orderArray[r].ordersId));
        row.appendChild(cell0);
        cell1.appendChild(document.createTextNode(orderArray[r].storeId));
        row.appendChild(cell1);
        cell2.appendChild(document.createTextNode(orderArray[r].salesPersonId));
        row.appendChild(cell2);
        cell3.appendChild(document.createTextNode(orderArray[r].cdId));
        row.appendChild(cell3);
        cell4.appendChild(document.createTextNode(orderArray[r].pricePaid));
        row.appendChild(cell4);
        cell5.appendChild(document.createTextNode(orderArray[r].date));
        row.appendChild(cell5);

        tbl.appendChild(row); // add the row to the end of the table body
    }

}

function findStorePerformance() {

    $.ajax({
        url: "api/Orders",
        type: "POST",
        data: JSON.stringify(newOrder),
        contentType: "application/json; charset=utf-8",

        success: function (result) {
            alert(result + " was added");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }
    });


}

function addOrder() {

    let selectSalesPerson = document.getElementById('salesPersonSelect');
    let salesPersonValue = selectSalesPerson.options[selectSalesPerson.selectedIndex].value;

    let selectCd = document.getElementById('cdSelect');
    let cdValue = selectCd.options[selectCd.selectedIndex].value;

    let selectStore = document.getElementById('storeSelect');
    let storeValue = selectStore.options[selectStore.selectedIndex].value;

    console.log(salesPersonValue)
    console.log(cdValue)
    console.log(storeValue)

    let newOrder = new Order();
    newOrder.SalesPersonId = salesPersonValue;
    newOrder.CdId = cdValue;
    newOrder.StoreId = storeValue;
    console.log(newOrder);
    $.ajax({
        url: "api/Orders",
        type: "POST",
        data: JSON.stringify(newOrder),
        contentType: "application/json; charset=utf-8",

        success: function (result) {
            alert(result + " was added");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }
    });
}

function findStorePerformance() {

    let selectPerformanceStore = document.getElementById('storePerformanceSelect');
    let storePerformanceValue = selectPerformanceStore.options[selectPerformanceStore.selectedIndex].value;

    console.log(storePerformanceValue);

    $.getJSON('api/Orders' + '/' + storePerformanceValue)
        .done(function (data) {
            console.log(data);
            $('#description').text(data);
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#description').text('Error: ' + err);
        });

}




let Order = function (pOrdersId, pStoreId, pSalesPersonId, pCdId, pPricePaid,pDate) {
    this.OrdersId = pOrdersId;
    this.StoreId = pStoreId;
    this.SalesPersonId = parseInt(pSalesPersonId);
    this.CdId = pCdId;
    this.PricePaid = pPricePaid;
    this.Date = pDate;
}
