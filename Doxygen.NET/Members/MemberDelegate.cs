using System;
using System.Collections.Generic;
using System.Text;

namespace Doxygen.NET
{
    public class MemberDelegate : Member
    {
        public override string Kind
        {
            get { return "memberdelegate"; }
        }
    }
}