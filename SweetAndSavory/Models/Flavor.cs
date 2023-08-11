using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweetAndSavory.Models
{
    public class Flavor
    {
        public int FlavorId { get; set; }
        [Display(Name = "Flavor Name: ")]
        public string FlavorName { get; set; }
        public List<FlavorTreat> JoinEntities { get; }
    }
}