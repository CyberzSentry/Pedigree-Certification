using System.Collections.Generic;

namespace Pedigree_Certification
{
    public class Breeder
    {
        public int BreederId { get; set; }
        /// <summary>
        /// Imię
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Nazwisko
        /// </summary>
        public string Surname { get; set; }

        public virtual ICollection<Dog> Dog { get; set; }
    }
}
