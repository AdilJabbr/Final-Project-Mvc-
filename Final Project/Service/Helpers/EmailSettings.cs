﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class EmailSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
    }
}
