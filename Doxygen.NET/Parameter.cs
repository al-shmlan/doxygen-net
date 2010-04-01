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
    public class Parameter
    {
        public string Type { get; protected internal set; }
        public string Name { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Type, Name);
        }
    }
}
