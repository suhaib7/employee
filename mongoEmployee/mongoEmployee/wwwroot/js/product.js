var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#productTable').DataTable({
        "ajax": {
            "url": '/product/getall', // Adjust URL based on your route configuration
            "dataSrc": "data" // Data source property
        },
        "columns": [
            { "data": "_id", "width": "25%" }, // Adjust width and data as needed
            { "data": "name", "width": "25%" },
            // Add more columns as needed
        ],
        "paging": true, // Enable pagination if needed
        "lengthChange": false, // Disable length change
        "searching": true, // Enable search bar
        "ordering": true, // Enable ordering
        "info": true, // Enable table information
        "autoWidth": false // Disable auto-width calculation for columns
    });
}
