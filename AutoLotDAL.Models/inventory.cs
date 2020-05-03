using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoLotDAL.Models.Base;

namespace AutoLotDAL.Models
{
    [Table("inventory")]
    public partial class inventory : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inventory()
        {
            
        }
        
        [StringLength(50)]
        public string Make { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50), Column("PetName")]
        public string PetName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
