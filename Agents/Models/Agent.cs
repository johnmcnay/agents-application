using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agents.Models
{
    public class Agent
    {
        public string agentCode { get; set; }

        public string name { get; set; }

        public string workingArea { get; set; }

        public float commission { get; set; }

        public string phoneNumber { get; set; }
        
    }
}