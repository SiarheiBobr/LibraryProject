var booking = function (bookId, username, date) {
    this.bookId = bookId;
    this.username = username;
    this.date = date;
};

var vm1 = {
    bookings: ko.observableArray([]),
    username: ko.observable(),
    youBooked: ko.observable(false),
    yourBooking: ko.observable(), 
    currentBookId: ko.observable(),
    currentBookCount: ko.observable(),
    bookedCount: ko.observable(),
    freeCount: ko.observable(),
    availableBooking: ko.observable(false),
    

    addBooking: function () {
        if (vm1.availableBooking() === true) {
            $.connection.bookingHub.server.addBooking(vm1.currentBookId(), vm1.username()).done(function () {
                loadBookings();
            });
        }
    },
    handOut: function () {

        $.connection.bookingHub.server.handOut(vm1.currentBookId(), vm1.username()).done(function () {
                loadBookings();
        });
        
    },
    removeBooking: function ()
    {  
            $.connection.bookingHub.server.removeBooking(vm1.currentBookId(), vm1.username()).done(function () {
                loadBookings();
            });     
    }


};


$(function () {
    var hub = $.connection.bookingHub;
    $.connection.hub.start().done(function () {
        loadBookings(); // Load posts when connected to hub
    });

    // Hub calls this after a new comment has been added
    hub.client.receivedNewBooking = function (bookId, username, date) {
        var newBooking = new booking(bookId, username, date);
        vm1.bookings.push(newBooking);
        vm1.bookedCount(vm1.bookedCount() + 1);
        vm1.freeCount(vm1.currentBookCount() - vm1.bookedCount());
        if (vm1.currentBookCount() > vm1.bookedCount()) {
            vm1.availableBooking(true);
        }
        else {
            vm1.availableBooking(false);
        }
    };

    hub.client.failure = function () {
        alert('The data is overdue, refresh page, please!');
    };
    
    hub.client.receivedRemovedBooking = function (bookId, username, date) {
        var removedBooking;
        var match = ko.utils.arrayFirst(vm1.bookings(), function (item) {
            return item.username === username && item.bookId === bookId;

        });
        if (match)
            removedBooking = match;
        vm1.bookings.remove(removedBooking);
        vm1.bookedCount(vm1.bookedCount() - 1);
        vm1.freeCount(vm1.currentBookCount() - vm1.bookedCount());
        if (vm1.currentBookCount() > vm1.bookedCount()) {
            vm1.availableBooking(true);
        }
        else
        {
            vm1.availableBooking(false);
        }

    };

});

//ko.applyBindings(vm1, );
//ko.applyBindings(vm1, document.getElementById("booking-div"));
ko.applyBindings(vm1, $("#booking-div")[0]);

function loadBookings() {
    $.get('/api/bookings/' + $('#bookid').val(), function (data) {
        vm1.username($('#username').val());
        vm1.currentBookId($('#bookid').val());
        var bookingsArray = [];
        vm1.bookedCount(data.length);
        vm1.currentBookCount($('#bookCount').val());
        vm1.freeCount(vm1.currentBookCount() - vm1.bookedCount());
        if (vm1.currentBookCount() > vm1.bookedCount())
        {
            vm1.availableBooking(true);
        }
        else
        {
            vm1.availableBooking(false);   
        }
        vm1.youBooked(false);
        vm1.bookings.removeAll();

        $.each(data, function (i, p) {
            var newBooking = new booking(id = p.BookID, username = p.User.Login, date = p.BookedDate);
            if (p.User.Login === vm1.username())
            {
                vm1.availableBooking(false);
                vm1.youBooked(true);
                vm1.yourBooking(newBooking);
            }
            vm1.bookings.push(newBooking);
        });
       
    });
}

