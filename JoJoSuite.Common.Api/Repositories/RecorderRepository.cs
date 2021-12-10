using Microsoft.Extensions.Configuration;
using JoJoSuite.Common.Api.Models;
using JoJoSuite.Common.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoJoSuite.Common.Api.Repositories
{
    public class RecorderRepository: IRecorderRepository
    {
        private readonly IConfiguration _config;

        public RecorderRepository(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> EndBot(Recorder recorder)
        {
            throw new NotImplementedException();
        }
    }
}
