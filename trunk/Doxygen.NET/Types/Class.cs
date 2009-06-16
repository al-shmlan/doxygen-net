using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class Class : Type
    {
        public override string Kind
        {
            get { return "class"; }
        }
    }
}