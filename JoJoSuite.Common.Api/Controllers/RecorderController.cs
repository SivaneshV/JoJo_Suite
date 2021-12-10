using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JoJoSuite.Common.Api.Models;
using JoJoSuite.Common.Api.Repositories.Interfaces;

namespace JoJoSuite.Common.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecorderController : ControllerBase
    {
        private readonly IRecorderRepository _dataRepo;
        [HttpPost]
        [Route("~/api/EndBot")]
        public async Task<ActionResult<string>> EndBot([FromBody]Recorder recorder)
        {
            return await _dataRepo.EndBot(recorder);
        }
    }
}