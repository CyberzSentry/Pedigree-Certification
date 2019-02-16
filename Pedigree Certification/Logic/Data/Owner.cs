using System.Collections.Generic;

namespace Pedigree_Certification
{
    public class Owner
    {
        public int OwnerId { get; set; }
        /// <summary>
        /// Imię
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Nazwisko
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Adres
        /// </summary>
        public string Adderss { get; set; }

        public virtual ICollection<Dog> Dog { get; set; }
    }
}
