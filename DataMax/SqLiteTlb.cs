using System;

namespace DataMax
{
    public class SqLiteTlb : Attribute
    {
        public string TblName { get; }

        public SqLiteTlb(string name)
        {
            TblName = name;
        }
    }
}
