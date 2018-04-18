using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Library.Domain.Entities
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        public string Name { get; set; }
    }
}
