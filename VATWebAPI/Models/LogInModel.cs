﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VATWebAPI.Models
{
    public class LogInModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        public string BIN { get; set; }
        
    }
}