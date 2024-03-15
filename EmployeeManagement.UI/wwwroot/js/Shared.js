const BaseUrl = "http://localhost:5072";

function post(url, data, success, exception) {
    return fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(result => {
            // Handle the result
            success(result);
        })
        .catch(error => {
            // Handle the error
            exception(error);
        });
}

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;';
}

function appendTableData(tableId, data) {
    var table = document.getElementById(tableId);
    var tbody = table.getElementsByTagName('tbody')[0];

    // Create a new row
    var newRow = document.createElement('tr');

    // Create cells and populate with data
    for (var i = 0; i < data.length; i++) {
        var cell = document.createElement('td');
        cell.textContent = data[i];
        newRow.appendChild(cell);
    }

    // Append the new row to the table body
    tbody.appendChild(newRow);
}
