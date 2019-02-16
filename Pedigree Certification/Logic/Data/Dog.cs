using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Pedigree_Certification
{
    public class Dog
    {
        public int DogId { get; set; }
        /// <summary>
        /// Imię
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Nr Chip
        /// </summary>
        public string ChipNumber { get; set; }
        /// <summary>
        /// Przydomek
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// Numer Rodowodu
        /// </summary>
        public string PedegreeCertificateNumber { get; set; }
        /// <summary>
        /// Rasa
        /// </summary>
        public string Rase { get; set; }
        /// <summary>
        /// Tatuaż
        /// </summary>
        public string Tatoo { get; set; }
        /// <summary>
        /// Data urodzenia
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// Umaszczenie
        /// </summary>
        public string Coat { get; set; }
        /// <summary>
        /// Płeć
        /// </summary>
        public string Sex {get; set;}
        /// <summary>
        /// Dysplazja
        /// </summary>
        public string Dysplasia { get; set;}
        /// <summary>
        /// Hodowca
        /// </summary>
        public virtual Breeder Breeder { get; set; }
        /// <summary>
        /// Właściciel
        /// </summary>
        public virtual Owner Owner { get; set; }
        /// <summary>
        /// Ojciec
        /// </summary>
        public int? FatherId { get; set; }
        /// <summary>
        /// Matka
        /// </summary>
        public int? MotherId { get; set; }
        /// <summary>
        /// DNA
        /// </summary>
        public string DNA { get; set; }
        /// <summary>
        /// Tytuły wystawowe
        /// </summary>
        public string ExhibitionTitles { get; set; }
        /// <summary>
        /// Wyszkolenie
        /// </summary>
        public string Training { get; set; }
        /// <summary>
        /// Data nadania uprawnień
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime? CertificationDate { get; set; }
        /// <summary>
        /// Numer certyfikatu
        /// </summary>
        public int? CertificationNo { get; set; }
    }
}
