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
    public class CPUBoundController : ControllerBase
    {
        private Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }

        // GET api/cpubound
        // .\bombardier.exe "http://localhost:5001/api/cpubound?start=1&end=1000000" -n 20 -t 100s
        [HttpGet]
        public async Task<ActionResult<int>> Get([FromQuery] int start, [FromQuery] int end)
        {
            return await GetPrimesCountAsync(start, end);
        }
    }
}
