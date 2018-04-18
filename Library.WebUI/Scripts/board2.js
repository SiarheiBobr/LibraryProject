$(function () {



    var board = $.connection.boardHub;

    board.client.receivedNewComment = function (username, message) {

        $('#Comments').append('<li><strong>' + htmlEncode(username)

            + '</strong>: ' + htmlEncode(message) + '</li>');

    };

    //$('#UserName').val(prompt('Please Enter Your Name:', ''));

    $('#message').focus();

    $.connection.hub.start().done(function () {

        $('#BtnSend').click(function () {

            board.server.writeComment($('#UserName').val(), $('#TxtMessage').val());

            $('#message').val('').focus();

        });

    });

});



function htmlEncode(value) {

    var encodedValue = $('<div />').text(value).html();

    return encodedValue;

}