using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class Struct : Type
    {
        public override string Kind
        {
            get { return "struct"; }
        }
    }
}
