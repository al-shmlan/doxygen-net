using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class Method : Member
    {
        public override string Kind
        {
            get { return "function"; }
        }

        public List<Parameter> Parameters { get; protected internal set; }

        public Method()
        {
            Parameters = new List<Parameter>();
        }
    }
}