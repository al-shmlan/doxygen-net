/*
 * Doxygen.NET - .NET object wrappers for Doxygen
 * Copyright 2009 - Ra-Software AS
 * This code is licensed under the LGPL version 3.
 * 
 */

namespace Doxygen.NET
{
    public interface IDocItem
    {
        string ID { get; }
        string Kind { get; }
        string FullName { get; }
        string Description { get; }
    }
}
