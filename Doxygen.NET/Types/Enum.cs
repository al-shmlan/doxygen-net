using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class Enum : Type
    {
        public override string Kind
        {
            get { return "enum"; }
        }

        public override List<Type> NestedTypes
        {
            get { return null; }
        }
    }
}
