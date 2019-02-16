using System.Collections.Generic;

namespace Pedigree_Certification
{
    public class Parent: Dog
    {
        public int ParentId { get; set; }
        public ICollection<Dog> Offspring { get; set; }
    }
}
