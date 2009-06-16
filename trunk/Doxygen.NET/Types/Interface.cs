using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class Interface : Class
    {
        public override string Kind
        {
            get { return "interface"; }
        }
    }
}
