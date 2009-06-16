using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class Field : Member
    {
        public override string Kind
        {
            get { return "variable"; }
        }
    }
}