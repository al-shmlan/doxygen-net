using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class Event : Member
    {
        public override string Kind
        {
            get { return "event"; }
        }
    }
}