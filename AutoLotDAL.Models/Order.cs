using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoLotDAL.Models.Base;

namespace AutoLotDAL.Models
{
    public partial class Order : EntityBase
    {
        public int Custld { get; set; }

        public int Carld { get; set; }

        [ForeignKey(nameof(Custld))]
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Carld))]
        public virtual inventory Car { get; set; }
    }
}
