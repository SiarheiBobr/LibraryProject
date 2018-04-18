var comment = function (id, message, username, date) {
    this.id = id;
    this.message = message;
    this.username = username;
    this.date = date;
};

var vm = {
    comments: ko.observableArray([]),
    username: ko.observable(),
    currentBookId: ko.observable(),

    writeComment: function () {
        $.connection.boardHub.server.writeComment(vm.currentBookId(),vm.username(), $('#message').val()).done(function () {
            $('#message').val('');
        });
    }
};


$(function () {
    var hub = $.connection.boardHub;
    $.connection.hub.start().done(function () {
        loadInfo(); // Load posts when connected to hub
    });

    // Hub calls this after a new comment has been added
    hub.client.receivedNewComment = function (id, username, message, date) {
        var newComment = new comment(id, message, username, date);
        vm.comments.unshift(newComment);
    };
});

//ko.applyBindings(vm);
ko.applyBindings(vm, $("#comments2-div")[0]);
ko.applyBindings(vm, $("#comments1-div")[0]);


function loadInfo() {
    $.get('/api/comments/' + $('#bookid').val(), function (data) {
        vm.username($('#username').val());
        vm.currentBookId($('#bookid').val());
        var commentsArray = [];
        $.each(data, function (i, p) {
            var newComment = new comment(id = p.BookID, message = p.Text, username = p.User.FirstName + ' ' + p.User.LastName, date = p.CreationDate);
            vm.comments.push(newComment);
        });
    });
}

