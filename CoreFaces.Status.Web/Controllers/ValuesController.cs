using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreFaces.Status.Web.Controllers
{
    [Route("api/[controller]")]

    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "sdsdsd";
        }
         
    }
}
