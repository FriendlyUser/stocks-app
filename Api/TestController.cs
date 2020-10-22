using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stock_notifications.Controllers
{
  // The first one specifies the route for actions in this controller as being api/[controller] which means if the controller is named GamesController the route will be api/Games.
  [Route("/api/[controller]")]
  // The second attribute, [ApiController], adds some useful validations to the class, such as ensuring every action method includes its own [Route] attribute.
  [ApiController]
  public class TestController : ControllerBase
  {
    // GET: api/Test
    // these extra routes are just extra, controller takes care of api/Test
    [Route("/api/Test2")]
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET: api/Test/5
    [Route("/api/Test2/{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST: api/Test
    [HttpPost]
    public string Post([FromBody] string value)
    {
      return "Post";
    }

    // PUT: api/Test/5
    [HttpPut("api/Test2/{id}")]
    public string Put(int id, [FromBody] string value)
    {
      return "Nothing";
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public string Delete(int id)
    {
      return "More of Nothing";
    }
  }
}