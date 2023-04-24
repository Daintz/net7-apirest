using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.Models
{
    public class Villa
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string Name {get; set;}
        public DateTime CreationDate {get; set;}
    }
}