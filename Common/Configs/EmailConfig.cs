﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configs
{
    public class EmailConfig
    {
        public static string Position { get; } = "email";
        public string Name { get; set; }
        public string Password { get; set; }
    }
}