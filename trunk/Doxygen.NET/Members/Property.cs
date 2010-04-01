﻿/*
 * Doxygen.NET - .NET object wrappers for Doxygen
 * Copyright 2009 - Ra-Software AS
 * This code is licensed under the LGPL version 3.
 * 
 */

using System;

namespace Doxygen.NET
{
    [Serializable]
    public class Property : Member
    {
        public override string Kind
        {
            get { return "property"; }
        }

        public string Signature
        {
            get
            {
                return string.Format("{0}{1}{2}", ReturnType, ReturnType.Contains(".") ? string.Empty : " ", Name);
            }
        }
    }
}