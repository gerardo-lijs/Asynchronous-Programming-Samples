using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AsyncWebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IOBoundController : ControllerBase
    {
        private string GetHtml()
        {
            var client = new System.Net.WebClient();
            string response = client.DownloadString("https://www.dotnetfoundation.org");
            return response;
        }

        // GET api/iobound
        // .\bombardier.exe "http://localhost:5000/api/iobound" -n 20 -t 100s
        [HttpGet]
        public ActionResult<int> Get()
        {
            var webContent = GetHtml();
            return webContent.Length;
        }

        // GET api/iobound/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/iobound
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/iobound/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/iobound/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
