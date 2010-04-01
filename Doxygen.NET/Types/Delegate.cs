/*
 * Doxygen.NET - .NET object wrappers for Doxygen
 * Copyright 2009 - Ra-Software AS
 * This code is licensed under the LGPL version 3.
 * 
 */

using System;
using System.Collections.Generic;

namespace Doxygen.NET
{
    [Serializable]
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
