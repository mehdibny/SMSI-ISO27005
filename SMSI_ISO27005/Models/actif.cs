//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMSI_ISO27005.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class actif
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public actif()
        {
            this.CID_actif = new HashSet<CID_actif>();
            this.vulnerabilte = new HashSet<vulnerabilte>();
        }
    
        public int id_actif { get; set; }
        public string nom_actif { get; set; }
        public Nullable<System.DateTime> date_creation_actif { get; set; }
        public string descr_actif { get; set; }
        public string categ_actif { get; set; }
        public string matricule { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CID_actif> CID_actif { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vulnerabilte> vulnerabilte { get; set; }
        public virtual collaborateur collaborateur { get; set; }
    }
}
