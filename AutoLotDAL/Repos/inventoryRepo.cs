using System.Collections.Generic;
using System.Linq;
using AutoLotDAL.Models;

namespace AutoLotDAL.Repos
{
    public class inventoryRepo : BaseRepo<inventory>
    {
        public List<inventory> GetAll() => Context.Cars.OrderBy(x => x.PetName).ToList();
    }
}