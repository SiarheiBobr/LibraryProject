using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace Library.Domain.Entities
{
    public class User
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please enter your firstname")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool Blocked { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int RoleID { get; set; }

        public List<Booking> Bookings { get; set; }

        public List<Comment> Comments { get; set; }

    }
}
