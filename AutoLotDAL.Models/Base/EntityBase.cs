using System;
using System.ComponentModel.DataAnnotations;

namespace AutoLotDAL.Models.Base
{
    public class EntityBase
    {
        [Key]
        public Int32 Id { get; set; }

        [Timestamp]
        public Byte[] Timestamp { get; set; }
    }
}
