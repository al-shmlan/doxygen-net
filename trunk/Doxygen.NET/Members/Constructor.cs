﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class Constructor : Method
    {
        public override string Kind
        {
            get { return "ctor"; }
        }
    }
}