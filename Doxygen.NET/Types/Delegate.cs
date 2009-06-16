using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class Delegate : Type
    {
        public override string Kind
        {
            get { return "delegate"; }
        }

        public override List<Type> NestedTypes
        {
            get { return null; }
        }
    }
}
