
var waitingMessage = document.getElementById("waitingMessage");
var waitingGif = document.getElementById("waitingGif");
var callDetailView = document.getElementById("callDetailView");
var staticBackdropLabel = document.getElementById("staticBackdropLabel");


function ToggleWaitingCallDetails(showIt, message = "") {
    waitingMessage.innerHTML = message;
    if (showIt) {
        waitingGif.style.display = "block";
        callDetailView.style.display = "none";
    }
    else {
        callDetailView.style.display = "block";
        waitingGif.style.display = "none";
    }
}


function DeleteCall(Id) {
    let yes_delete = confirm("Are you sure to delete this record?");
    if (yes_delete !== true)
        return;


    $.ajax({
        url: `/customerCalls/delete?id=${Id}`,
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
            ToggleWaitingCallDetails(false);
        }
    });
}

let customers = [];

function AddOrEditCall(id) {
    ToggleWaitingCallDetails(true, "Loading Call Details...");

    if (customers.length === 0) {

        LoadAllCustomers();
    }

    if (id) {

        staticBackdropLabel.innerHTML = "Edit Call Information";
        $.ajax({
            url: `/customerCalls/get?Id=${id}`,
            type: 'GET',
            headers: { 'Content-Type': 'application/json' },
            xhrFields: {
                withCredentials: true
            },
            dataType: 'text',
            success: function (result, status, xhr) {
                var call = JSON.parse(result);
                LoadCallToForm(call);
                ToggleWaitingCallDetails(false);
            },
            error: function (xhr, status, error) {
                console.log(error);
                ToggleWaitingCallDetails(false);
            }
        });
    }
    else {

        staticBackdropLabel.innerHTML = "Add New Call";
        let call = {};
        LoadCallToForm(call);
        ToggleWaitingCallDetails(false);


    }
}

var timeOfCallInput = document.getElementById("timeOfCallInput");      
var customerSelect = document.getElementById("customerSelect");   
var subjectInput = document.getElementById("subjectInput");   
var notesInput = document.getElementById("notesInput");     
var timeOfCallInput = document.getElementById("timeOfCallInput");     
var currentCall = {};


function LoadCallToForm(call) {
    currentCall = call;
    var dateObject = new Date(call.timeofCall);
    var dateString = formatDate(dateObject);
    var timeString = formatDate(dateObject, true);
    $('#timeOfCallInput').val(timeString);
    //dateOfCallInput.value = timeString;
    //timeOfCallInput.value = timeString;
    console.log(timeString);

    customerSelect.value = call.customerNumber;
    idInput.value = call.id;
    subjectInput.value = call.subject;
    notesInput.value = call.notes;

}

//function customerChanged() {
//    console.log("sddfs");
//    console.log(customerSelect.value);
//}

function LoadAllCustomers() {
    $.ajax({
        url: `/customers/getAll`,
        type: 'GET',
        headers: { 'Content-Type': 'application/json' },
        xhrFields: {
            withCredentials: true
        },
        dataType: 'text',
        success: function (result, status, xhr) {
            customers = JSON.parse(result);
            LoadCustomersToInput(customers)
        },
        error: function (xhr, status, error) {
            return [];
        }
    });
}

function LoadCustomersToInput(customers) {
    var html = "<option></option>";
    customers.forEach(function (customer) {
        html += `<option value='${customer.number}'>${customer.name}</option >`;
    })
    customerSelect.innerHTML = html;
    customerSelect.value = currentCall.customerNumber;
}


function GetCurrentCall() {
    let call =
    {
        "id": `${idInput.value}`,
        "timeOfCall": `${timeOfCallInput.value}`,
        "customerNumber": `${customerSelect.value}`,
        "subject": `${subjectInput.value}`,
        "notes": `${notesInput.value}`,
    };
    return call;
}

function SaveCall() {
    let call = GetCurrentCall();
    console.log(call);

    $.ajax({
        url: `/customerCalls/save`,
        type: 'POST',
        data: call,
        success: function (result, status, xhr) {
            window.location.reload();
        },
        error: function (error) {
            console.log(status);
            ToggleWaitingCallDetails(false);
        }
    });
}



