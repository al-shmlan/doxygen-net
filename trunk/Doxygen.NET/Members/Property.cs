using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class Property : Member
    {
        public override string Kind
        {
            get { return "property"; }
        }
    }
}