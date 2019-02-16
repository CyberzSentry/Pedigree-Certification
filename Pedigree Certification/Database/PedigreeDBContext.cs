using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedigree_Certification
{
    class PedigreeDbContext: DbContext
    {
        /// <summary>
        /// Dogs table
        /// </summary>
        public DbSet<Dog> Dogs { get; set; }
        /// <summary>
        /// Owners table
        /// </summary>
        public DbSet<Owner> Owners { get; set; }
        /// <summary>
        /// Breaders table
        /// </summary>
        public DbSet<Breeder> Breeders { get; set; }
        /// <summary>
        /// Parents table
        /// </summary>
        public DbSet<Parent> Parents { get; set; }
    }
}
