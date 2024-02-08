using System;

namespace DataMax
{
    public class SqLiteFld : Attribute
    {
        public string FldName { get; }
        public bool FldPK { get; }
        public bool FldIndex { get; }

        public SqLiteFld(string name, bool PK, bool Index)
        {
            FldName = name;
            FldPK = PK;
            FldIndex = Index;
        }
    }
}
