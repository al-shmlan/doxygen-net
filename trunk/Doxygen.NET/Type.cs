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
    public class Type : IDocItem
    {
        #region IDocItem Members

        public virtual string ID { get; protected internal set; }
        public virtual string Kind { get; protected internal set; }
        public virtual string FullName { get; protected internal set; }
        public virtual string Description { get; protected internal set; }

        #endregion

        public Namespace Namespace { get; protected internal set; }
        public virtual List<Type> NestedTypes { get; protected internal set; }
        public List<Member> Members { get; protected internal set; }
        public List<string> BaseTypes { get; protected internal set; }

        public virtual string Name
        {
            get 
            { 
                return FullName.Contains(".") ? 
                    FullName.Remove(0, FullName.LastIndexOf(".") + 1): 
                    FullName; 
            }
        }

        public Type()
        {
            BaseTypes = new List<string>();
            Members = new List<Member>();
        }

        public List<Member> Methods
        {
            get { return Members.FindAll(FindByKind("function")); }
        }

        public List<Member> Constructors
        {
            get { return Members.FindAll(FindByKind("ctor")); }
        }

        public List<Member> Properties
        {
            get { return Members.FindAll(FindByKind("property")); }
        }

        public List<Member> Events
        {
            get { return Members.FindAll(FindByKind("event")); }
        }

        public List<Member> MemberDelegates
        {
            get { return Members.FindAll(FindByKind("memberdelegates")); }
        }

        private static Predicate<Member> FindByKind(string kind)
        {
            return member => member.Kind == kind;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
