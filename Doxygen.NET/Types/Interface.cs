/*
 * Doxygen.NET - .NET object wrappers for Doxygen
 * Copyright 2009 - Ra-Software AS
 * This code is licensed under the LGPL version 3.
 * 
 */

using System;

namespace Doxygen.NET
{
    [Serializable]
    public class Interface : Class
    {
        public override string Kind
        {
            get { return "interface"; }
        }
    }
}
