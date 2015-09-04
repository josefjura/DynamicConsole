﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicConsole.Tests
{
    using global::DynamicConsole.Commands.Attributes;

    public class SingleIndexParameterSignature
    {
        [CommandParameter("param1", Index = 0)]
        public string Param1 { get; set; }
    }

    public class SingleNameParameterSignature
    {
        [CommandParameter("param1")]
        public string Param1 { get; set; }
    }

    public class SingleTokenParameterSignature
    {
        [CommandToken(0, "param1")]
        public string Param1 { get; set; }
    }
}
