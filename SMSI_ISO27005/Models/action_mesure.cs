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
    
    public partial class action_mesure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public action_mesure()
        {
            this.action = new HashSet<action>();
        }
    
        public int id_mesure { get; set; }
        public string chapitre { get; set; }
        public string objects { get; set; }
        public string mesures { get; set; }
        public string description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<action> action { get; set; }
    }
}
