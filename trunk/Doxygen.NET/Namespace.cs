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
    public class Namespace : IDocItem
    {
        #region IDocItem Members

        public string ID { get; protected internal set; }

        public string Kind 
        {
            get { return "namespace"; }
        }

        public string FullName { get; protected internal set; }
        public string Description { get; protected set; }

        #endregion

        public Namespace()
        {
            Types = new List<Type>();
        }

        public List<Type> Types { get; protected internal set; }

        public List<Type> Classes
        {
            get { return Types.FindAll(FindByKind("class")); }
        }

        public List<Type> Structs
        {
            get { return Types.FindAll(FindByKind("struct")); }
        }

        public List<Type> Enums
        {
            get { return Types.FindAll(FindByKind("enum")); }
        }

        public List<Type> Delegates
        {
            get { return Types.FindAll(FindByKind("delegate")); }
        }

        private static Predicate<Type> FindByKind(string kind)
        {
            return type => type.Kind == kind;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
