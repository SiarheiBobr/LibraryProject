using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Library.Domain.Entities
{
    public class Comment
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int CommentID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ForeignKey("User")]
        public int UserID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ForeignKey("Book")]
        public int BookID { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

      
        [HiddenInput(DisplayValue = false)]
        public  Book Book { get; set; }
       
        [HiddenInput(DisplayValue = false)]
        public  User User { get; set; }




    }
}
