var uri = 'api/notes';

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
            $("#deleteNote").val('');    
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
            $("#Subject").val('');
            $("#Details").val('');    
            $("#Priority").val('');    
        },
        error: function () {
            $('#saveResponse').text("Error: Save Failed");
        }
    });
}

$(document).on('pagebeforeshow', '#delete-page', function () {

});

$(document).on('pagebeforeshow ', '#pageone', function () {   // see: https://stackoverflow.com/questions/14468659/jquery-mobile-document-ready-vs-page-events
    var info_view = "";      //string to put HTML in
    $('#notes').empty();  // since I do this everytime the page is redone, I need to remove existing before apending them all again


    $.each(data, function (index, record) {   // make up each li as an <a> to the details-page
        $('#notes').append('<li><a data-transition="pop" data-parm=' + record.Id + ' href="#details-page">' + record.Priority + ' => ' + record.Subject + '</a></li>');
    });

    $("#notes").listview('refresh');  // need this so jquery mobile will apply the styling to the newly added li's

    $("a").on("click", function (event) {    // set up an event, if user clicks any, it writes that items data-parm into the 
        //details page's html so I can get it there
        var parm = $(this).attr("data-parm");  // passing in the record.Id
        //do something here with parameter on  details page
        $("#detailParmHere").html(parm);

    });


});

$(document).on('pagebeforeshow', '#details-page', function () {

    var textString = 'fix me';
    var id = $('#detailParmHere').text();

    $.each(data, function (index, record) {
        if (id = record.Id) {
            textString = "Priority: " + record.Priority + " Subject: " + record.Subject + " Details: " + record.Details;
        }
    });

    $('#showdata').text(textString);
});
