
$(document).ready(function () {
    $('#table').DataTable({
        "ordering": true,
        "columnDefs": [{
            "targets": [1, 2, 3, 4],
            "ordorable": false
        }]
    });
});

var waitingMessage = document.getElementById("waitingMessage");
var waitingGif = document.getElementById("waitingGif");
var customerDetailView = document.getElementById("customerDetailView");
var staticBackdropLabel = document.getElementById("staticBackdropLabel");

function ToggleWaitingCustomerDetails(showIt, message = "") {
    waitingMessage.innerHTML = message;
    if (showIt) {
        waitingGif.style.display = "block";
        customerDetailView.style.display = "none";
    }
    else {
        customerDetailView.style.display = "block";
        waitingGif.style.display = "none";
    }
}


function DeleteCustomer(customerNumber) {
    let yes_delete = confirm("Are you sure to delete this customer?");
    if (yes_delete !== true)
        return;


    $.ajax({
        url: `/customers/delete?customerNumber=${customerNumber}`,
        type: 'GET',
        headers: { 'Content-Type': 'application/json' },
        xhrFields: {
            withCredentials: true
        },
        dataType: 'text',
        success: function (result, status, xhr) {
            window.location.reload();
        },
        error: function (xhr, status, error) {
            console.log(error);
            ToggleWaitingCustomerDetails(false);
        }
    });
}

function AddOrEditCustomer(customerNumber) {
    ToggleWaitingCustomerDetails(true, "Loading Customer Details...");

    if (customerNumber === 0) {
        staticBackdropLabel.innerHTML = "Add New Customer";
        let customer =
        {
            "number": 0,
            "name":         '',
            "surName":      '',                      
            "address":      '',                     
            "postalCode":   '',                  
            "country":      '',                     
            "dateOfBirth":  '',                 
        };
        LoadCustomerToForm(customer);
        ToggleWaitingCustomerDetails(false);
    }
    else {
        staticBackdropLabel.innerHTML = "Edit Customer Information";
        $.ajax({
            url: `/customers/get?customerNumber=${customerNumber}`,
            type: 'GET',
            headers: { 'Content-Type': 'application/json' },
            xhrFields: {
                withCredentials: true
            },
            dataType: 'text',
            success: function (result, status, xhr) {
                var customer = JSON.parse(result);
                LoadCustomerToForm(customer);
                ToggleWaitingCustomerDetails(false);
            },
            error: function (xhr, status, error) {
                console.log(error);
                ToggleWaitingCustomerDetails(false);
            }
        });
    }
}

var customerNameInput = document.getElementById("customerNameInput");      
var customerSurNameInput = document.getElementById("customerSurNameInput");   
var customerNumberInput = document.getElementById("customerNumberInput");    
var customerAddressInput = document.getElementById("customerAddressInput");   
var customerPostalCodeInput = document.getElementById("customerPostalCodeInput");
var customerCountryInput = document.getElementById("customerCountryInput");   
var customerDOBInput = document.getElementById("customerDOBInput");       


function LoadCustomerToForm(customer) {
    console.log(customer);
    customerNameInput.value = customer.name;
    customerSurNameInput.value = customer.surName;
    customerNumberInput.value = customer.number;
    customerAddressInput.value = customer.address;
    customerPostalCodeInput.value = customer.postalCode;
    customerCountryInput.value = customer.country;

    var dob = new Date(customer.dateOfBirth);
    var dobString = formatDate(dob);
    customerDOBInput.value = dobString;
}


function formatDate(date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    return `${year}-${month.toLocaleString(undefined, { minimumIntegerDigits: 2 })}-${day.toLocaleString(undefined, { minimumIntegerDigits: 2 }) }`;
}

function GetCurrentCustomer() {
    let customer =
    {
        "Name": `${customerNameInput.value}`,
        "SurName": `${customerSurNameInput.value}`,
        "Number": `${customerNumberInput.value}`,
        "Address": `${customerAddressInput.value}`,
        "PostalCode": `${customerPostalCodeInput.value}`,
        "Country": `${customerCountryInput.value}`,
        "DateOfBirth": `${customerDOBInput.value}`,
    };
    return customer;
}

function SaveCustomer() {
    let customer = GetCurrentCustomer();
    let stringified = JSON.stringify(customer);
    console.log(stringified);
    console.log(customer);


    $.ajax({
        url: `/customers/save`,
        type: 'POST',
        data: customer,
        success: function (result, status, xhr) {
            window.location.reload();

        },
        error: function (error) {
            console.log(status);
            ToggleWaitingCustomerDetails(false);
        }
    });
}

