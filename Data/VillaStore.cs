using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.Models.DTO;

namespace ApiRest.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>{
            new VillaDTO{Id = 1, Name = "Vista a la piscina"},
            new VillaDTO{Id = 2, Name = "Vista a la playa"},
        };
    }
}