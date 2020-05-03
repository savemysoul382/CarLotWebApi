using System;
using System.ComponentModel.DataAnnotations;

namespace AutoLotDAL.Models.MetaData
{
    public class InventoryMetaData
    {
        [Display(Name = "Pet Name")]
        public String PetName;
        [StringLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        public String Make;
    }
}
