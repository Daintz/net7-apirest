using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.Models.DTO
{
    public class VillaCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}