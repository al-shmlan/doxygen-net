/*
 * Doxygen.NET - .NET object wrappers for Doxygen
 * Copyright 2009 - Ra-Software AS
 * This code is licensed under the LGPL version 3.
 * 
 */

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace Doxygen.NET
{
    [Serializable]
    public class Docs
    {
        private readonly XmlDocument _indexXmlDoc;

        public List<FileInfo> XmlFiles { get; set; }
        public DirectoryInfo XmlDirectory { get; protected set; }
        public List<Namespace> Namespaces { get; protected set; }

        private bool _eagerParsing;
                
        public Docs(string doxygenXmlOuputDirectoryPath)
            : this(doxygenXmlOuputDirectoryPath, true)
        { }

        public Docs(string doxygenXmlOuputDirectoryPath, bool eagerParsing)
        {
            if (!Directory.Exists(doxygenXmlOuputDirectoryPath))
                throw new Exception("The specified directory does not exist.");

            XmlDirectory = new DirectoryInfo(doxygenXmlOuputDirectoryPath);

            if (!File.Exists(Path.Combine(XmlDirectory.FullName, "index.xml")))
                throw new Exception("The specified directory does not contain an essential file, \"index.xml\".");

            _eagerParsing = eagerParsing;

            _indexXmlDoc = new XmlDocument();
            _indexXmlDoc.Load(Path.Combine(XmlDirectory.FullName, "index.xml"));

            XmlFiles = new List<FileInfo>(XmlDirectory.GetFiles("*.xml"));

            LoadNamespaces();    
        }

        public Namespace GetNamespaceByName(string namespaceName)
        {
            return Namespaces.Find(n => n.FullName == namespaceName);
        }

        public Type GetTypeByName(string typeFullName)
        {
            string namespaceName = typeFullName.Remove(typeFullName.LastIndexOf("."));
            Namespace nspace = GetNamespaceByName(namespaceName);

            if (nspace != null)
            {
                Type type = nspace.Types.Find(t => t.FullName == typeFullName);
                return type;
            }

            return null;
        }

        public Type GetTypeByID(string id)
        {
            foreach (Namespace nspace in Namespaces)
            {
                Type type = nspace.Types.Find(t => t.ID == id);
                if (type != null)
                    return type;
            }
            return null;
        }

        public List<Class> GetAllClasses()
        {
            List<Class> classes = new List<Class>();
            foreach (Namespace nspace in Namespaces)
            {
                foreach (Class c in nspace.Classes)
                {
                    classes.Add(c);
                }
            }
            return classes;
        }

        public void LoadNamespaces()
        {
            Namespaces = new List<Namespace>();
            XmlNodeList namespaceXmlNodes = _indexXmlDoc.SelectNodes("/doxygenindex/compound[@kind=\"namespace\"]");

            if (namespaceXmlNodes == null) 
                return;

            foreach (XmlNode namespaceXmlNode in namespaceXmlNodes)
            {
                Namespace nspace = new Namespace
                                   {
                                       ID = namespaceXmlNode.Attributes["refid"].Value,
                                       FullName = GetElementInnerText(namespaceXmlNode, "name").Replace("::", ".")
                                   };

                if (_eagerParsing)
                {
                    LoadTypes(nspace, false);
                }
                Namespaces.Add(nspace);
            }
        }

        private static string GetElementInnerText(XmlNode xmlNode, string elementName)
        {
            XmlElement element = xmlNode[elementName];
            
            if (element != null) 
                return element.InnerText ?? string.Empty;

            return string.Empty;
        }

        private static string GetElementInnerXml(XmlNode xmlNode, string elementName)
        {
            XmlElement element = xmlNode[elementName];

            if (element != null)
                return element.InnerXml ?? string.Empty;

            return string.Empty;
        }

        public void LoadTypes(Namespace nspace, bool forceReload)
        {
            if (!forceReload && nspace.Types.Count > 0)
                return;

            nspace.Types = new List<Type>();

            XmlNodeList typesXmlNodes = _indexXmlDoc.SelectNodes(
                "/doxygenindex/compound[@kind=\"class\" or @kind=\"interface\" or @kind=\"enum\" or @kind=\"struct\" or @kind=\"delegate\"]");

            if (typesXmlNodes == null) 
                return;

            foreach (XmlNode typeXmlNode in typesXmlNodes)
            {
                string typeName = GetElementInnerText(typeXmlNode, "name").Replace("::", ".");
                if (typeName.Contains(".") && typeName.Remove(typeName.LastIndexOf(".")) == nspace.FullName)
                {
                    Type t = CreateNewType(typeXmlNode.Attributes["kind"].Value);

                    t.ID = typeXmlNode.Attributes["refid"].Value;
                    t.Kind = typeXmlNode.Attributes["kind"].Value;
                    t.FullName = typeName;
                    t.Namespace = nspace;

                    if (_eagerParsing)
                    {
                        LoadTypesMembers(t, false);
                    }
                    nspace.Types.Add(t);
                }
            }
        }
        
        public void LoadTypesMembers(Type t, bool forceReload)
        {
            if (!forceReload && t.Members.Count > 0)
                return;

            FileInfo typeXmlFile = XmlFiles.Find(
                file => file.Name.Remove(file.Name.LastIndexOf(file.Extension)) == t.ID);

            if (typeXmlFile == null || !typeXmlFile.Exists)
                return;

            XmlDocument typeDoc = new XmlDocument();
            typeDoc.Load(typeXmlFile.FullName);

            t.Description = typeDoc.SelectSingleNode("/doxygen/compounddef/detaileddescription").InnerXml.Replace("preformatted", "pre");

            XmlNodeList baseTypes = typeDoc.SelectNodes("/doxygen/compounddef/basecompoundref");
            
            if (baseTypes != null)
            {
                foreach (XmlNode baseType in baseTypes)
                {
                    t.BaseTypes.Add(baseType.Attributes["refid"].Value);
                }
            }

            XmlNodeList members = typeDoc.SelectNodes("/doxygen/compounddef/sectiondef/memberdef");

            if (members == null) 
                return;

            foreach (XmlNode member in members)
            {
                string kind = member.Attributes["kind"].Value;
                string name = GetElementInnerText(member, "name");
                string args = GetElementInnerText(member, "argsstring").Replace("(", "").Replace(")", "").Trim();

                List<Parameter> parameters = new List<Parameter>();

                if (!string.IsNullOrEmpty(args) && kind == "function")
                {
                    string[] argsSplits = args.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string arg in argsSplits)
                    {
                        string[] argParts = arg.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        Parameter p = new Parameter { Type = argParts[0].Trim(), Name = argParts[1].Trim() };
                        parameters.Add(p);
                    }
                }
                
                if (kind == "function" && name == t.Name)
                    kind = "ctor";

                Member m = CreateNewMember(kind);

                if (parameters.Count > 0)
                    ((Method) m).Parameters = parameters;

                m.ID = member.Attributes["id"].Value;
                m.FullName = string.Format("{0}.{1}", t.FullName, name);
                m.Name = name;
                m.Kind = kind;
                m.Description = GetElementInnerXml(member, "detaileddescription").Replace("preformatted", "pre");
                m.AccessModifier = member.Attributes["prot"].Value;
                m.Parent = t;
                m.ReturnType = GetElementInnerText(member, "type");
                if (m.ReturnType.EndsWith("."))
                    m.ReturnType += m.Name;
                t.Members.Add(m);
            }
        }

        private static Type CreateNewType(string kind)
        {
            switch (kind)
            {
                case "class":
                    return new Class();
                case "interface":
                    return new Interface();
                case "delegate":
                    return new Delegate();
                case "enum":
                    return new Enum();
                case "struct":
                    return new Struct();
            }
            return new Type();
        }

        private static Member CreateNewMember(string kind)
        {
            switch (kind)
            {
                case "property":
                    return new Property();
                case "event":
                    return new Event();
                case "function":
                    return new Method();
                case "variable":
                    return new Field();
                case "ctor":
                    return new Constructor();
                case "memberdelegates":
                    return new MemberDelegate();
            }
            return new Member();
        }
    }
}
