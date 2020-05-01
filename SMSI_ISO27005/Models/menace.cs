//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMSI_ISO27005.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class menace
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public menace()
        {
            this.impact = new HashSet<impact>();
            this.prob_occurrence = new HashSet<prob_occurrence>();
        }

        [Required(ErrorMessage = "Ce Champs Et Obligatoire")]
        public int id_menace { get; set; }
        [Required(ErrorMessage = "Ce Champs Et Obligatoire")]
        public string nom_menace { get; set; }
        [Required(ErrorMessage = "Ce Champs Et Obligatoire")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm}")]
        public Nullable<System.DateTime> date_creation_menace { get; set; }
        [Required(ErrorMessage = "Ce Champs Et Obligatoire")]
        public string desc_menace { get; set; }
        [Required(ErrorMessage = "Ce Champs Et Obligatoire")]
        public Nullable<int> id_vulne { get; set; }
        public string errorMessage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<impact> impact { get; set; }
        public virtual vulnerabilte vulnerabilte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prob_occurrence> prob_occurrence { get; set; }
    }
}
