using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiRest.Models.DTO;
using ApiRest.Data;

namespace ApiRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas() {
            return Ok(VillaStore.villaList);
        }
    }
}