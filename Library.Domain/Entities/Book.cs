using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Library.Domain.Entities
{
    public class Book
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int? BookID { get; set; }

        [Required(ErrorMessage = "Please enter a book title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a book description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a book author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please enter a book publish year")]
        [Range(1,2018, ErrorMessage = "Please enter a correct year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter a book category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter a count of these books")]
        public int Count { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public List<Booking> Bookings { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
