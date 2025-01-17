using System;

namespace Tashpa11.Model
{
    public class Name
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string UName {  get; set; }
        public string Pass {  get; set; }
        public bool Admin {  get; set; }
    }
    public class Names : List<Name>
    {
        public Names() { }
        public Names(IEnumerable<Name> list)
                : base(list) { }
    }
}
