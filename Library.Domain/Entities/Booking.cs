using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Entities
{
    public class Booking
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int BookingID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ForeignKey("User")]
        public int UserID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ForeignKey("Book")]
        public int BookID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Boolean Booked { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Boolean? Issued { get; set; }


        public DateTime BookedDate { get; set; }


        public DateTime? IssuedDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        public  Book Book { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public  User User { get; set; }




    }
}
