﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class ForgotPassword
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}