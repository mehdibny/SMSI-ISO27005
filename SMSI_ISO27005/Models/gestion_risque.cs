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
    
    public partial class gestion_risque
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public gestion_risque()
        {
            this.action = new HashSet<action>();
        }
    
        public int id_gestion_risk { get; set; }
        public string nom_gesion_risk { get; set; }
        public string description_gestion_risk { get; set; }
        public Nullable<System.DateTime> date_creation_g_risk { get; set; }
        public string risk_dicision { get; set; }
        public Nullable<int> id_vulne { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<action> action { get; set; }
        public virtual vulnerabilte vulnerabilte { get; set; }
    }
}
