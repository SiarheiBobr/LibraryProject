﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebUI.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}