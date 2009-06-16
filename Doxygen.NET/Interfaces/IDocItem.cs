using System;
using System.Collections.Generic;
using System.Text;

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
