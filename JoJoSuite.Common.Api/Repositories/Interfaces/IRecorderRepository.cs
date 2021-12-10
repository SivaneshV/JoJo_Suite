using JoJoSuite.Common.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JoJoSuite.Common.Api.Repositories.Interfaces
{
    public interface IRecorderRepository
    {
        Task<string> EndBot(Recorder recorder);
    }
}
