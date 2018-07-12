using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotzerAssignment.Model.DTO;

namespace SpotzerAssignment.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        [HttpPost]
        public void Post([FromBody]OrderDTO order)
        {
            ;
        }

    }
}
