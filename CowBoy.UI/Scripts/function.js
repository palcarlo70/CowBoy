function GetSelectedRow(lnk) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var customerId = row.cells[1].innerHTML;
    var city = row.cells[1].getElementsByTagName("input")[0].value;
    alert("RowIndex: " + rowIndex + " CustomerId: " + customerId + " City:" + city);
    return false;
}