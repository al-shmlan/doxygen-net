/*
 * Doxygen.NET - .NET object wrappers for Doxygen
 * Copyright 2009 - Ra-Software AS
 * This code is licensed under the LGPL version 3.
 * 
 */

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

        public string Signature
        {
            get 
            {
                string parameters = string.Empty;
                bool first = true;
                foreach (Parameter param in Parameters)
	            {
                    parameters += string.Format("{0}{1}", first ? string.Empty : ", ", param);
                    first = false;
	            }
                return string.Format("{0}{1}{2}({3})", ReturnType, ReturnType.Contains(".") ? string.Empty : " " , Name, parameters);
            }
        }

    }
}