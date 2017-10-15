﻿var uri = 'api/notes';

$(document).ready(function () {
    // Send an AJAX request
    refreshNoteList();
});

//function formatItem(item) {
//    return item.Priority + ">" + item.Subject + ":  " + item.Details;
//}

function refreshNoteList() {
    $.getJSON(uri)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            $.each(data, function (key, item) {
                // Add a list item for the product.
                // Change the way to format the string(Sunny)
                $('#notes').append('<li><a data-transition="pop" data-parm=' + item.Id + ' href="#details-page">' + item.Priority + ' > ' + item.Subject + '</a></li>');
                // Listview refresh after each inner loop(Sunny)
                $("#notes").listview("refresh");
            });
        });
}

function clearResponse() {
    $('#deleteResponse').text("");
    $('#saveResponse').text("");
}

function find() {
    clearResponse()
    var id = $('#noteId').val();
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            $('#note').text(formatItem(data));
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#note').text('Error: ' + err);
        });
}

function deleteNote() {
    clearResponse()
    var id = $('#deleteNote').val();
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        contentType: "application/json",
        success: function () {
            $("#notes").empty();
            refreshNoteList();
            $('#deleteResponse').text("Success: Note Deleted");
        },
        error: function () {
            $('#deleteResponse').text("Error: Delete Failed");
        }
    });
}

function saveNote() {
    clearResponse()
    var note = {
        subject: $('#Subject').val(),
        details: $('#Details').val(),
        priority: $('#Priority').val()
    };

    $.ajax({
        url: uri + "/notes",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(note),
        success: function (data) {
            //self.notes.push(data);
            $("#notes").empty();
            refreshNoteList();
            $('#saveResponse').text("Success: Saved Note");
        },
        error: function () {
            $('#saveResponse').text("Error: Save Failed");
        }
    });
}